using Api.Exceptions;
using Core;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(
            IConfiguration configuration,
            ITransaction transaction,
            IUserRepository userRepository)
        {
            Configuration = configuration;
            Transaction = transaction;
            UserRepository = userRepository;
        }
        public IConfiguration Configuration { get; }
        public ITransaction Transaction { get; }
        public IUserRepository UserRepository { get; }

        /// <summary>
        /// token request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("api/auths/token")]
        public async Task<IActionResult> Auths_Token([FromBody] TokenRequest request)
        {
            var user = await UserRepository.Entities.SingleOrDefaultAsync(x => x.Account == request.Account && x.Password == request.Password);

            if (user == null)
                return NotFound();

            if (user.Token == null ||
                user.TokenExpireTime == null ||
                user.TokenRefreshExpireTime == null ||
                user.TokenRefreshExpireTime <= DateTime.Now)
            {
                var notBefore = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); // truncate to whole seconds
                var tokenExpireTime = notBefore.AddSeconds(60 * 5);
                var tokenRefreshExpireTime = notBefore.AddSeconds(60 * 30); // yes and also update tokenRefreshExpireTime
                var token = new JwtSecurityTokenHandler().WriteToken(
                    new JwtSecurityToken(
                        Configuration["AuthSetting:Issuer"],
                        Configuration["AuthSetting:Audience"],
                        new Claim[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString())
                        },
                        notBefore,
                        tokenExpireTime,
                        new SigningCredentials(new SymmetricSecurityKey(
                            Convert.FromBase64String(Configuration["AuthSetting:Secret"])),
                            SecurityAlgorithms.HmacSha256)
                    ));
                user.Token = token;
                user.TokenExpireTime = tokenExpireTime;
                user.TokenRefreshExpireTime = tokenRefreshExpireTime;
                await UserRepository.UpdateAsync(user);
                await Transaction.SaveChangesAsync();
            }
            else
            {
                if (user.TokenExpireTime <= DateTime.Now)
                {
                    var notBefore = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); // truncate to whole seconds
                    var tokenExpireTime = notBefore.AddSeconds(60 * 5);
                    var tokenRefreshExpireTime = user.TokenRefreshExpireTime; // yes but not update tokenRefreshExpireTime
                    var token = new JwtSecurityTokenHandler().WriteToken(
                        new JwtSecurityToken(
                            Configuration["AuthSetting:Issuer"],
                            Configuration["AuthSetting:Audience"],
                            new Claim[]
                            {
                                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString())
                            },
                            notBefore,
                            tokenExpireTime,
                            new SigningCredentials(new SymmetricSecurityKey(
                                Convert.FromBase64String(Configuration["AuthSetting:Secret"])),
                                SecurityAlgorithms.HmacSha256)
                        ));
                    user.Token = token;
                    user.TokenExpireTime = tokenExpireTime;
                    user.TokenRefreshExpireTime = tokenRefreshExpireTime;
                    await UserRepository.UpdateAsync(user);
                    await Transaction.SaveChangesAsync();
                }
            }

            return Ok(new TokenResponse
            {
                Token = user.Token,
                TokenExpireTime = (DateTime)user.TokenExpireTime,
                TokenRefreshExpireTime = (DateTime)user.TokenRefreshExpireTime
            });
        }

        /// <summary>
        /// token refresh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut, Route("api/auths/token/refresh")]
        public async Task<IActionResult> Auths_Token_Refresh([FromBody] TokenRefreshRequest request)
        {
            var claimsPrincipal = default(ClaimsPrincipal);

            try
            {
                claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(
                    request.Token,
                    new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Configuration["AuthSetting:Secret"])),
                        RequireExpirationTime = true,
                        ValidAudience = Configuration["AuthSetting:Audience"],
                        ValidIssuer = Configuration["AuthSetting:Issuer"],
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = false
                    },
                    out var token);
            }
            catch (Exception)
            {
                throw new TokenException("token invalid.");
            }

            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;

            var claim = claimsIdentity.FindFirst(x => x.Type == JwtRegisteredClaimNames.Sub);

            _ = Guid.TryParse(claim?.Value, out var userId);

            var user = await UserRepository.FindAsync(userId);

            if (user.Token != request.Token ||
                user.TokenExpireTime.Value != request.TokenExpireTime ||
                user.TokenRefreshExpireTime.Value != request.TokenRefreshExpireTime ||
                user.TokenRefreshExpireTime <= DateTime.Now)
            {
                throw new TokenException("token invalid.");
            }
            else
            {
                if (user.TokenExpireTime <= DateTime.Now)
                {
                    var notBefore = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); // truncate to whole seconds
                    var tokenExpireTime = notBefore.AddSeconds(60);
                    var tokenRefreshExpireTime = user.TokenRefreshExpireTime; // yes but not update tokenRefreshExpireTime
                    var token = new JwtSecurityTokenHandler().WriteToken(
                        new JwtSecurityToken(
                            Configuration["AuthSetting:Issuer"],
                            Configuration["AuthSetting:Audience"],
                            new Claim[]
                            {
                                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString())
                            },
                            notBefore,
                            tokenExpireTime,
                            new SigningCredentials(new SymmetricSecurityKey(
                                Convert.FromBase64String(Configuration["AuthSetting:Secret"])),
                                SecurityAlgorithms.HmacSha256)
                        ));
                    user.Token = token;
                    user.TokenExpireTime = tokenExpireTime;
                    user.TokenRefreshExpireTime = tokenRefreshExpireTime;
                    await UserRepository.UpdateAsync(user);
                    await Transaction.SaveChangesAsync();
                }
            }

            return Ok(new TokenResponse
            {
                Token = user.Token,
                TokenExpireTime = (DateTime)user.TokenExpireTime,
                TokenRefreshExpireTime = (DateTime)user.TokenRefreshExpireTime
            });
        }

        //[HttpGet, Route("api/auths/profile")]
        //public async Task<IActionResult> Auths_Profile()
        //{
        //    var user = await _userRepository.FindAsync(Identity);
        //    return Ok(user);
        //}

        //[HttpGet, Route("api/auths/resource")]
        //public async Task<IActionResult> Auths_Resource()
        //{
        //    var user = await _userRepository.FindAsync(Identity);
        //    if (user.Type == UserType.MANAGER)
        //    {
        //        var rescs = await _rescRepository.Entities.AsNoTracking().ToListAsync();
        //        return Ok(Mapper.Map<List<RescModel>>(rescs));
        //    }
        //    else
        //    {
        //        var roleIds = await _userRoleRepository.Entities.AsNoTracking().Where(x => x.UserId == Identity).Select(x => x.RoleId).ToListAsync();
        //        var rescIds = await _roleRescRepository.Entities.AsNoTracking().Where(x => roleIds.Contains(x.RoleId)).Select(x => x.RescId).ToListAsync();
        //        var rescs = await _rescRepository.Entities.AsNoTracking().Where(x => rescIds.Contains(x.RescId)).ToListAsync();
        //        return Ok(Mapper.Map<List<RescModel>>(rescs));
        //    }
        //}

        #region Token        
        //private dynamic TokenGenerate(string subject)
        //{
        //    var notBefore = DateTime.Now;
        //    var accessTokenExpireTime = notBefore.AddSeconds(3600);
        //    var accessToken = new JwtSecurityTokenHandler().WriteToken(
        //        new JwtSecurityToken(
        //            Sword.Issuer,
        //            Sword.Audience,
        //            new Claim[]
        //            {
        //                new Claim(JwtRegisteredClaimNames.Sub, subject)
        //            },
        //            notBefore,
        //            accessTokenExpireTime,
        //            new SigningCredentials(new SymmetricSecurityKey(
        //                Convert.FromBase64String(Sword.Secret)),
        //                SecurityAlgorithms.HmacSha256)
        //        )
        //    );
        //    var refreshTokenExpireTime = notBefore.AddSeconds(3600 * 24);
        //    var bytes = new byte[32];
        //    using (var generator = RandomNumberGenerator.Create()) generator.GetBytes(bytes);
        //    var refreshToken = Convert.ToBase64String(bytes);
        //    return new
        //    {
        //        AccessToken = accessToken,
        //        AccessTokenExpireTime = accessTokenExpireTime,
        //        RefreshToken = refreshToken,
        //        RefreshTokenExpireTime = refreshTokenExpireTime
        //    };
        //}
        //private ClaimsPrincipal TokenValidate(string accessToken)
        //{
        //    try
        //    {
        //        var validationParameters = new TokenValidationParameters
        //        {
        //            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Sword.Secret)),
        //            RequireExpirationTime = true,
        //            ValidAudience = Sword.Audience,
        //            ValidIssuer = Sword.Issuer,
        //            ValidateAudience = true,
        //            ValidateIssuer = true,
        //            ValidateIssuerSigningKey = true,
        //            ValidateLifetime = false
        //        };
        //        return new JwtSecurityTokenHandler().ValidateToken(accessToken, validationParameters, out var validatedToken);
        //    }
        //    catch (Exception)
        //    {
        //        throw new TokenException("access_token invalid."); // exception filter will handle it
        //    }
        //}
        #endregion
    }
}
