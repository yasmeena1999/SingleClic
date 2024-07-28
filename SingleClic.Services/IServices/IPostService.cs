using SigleClic.Data.Responses;
using SingleClic.Core.DTOs;
using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Services.IServices
{
    public interface IPostService
    {
        Task<BaseResponse<BlogPost>> CreatePostAsync(BlogPostDto post, ClaimsPrincipal userPrincipal);
        Task<BaseResponse<BlogPost>> GetPostByIdAsync(int postId);
        Task<BaseResponse<IEnumerable<BlogPost>>> GetPostsAsync(string title, string authorId);
        Task<BaseResponse<BlogPost>> UpdatePostAsync(int postId, BlogPost updatedPost, string currentUserId);
        Task<BaseResponse<bool>> DeletePostAsync(int postId, string currentUserId);
    }
}
