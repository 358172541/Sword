﻿using AutoMapper;
using Sword.Application.Dtos;
using Sword.Core;
using Sword.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sword.Api
{
    [Authorize]
    public class UserController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly ITransaction _transaction;
        private readonly IUserRepository _userRepository;

        public UserController(
            IMapper mapper,
            ITransaction transaction,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _transaction = transaction;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpGet, Route("api/users")]
        public async Task<IActionResult> Users_Search()
        {
            var users = await _userRepository.Entities.AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<List<UserDto>>(users));
        }

        /// <summary>
        /// 显示一些注释
        /// </summary>
        /// <param name="request">参数</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("api/auths/token")]
        public async Task<IActionResult> Auths_Token([FromBody] TokenRequest request)
        {
            var user = await _userRepository.Entities.SingleOrDefaultAsync(x => x.Account == request.Account && x.Password == request.Password);

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
                        Program.Issuer,
                        Program.Audience,
                        new Claim[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString())
                        },
                        notBefore,
                        tokenExpireTime,
                        new SigningCredentials(new SymmetricSecurityKey(
                            Convert.FromBase64String(Program.Secret)),
                            SecurityAlgorithms.HmacSha256)
                    ));
                user.Token = token;
                user.TokenExpireTime = tokenExpireTime;
                user.TokenRefreshExpireTime = tokenRefreshExpireTime;
                await _userRepository.UpdateAsync(user);
                await _transaction.SaveChangesAsync();
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
                            Program.Issuer,
                            Program.Audience,
                            new Claim[]
                            {
                                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString())
                            },
                            notBefore,
                            tokenExpireTime,
                            new SigningCredentials(new SymmetricSecurityKey(
                                Convert.FromBase64String(Program.Secret)),
                                SecurityAlgorithms.HmacSha256)
                        ));
                    user.Token = token;
                    user.TokenExpireTime = tokenExpireTime;
                    user.TokenRefreshExpireTime = tokenRefreshExpireTime;
                    await _userRepository.UpdateAsync(user);
                    await _transaction.SaveChangesAsync();
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
        /// 显示一些注释
        /// </summary>
        /// <param name="request">参数</param>
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
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Program.Secret)),
                        RequireExpirationTime = true,
                        ValidAudience = Program.Audience,
                        ValidIssuer = Program.Issuer,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = false
                    },
                    out var token);
            }
            catch (Exception)
            {
                // return BadRequest(); // ???
                throw new TokenException("token invalid.");
            }

            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;

            var claim = claimsIdentity.FindFirst(x => x.Type == JwtRegisteredClaimNames.Sub);

            _ = Guid.TryParse(claim?.Value, out var userId);

            var user = await _userRepository.FindAsync(userId);

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
                            Program.Issuer,
                            Program.Audience,
                            new Claim[]
                            {
                                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString())
                            },
                            notBefore,
                            tokenExpireTime,
                            new SigningCredentials(new SymmetricSecurityKey(
                                Convert.FromBase64String(Program.Secret)),
                                SecurityAlgorithms.HmacSha256)
                        ));
                    user.Token = token;
                    user.TokenExpireTime = tokenExpireTime;
                    user.TokenRefreshExpireTime = tokenRefreshExpireTime;
                    await _userRepository.UpdateAsync(user);
                    await _transaction.SaveChangesAsync();
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
        /// 显示一些注释
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/auths/profile")]
        public async Task<IActionResult> Auths_Profile()
        {
            var user = await _userRepository.FindAsync(Identity);
            return Ok(user);
        }

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