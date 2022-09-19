﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AccountController (JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        private IEnumerable<User> Logins = new List<User> ()
        {
            new User()
            {
                Id = 1,
                Email = "admin@correo.cl",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id = 2,
                Email = "pepe@correo.cl",
                Name = "User1",
                Password = "pepe"
            }
        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = Login.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if (Valid)
                {
                    var user = Login.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else return BadRequest("Wrong password");

                return Ok(Token);
            }
            catch(Exception ex)
            {
                throw new Exception("GetToken error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }
}
