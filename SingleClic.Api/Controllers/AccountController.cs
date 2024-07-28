using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SingleClic.Core.DTOs;
using SingleClic.Core.Models;
using SingleClic.Services.IServices;
using System.ComponentModel.DataAnnotations;

namespace SingleClic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper, IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authService.login(request.Email, request.Password);


            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
           
            var response = await _authService.Register(request);


            return Ok(response);
        }

    }
}
