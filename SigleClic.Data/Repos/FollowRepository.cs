using Microsoft.EntityFrameworkCore;
using SigleClic.Data.Context;
using SigleClic.Data.IRepos;
using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigleClic.Data.Repos
{
    public class FollowRepository :IFollowRepository
    {
        private readonly BloggingContext _context;

        public FollowRepository(BloggingContext context)
        {
            _context = context;
        }
        public async Task<Follow> GetFollowAsync(string followerId, string followeeId)
        {
            return await _context.UserFollowers
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
        }

        public async Task AddAsync(Follow follow)
        {
            await _context.UserFollowers.AddAsync(follow);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Follow follow)
        {
            _context.UserFollowers.Remove(follow);
            await _context.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<User>> GetFollowersByUserIdAsync(string userId)
        {
            return await _context.UserFollowers
                .Where(f => f.FolloweeId == userId)
                .Select(f => f.Follower)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<User>> GetFollowingsByUserIdAsync(string userId)
        {
            return await _context.UserFollowers
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToListAsync();
        }
    }
}
