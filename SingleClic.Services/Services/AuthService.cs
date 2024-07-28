using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SigleClic.Data.IRepos;
using SigleClic.Data.Responses;
using SingleClic.Core.Models;
using SingleClic.Core.DTOs;
using SingleClic.Services.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SingleClic.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;
       

        public AuthService(IConfiguration configuration,IUserRepository userRepository,IMapper mapper)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mapper = mapper;
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

        }



        public async Task<string> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public async Task<BaseResponse<string>> login(string Email, string password)
        {
            

            var validLogin = await _userRepository.LoginAsync(Email,password);

            if (!validLogin)
                throw new Exception("Invalid Email or password!");

            var user = await _userRepository.GetByEmailAsync(Email);

            var token = await CreateTokenAsync(user);
            return new BaseResponse<string> { Message = $"{user.UserName} Authenticated Successfully", Result = token };

        }
        public async Task<AuthenticationResponse> Register(RegisterRequestDto request)
        {
            var user = _mapper.Map<User>(request);
            var response = await _userRepository.RegisterAsync(user, request.Password);

            return response;
        }

    }
}
