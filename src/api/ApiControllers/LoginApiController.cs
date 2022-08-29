using api.Models.LoginApi;
using auth;
using core;
using entities.Interfaces;
using Microsoft.IdentityModel.Tokens;
using operations.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Http;

namespace api.ApiControllers
{
    public class LoginApiController : BaseApiController
    {
        private IUserOperation UserOperation { get; set; }

        public LoginApiController(IUserOperation userOperation)
        {
            UserOperation = userOperation;
        }

        [HttpPost]
        public TokenModel Token(UserModel model)
        {
            var identity = GetIdentity(model.Name, model.Password);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenModel { AccessToken = encodedJwt, Name = identity.Name };
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            IUser user = UserOperation.GetObject(username);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
