using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crud_api.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace crud_api.Controllers
{

    
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandlerRepository tokenHandlerRepository;

        public AuthController(IUserRepository userRepository, ITokenHandlerRepository tokenHandlerRepository)
        {
            this.userRepository = userRepository;
            this.tokenHandlerRepository = tokenHandlerRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {

            //Check if user is authenticated

            //Check username and password
            var user = await userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
            if (user != null)
            {
                //Generate a JWT token
                var token = await tokenHandlerRepository.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username or Password is invalid");
        }
    }
}