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
        Task<BaseResponse<PostRetrievedDto>> CreatePostAsync(BlogPostDto post,string userid);
        Task<BaseResponse<PostRetrievedDto>> GetPostByIdAsync(int postId);
        Task<BaseResponse<IEnumerable<PostRetrievedDto>>> GetPostsAsync(string title, string authorId);
        Task<BaseResponse<PostRetrievedDto>> UpdatePostAsync(int postId, BlogPostDto updatedPostDto, string currentUserId);
        Task<BaseResponse<bool>> DeletePostAsync(int postId, string currentUserId);
        Task<BaseResponse<IEnumerable<PostRetrievedDto>>> GetAllPostsAsync();
    }
}
