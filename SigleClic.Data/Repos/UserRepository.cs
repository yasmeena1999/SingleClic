using Microsoft.AspNetCore.Identity;
using SigleClic.Data.Context;
using SigleClic.Data.IRepos;
using SigleClic.Data.Responses;
using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigleClic.Data.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly BloggingContext _context;
        public UserRepository(UserManager<User> userManager, BloggingContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }


      
        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }


            return await _userManager.CheckPasswordAsync(user, password);
        }

      
            public async Task<AuthenticationResponse> RegisterAsync(User user, string password)
            {
                var createResult = await _userManager.CreateAsync(user, password);
                if (!createResult.Succeeded)
                    return new AuthenticationResponse()
                    {
                        Success = createResult.Succeeded,
                        Errors = createResult.Errors.Select(e => e.Description).ToArray()
                    };

               

                return new AuthenticationResponse() { Success = createResult.Succeeded, Id = user.Id };
            }
      
    }
    }



