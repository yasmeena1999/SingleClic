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
    public class BlogPostRepository : IBlogPostRepo
    {
        private readonly BloggingContext _context;

        public BlogPostRepository(BloggingContext context)
        {
            _context = context;
        }
        public async Task<BlogPost> GetByIdAsync(int id)
        {
            return await _context.BlogPosts
                .Include(bp => bp.Author)
                .FirstOrDefaultAsync(bp => bp.Id == id);
        }

        public async Task<IReadOnlyList<BlogPost>> ListAllAsync()
        {
            return await _context.BlogPosts
                .Include(bp => bp.Author)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(BlogPost entity)
        {
            _context.BlogPosts.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BlogPost entity)
        {
            _context.BlogPosts.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(BlogPost entity)
        {
            _context.BlogPosts.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IReadOnlyList<BlogPost>> SearchPostsAsync(string title, string authorname)
        {
            var query = _context.BlogPosts.Include(bp => bp.Author).AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(bp => bp.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(authorname))
            {
                query = query.Where(bp => bp.Author.UserName.Contains( authorname));
            }

            return await query.OrderByDescending(bp => bp.CreationDate).ToListAsync();
        }
    }
}
