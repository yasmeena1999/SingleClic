using SigleClic.Data.Responses;
using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigleClic.Data.IRepos
{
    public interface IUserRepository
    {
       
        Task<bool> LoginAsync(string email, string password);
        
        Task<AuthenticationResponse> RegisterAsync(User user, string password);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(string id);
    }
}
