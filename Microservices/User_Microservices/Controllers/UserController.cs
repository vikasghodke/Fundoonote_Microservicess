using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User_Microservices.IService;
using User_Microservices.UserDBContext;
using User_Microservices.UserModel;

namespace User_Microservices.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string V = "not found";
        public readonly IUserService _userService;
        public readonly IConfiguration _config;
        public readonly IEmailService _emailService;

        public UserController(IUserService userService, IConfiguration config, IEmailService emailService)
        {
            _userService = userService;
            _config = config;
            _emailService = emailService;
        }
        [HttpPost("Add")]
        public IActionResult AddUserDetail(UserM userM)
        {
            var result = _userService.AddUserDetail(userM);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpGet]
        public ResponseModel<UserDisplay> ViewUserDetail(int userID)
        {
            var response = new ResponseModel<UserDisplay>();
            try
            {
                var user = _userService.ViewUserDetail(userID);
                if (user != null)
                {
                    response.Message = "User details retrived successfully.";
                    response.Data = user;
                }
                else
                {
                    response.Success = false;
                    response.Message = "User Not Found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

           
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            var result=_userService.Login(userLogin);

            if(result!=null)
            {
                return Ok(new { Success = true, Message = "Login Sucessfully", Data = result });
            
            }
            else
            {
                return BadRequest(new { Success = false, Message = "something wet wrong" });
            }
        }
        [HttpPost]
        [Route("ForgetPass")]

        public Task<string> ForgetPass(string Email)
        {

            var result = _userService.ForgetPass(Email);
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }

        }
        [HttpPost("ResetPass")]
        public Task<string> ResetPassword(string token, string Password1)
        {
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            SecurityToken validatedToekn;
            var principle = handler.ValidateToken(token, validationParameters, out validatedToekn);
            var userID = principle.FindFirstValue(ClaimTypes.NameIdentifier);
            int _userID = Convert.ToInt32(userID);
            var result = _userService.ResetPassword(Password1, _userID);

            if (result != null)
            {
                return result;

            }
            else
            {
                return null;
            }

        }
    }
}
