using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtTokenDemo.Model.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenDemo.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UserDataController : Controller
    {
        // GET api/UserData
        [HttpGet]
        public UserDataResponse Get()
        {
            return new UserDataResponse { FirstName = "Venkata", LastName = "Maganti", Gender = "Male", Age = 37, Message = "Successfully Authenticated by JWT" };
        }
    }
}