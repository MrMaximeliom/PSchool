using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSchool.Backend.Models;
using PSchool.Backend.Services;

namespace PSchool.Backend.Controllers
{
    [Route("api/auth")]
    [Produces("application/json")]  
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Login users and returns token
        /// </summary>
        /// 
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>a new valid token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     {
        ///        "email": "abc@examble.com",
        ///        "password": "password"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns an auth object with valid token and user's details</response>
        /// <response code="400">Returns bad request if email or password is not submitted or no user found </response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 

        public async Task<IActionResult> Login(RequestToken model)
        {
            var auth = await _authService.GetTokenAsync(model);

            if(!auth.IsAuthenticated)
            {
                return BadRequest(auth.Message);    
            }

            return Ok(auth);
        }

        /// <summary>
        /// Register users and returns token
        /// </summary>
        /// 
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>a new valid token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///        "firstName": "first_name",
        ///        "lastName": "last_name",
        ///        "email": "test@gmail.com",
        ///        "password": "password",
        ///        "phoneNumber": "0565454321"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns an auth object with valid token and user's details</response>
        /// <response code="400">Returns bad request if submitted data was wrong </response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            var auth = await _authService.RegisterAsync(model);

            if(!auth.IsAuthenticated) {
            
            return BadRequest(auth.Message);
            }
            return Ok(auth);
        }

        /// <summary>
        /// Revoke token
        /// </summary>
        /// 
        /// <param name="refreshToken"></param>
        /// <returns>returns true if token has been revoked or false otherwise</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /revoke-token
        ///     {
        ///        "refreshToken": "token"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns true if token has been revoked</response>
        /// <response code="400">Returns bad request if submitted token is not valid </response>
        [HttpPut("revoke-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeToken(string refreshToken)
        {
            var isRevoked = await _authService.RevokeTokenAsync(refreshToken);

            if (!isRevoked)
            {
                return BadRequest("either there is no user with this token or this token is invalid!");
            }

            return Ok("refresh token has been successfully revoked");


        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// 
        /// <param name="refreshToken"></param>
        /// <returns>returns new token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /refresh-token
        ///     {
        ///        "refreshToken": "token"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns true if token has been revoked</response>
        /// <response code="400">Returns bad request if submitted token is not valid </response>
        [HttpPut("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var auth = await _authService.RefreshTokenAsync(refreshToken);

            if (!auth.IsAuthenticated)
            {
                return BadRequest("either there is no user with this token or this token is invalid!");
            }

            return Ok(auth);


        }

        /// <summary>
        /// Add user to named role
        /// </summary>
        /// 
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns>returns new token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /add-user-to-role
        ///     {
        ///        "userId": "user_id"
        ///        "role": "role_name"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns user has been added to role </response>
        /// <response code="400">Returns bad request if submitted data was wrong </response>
        [HttpPost("add-user-to-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserToRoleToken(AddRole model)
        {
            var resultText = await _authService.AddUserToRoleAsync(model);

            if (!string.IsNullOrEmpty(resultText))
            {
                return BadRequest("either there is no user with this token or this token is invalid!");
            }

            return Ok("User has been added to role successfully");


        }

    }

}
