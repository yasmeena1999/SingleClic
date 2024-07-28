using Microsoft.AspNetCore.Identity;
using SigleClic.Data.Responses;
using SingleClic.Core.Models;
using SingleClic.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Services.IServices
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(User user);
        Task<AuthenticationResponse> Register(RegisterRequestDto request);
        Task<BaseResponse<string>> login(string Email, string password);
    }
}
