using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TSYSCertApi.Classes;
//using TSYSCertApi.Classes.JwtToken;
using TSYSCertApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TSYSCertApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        //private readonly ILogger<TokenController> _logger;
        //private readonly IUserService _userService;
        //private readonly IJwtAuthManager _jwtAuthManager;

        //public TokenController(ILogger<TokenController> logger, IUserService userService, IJwtAuthManager jwtAuthManager)
        //{
        //    _logger = logger;
        //    _userService = userService;
        //    _jwtAuthManager = jwtAuthManager;
        //}

        //{
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }


        // POST api/<TokenController>
        [HttpPost]
        public IActionResult Post(TokenModel LoginParams)
        {
            IActionResult response = Unauthorized(); //initialized to Unauthorized

            var objTokenAuth = new clsTokenAuth(_config);


            var devuser = objTokenAuth.AuthenticateUser(LoginParams);

            if (devuser != null)
            {
                objTokenAuth.GenerateJSONWebToken(devuser); //perform token generation

                var tokenString = objTokenAuth.NewToken;
                var expiration = objTokenAuth.Expires;
                var userfirstname = objTokenAuth.UserFirstName;
                var userlastname = objTokenAuth.UserLastName;

                response = Ok(new { token = tokenString, expires = expiration, userfirstname, userlastname });
            }


            return response;

            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest();
            //    }

            //    if (!_userService.IsValidUserCredentials(LoginParams.LoginID, LoginParams.Password))
            //    {
            //        return Unauthorized();
            //    }

            //    var role = _userService.GetUserRole(LoginParams.LoginID);
            //    var claims = new[]
            //    {
            //    new Claim(ClaimTypes.Name,LoginParams.LoginID),
            //    new Claim(ClaimTypes.Role, role)
            //};

            //    var jwtResult = _jwtAuthManager.GenerateTokens(LoginParams.LoginID, claims, DateTime.Now);
            //    _logger.LogInformation($"User [{LoginParams.LoginID}] logged in the system.");
            //    return Ok(new LoginResult
            //    {
            //        UserName = LoginParams.LoginID,
            //        Role = role,
            //        AccessToken = jwtResult.AccessToken,
            //        RefreshToken = jwtResult.RefreshToken.TokenString
            //    });


        }


        //[HttpGet("user")]
        //[Authorize]
        //public ActionResult GetCurrentUser()
        //{
        //    return Ok(new LoginResult
        //    {
        //        UserName = User.Identity?.Name,
        //        Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
        //        OriginalUserName = User.FindFirst("OriginalUserName")?.Value
        //    });
        //}

        //[HttpPost("logout")]
        //[Authorize]
        //public ActionResult Logout()
        //{
        //    // optionally "revoke" JWT token on the server side --> add the current token to a block-list
        //    // https://github.com/auth0/node-jsonwebtoken/issues/375

        //    var userName = User.Identity?.Name;
        //    _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
        //    _logger.LogInformation($"User [{userName}] logged out the system.");
        //    return Ok();
        //}

        //[HttpPost("refresh-token")]
        //[Authorize]
        //public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        //{
        //    try
        //    {
        //        var userName = User.Identity?.Name;
        //        _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

        //        if (string.IsNullOrWhiteSpace(request.RefreshToken))
        //        {
        //            return Unauthorized();
        //        }

        //        var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
        //        var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
        //        _logger.LogInformation($"User [{userName}] has refreshed JWT token.");
        //        return Ok(new LoginResult
        //        {
        //            UserName = userName,
        //            Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
        //            AccessToken = jwtResult.AccessToken,
        //            RefreshToken = jwtResult.RefreshToken.TokenString
        //        });
        //    }
        //    catch (SecurityTokenException e)
        //    {
        //        return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
        //    }
        //}

        //[HttpPost("impersonation")]
        //[Authorize(Roles = UserRoles.Admin)]
        //public ActionResult Impersonate([FromBody] ImpersonationRequest request)
        //{
        //    var userName = User.Identity?.Name;
        //    _logger.LogInformation($"User [{userName}] is trying to impersonate [{request.UserName}].");

        //    var impersonatedRole = _userService.GetUserRole(request.UserName);
        //    if (string.IsNullOrWhiteSpace(impersonatedRole))
        //    {
        //        _logger.LogInformation($"User [{userName}] failed to impersonate [{request.UserName}] due to the target user not found.");
        //        return BadRequest($"The target user [{request.UserName}] is not found.");
        //    }
        //    if (impersonatedRole == UserRoles.Admin)
        //    {
        //        _logger.LogInformation($"User [{userName}] is not allowed to impersonate another Admin.");
        //        return BadRequest("This action is not supported.");
        //    }

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Name,request.UserName),
        //        new Claim(ClaimTypes.Role, impersonatedRole),
        //        new Claim("OriginalUserName", userName ?? string.Empty)
        //    };

        //    var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
        //    _logger.LogInformation($"User [{request.UserName}] is impersonating [{request.UserName}] in the system.");
        //    return Ok(new LoginResult
        //    {
        //        UserName = request.UserName,
        //        Role = impersonatedRole,
        //        OriginalUserName = userName,
        //        AccessToken = jwtResult.AccessToken,
        //        RefreshToken = jwtResult.RefreshToken.TokenString
        //    });
        //}

        //[HttpPost("stop-impersonation")]
        //public ActionResult StopImpersonation()
        //{
        //    var userName = User.Identity?.Name;
        //    var originalUserName = User.FindFirst("OriginalUserName")?.Value;
        //    if (string.IsNullOrWhiteSpace(originalUserName))
        //    {
        //        return BadRequest("You are not impersonating anyone.");
        //    }
        //    _logger.LogInformation($"User [{originalUserName}] is trying to stop impersonate [{userName}].");

        //    var role = _userService.GetUserRole(originalUserName);
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.Name,originalUserName),
        //        new Claim(ClaimTypes.Role, role)
        //    };

        //    var jwtResult = _jwtAuthManager.GenerateTokens(originalUserName, claims, DateTime.Now);
        //    _logger.LogInformation($"User [{originalUserName}] has stopped impersonation.");
        //    return Ok(new LoginResult
        //    {
        //        UserName = originalUserName,
        //        Role = role,
        //        OriginalUserName = null,
        //        AccessToken = jwtResult.AccessToken,
        //        RefreshToken = jwtResult.RefreshToken.TokenString
        //    });
        //}
        //}




    }

}
