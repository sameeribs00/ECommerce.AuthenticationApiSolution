using AuthenticationApi.Application.DTOs;
using AuthenticationApi.Application.Interfaces;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUser user) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginRequest)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await user.Login(loginRequest);
            return new JsonResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AppUserDto userDto)
        {
            var result = await user.Register(userDto);
            return new JsonResult(result);
        }

        [HttpGet("byId/{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id <= 0) return BadRequest("Invalid Id Provided");

            var result = await user.GetUserById(id);
            return new JsonResult(result);
        }

        [HttpGet("byEmail/{emailAddress}")]
        public async Task<IActionResult> GetUserByEmailAddress(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress)) return BadRequest("Invalid Email Address Provided");

            var result = await user.GetUserByEmailAddress(emailAddress);
            return new JsonResult(result);
        }
    }
}
