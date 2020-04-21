using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtTokenDemo.Model.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenDemo.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        // POST api/Token
        [HttpPost]
        public IActionResult GetToken([FromBody] TokenRequest tokenRequest)
        {
            if(!ModelState.IsValid) {
                return BadRequest();
            }

            if (!VerifyCredentials(tokenRequest.Username, tokenRequest.Password)) {
                return Unauthorized();
            }

            //The user provided valid credentials
            //Let's create a ClaimsIdentity for him
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            //We add one or more claims relating to the logged in user
            identity.AddClaim(new Claim(ClaimTypes.Name, tokenRequest.Username));
            //We encapsulate the identity in a ClaimsPrincipal and associate it with the current request
            HttpContext.User = new ClaimsPrincipal(identity);

            //We don't return anything. The token will be produced by the JwtTokenMiddleware
            return NoContent();
        }

        private bool VerifyCredentials(string username, string password) {
            //Let's see if the credentials provided are valid
            //TODO: Modify this implementation, which is purely demonstrative
            return username == "test" && password == "test";
        }

    }
}
