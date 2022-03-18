using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core
{
    [Authorize]
    public abstract class BaseController : ControllerBase
    {
        protected Guid Identity
        {
            get
            {
                var identity = Guid.Empty;
                var subject = (User.Identity as ClaimsIdentity).FindFirst(x => x.Type == JwtRegisteredClaimNames.Sub);
                if (subject != null) _ = Guid.TryParse(subject.Value, out identity);
                return identity;
            }
        }
    }
}
