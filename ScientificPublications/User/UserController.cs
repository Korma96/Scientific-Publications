using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Models;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.Service.User;

namespace ScientificPublications.User
{
    public class UserController : AbstractController
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UserController(
            IOptions<AppSettings> appSettings, 
            IUserService userService,
            IMapper mapper) : base(appSettings)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userService.Login(loginDto.Username, loginDto.Password);
            var sessionDto = _mapper.Map<SessionDto>(user);
            var idToken = JwtUtility.CreateJwtToken(AppSettings, sessionDto);
            Request.HttpContext.Response.Headers.Add(Constants.AccessToken, idToken);
            return Ok(user);
        }
    }
}
