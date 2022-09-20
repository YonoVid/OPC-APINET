using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Model.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDbContext _context;
        private readonly JwtSettings _jwtSettings;
        private readonly IStringLocalizer<User> _userLocalizer;

        public AccountController (UniversityDbContext context,
                                    JwtSettings jwtSettings,
                                    IStringLocalizer<User> userLocalizer)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _userLocalizer = userLocalizer;
        }

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = _context.Users.Any(user => user.Name.ToUpper() == userLogin.UserName.ToUpper());

                if (Valid)
                {
                    var user = _context.Users
                        .FirstOrDefault(user => user.Name.ToUpper() == userLogin.UserName.ToUpper() &&
                                                user.Password.Equals(userLogin.Password));
                    if (user != null)
                    {
                        Token = JwtHelpers.GenTokenKey(new UserTokens()
                        {
                            UserName = user.Name,
                            EmailId = user.Email,
                            Id = user.Id,
                            GuidId = Guid.NewGuid()
                        }, _jwtSettings);
                    }
                    else return BadRequest("Wrong password");
                }
                else return BadRequest("User not found");

                return Ok(new { 
                    Token,
                    Message = String.Format(_userLocalizer.GetString("Welcome"), Token.UserName)
            });
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
            return Ok(_context.Users);
        }
    }
}
