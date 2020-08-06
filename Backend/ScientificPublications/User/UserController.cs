using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Models;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.Service.User;
using System.Threading.Tasks;

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
        [Consumes(Constants.XmlContentType)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.Login(loginDto.Username, loginDto.Password);
            var sessionDto = _mapper.Map<SessionDto>(user);
            var idToken = JwtUtility.CreateJwtToken(AppSettings, sessionDto);
            Request.HttpContext.Response.Headers.Add(Constants.AccessToken, idToken);
            return Ok(user);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [Consumes(Constants.XmlContentType)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            _userService.Validate(registerDto);
            await _userService.Register(registerDto);

            var user = _mapper.Map<UserDto>(registerDto);
            var sessionDto = _mapper.Map<SessionDto>(user);
            var idToken = JwtUtility.CreateJwtToken(AppSettings, sessionDto);
            Request.HttpContext.Response.Headers.Add(Constants.AccessToken, idToken);

            return Ok(user);
        }

        [HttpGet("reviewers")]
        [AuthorizationFilter(Role.Editor)]
        public async Task<IActionResult> GetReviewers()
        {
            var reviewers = await _userService.GetReviewersAsync();
            return Ok(reviewers);
        }
    }
}
