using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Core.Extensions;
using System.Linq;
using Core.Utilities.Security.Encryption;

namespace Core.Utilities.Security.JWT
{
    public class JWTHelper : ITokenHelper
    {
        IConfiguration Configuration;
        TokenOptions _tokenOptions;
        DateTime _accessTokenExpiration;
        public JWTHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt=CreateJWTSecurityToken(_tokenOptions,user,operationClaims,signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token =jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken()
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }
        public JwtSecurityToken CreateJWTSecurityToken(TokenOptions tokenOptions,User user,
            List<OperationClaim> operationClaims,SigningCredentials signingCredentials)
        {
            var jwt=new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore:DateTime.Now,
                claims:SetClaims(user,operationClaims),
                signingCredentials:signingCredentials                
                );
            return jwt;
        }
        public IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddRoles(operationClaims.Select(o => o.Name).ToArray());
            return claims;
        }
    }
}
