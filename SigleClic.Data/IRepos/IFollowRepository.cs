using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigleClic.Data.IRepos
{
    public interface IFollowRepository
    {
        Task<Follow> GetFollowAsync(string followerId, string followeeId);
        Task AddAsync(Follow follow);
        Task DeleteAsync(Follow follow);
        Task<IReadOnlyList<User>> GetFollowersByUserIdAsync(string userId);
        Task<IReadOnlyList<User>> GetFollowingsByUserIdAsync(string userId);
    }
}
