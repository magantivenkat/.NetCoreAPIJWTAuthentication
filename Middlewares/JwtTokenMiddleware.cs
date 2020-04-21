using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace JwtTokenDemo.Middlewares {

    public class JwtTokenMiddleware {
        private readonly RequestDelegate next;
        public JwtTokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context) {
            //The moment another middleware produces the answer
            //we issue the token in the header X-Token header.
            //The header could have another name, the important thing is that
            //is documented to clients who will use our API
            context.Response.OnStarting(() => {
                var identity = context.User.Identity as ClaimsIdentity;
                if (identity.IsAuthenticated) {
                    //The client will be able to use this new token in his next request
                    context.Response.Headers.Add("token", CreateTokenForIdentity(identity));
                }
                return Task.CompletedTask;
            });
            await next.Invoke(context);
        }

        private StringValues CreateTokenForIdentity(ClaimsIdentity identity)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecureSecretKey"));
		    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: "Issuer",
                                             audience: "Audience",
                                             claims: identity.Claims,
                                			 expires: DateTime.Now.AddMinutes(2),
                                			 signingCredentials: credentials
                                             );
            var tokenHandler = new JwtSecurityTokenHandler();
            var serializedToken = tokenHandler.WriteToken(token);
            return serializedToken;
        }
    }
}