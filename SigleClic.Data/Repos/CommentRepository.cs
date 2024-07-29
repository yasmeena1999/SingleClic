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
    public class CommentRepository : ICommentRepo
    {
        private readonly BloggingContext _context;

        public CommentRepository(BloggingContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Comment entity)
        {
            _context.Comments.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Comment entity)
        {
            _context.Comments.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Comment entity)
        {
            _context.Comments.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Comment> GetByIdAsync(int id)
        { 
            return await _context.Comments
                .Include(bp => bp.User)
                .Include(bp=>bp.BlogPost)
                .FirstOrDefaultAsync(bp => bp.Id == id);
        }

        public Task<IReadOnlyList<Comment>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

       
    }
}
