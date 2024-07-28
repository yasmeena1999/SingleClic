using SigleClic.Data.Responses;
using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Services.IServices
{
    public interface IPostService
    {
        Task<BaseResponse<BlogPost>> CreatePostAsync(BlogPost post);
        Task<BaseResponse<BlogPost>> GetPostByIdAsync(int postId);
        Task<BaseResponse<IEnumerable<BlogPost>>> GetPostsAsync(string title, string authorId);
        Task<BaseResponse<BlogPost>> UpdatePostAsync(int postId, BlogPost updatedPost);
        Task<BaseResponse<bool>> DeletePostAsync(int postId);
    }
}
