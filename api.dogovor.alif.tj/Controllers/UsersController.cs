﻿using Domain.TransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.UserService;
using System.Security.Claims;
using Domain.ReturnMessage;

namespace api.dogovor.alif.tj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var returnMessage = await _userService.RegisterUser(dto);
            return returnMessage.StatusCode == System.Net.HttpStatusCode.BadRequest ? BadRequest(returnMessage) : Ok(returnMessage);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthorizationDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var returnMessage = await _userService.Login(dto);
            return returnMessage.StatusCode == System.Net.HttpStatusCode.BadRequest ? BadRequest(returnMessage) : Ok(returnMessage);
        }

        [HttpGet("CurrentUser")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CurrentUser()
        {
            var claim = User.Identity as ClaimsIdentity;
            return claim == null ? NotFound() : Ok(await _userService.UsersInformation(claim)); 
        }

        [HttpPost("SendEmailMessage")]
        public async Task<IActionResult> SendEmailMessage(string Email)
        {
            return Ok(await _userService.SendEmailCode(Email));
        }

        [HttpPost("VarifyUser")]
        public async Task<IActionResult> VerifyUser(RandomNumberDTO randpmNumberdto)
        {
            var userEmail = await _userService.VerifyUser(randpmNumberdto);
            return userEmail.StatusCode == System.Net.HttpStatusCode.NotFound ? BadRequest(userEmail) : Ok(userEmail);
        }

        [HttpPut("UpdatePassword")]
        public async Task<ActionResult> ResetPassword(NewPasswordDTO dto)
        {
            var reset = await _userService.UpdateUserPassword(dto);
            return reset == null ? BadRequest(reset) : Ok(reset);
        }
    }
}
