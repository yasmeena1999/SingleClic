using SigleClic.Data.Responses;
using SingleClic.Core.Models;
using SingleClic.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Services.Services
{
    public class PostService : IPostService
    {
        public Task<BaseResponse<BlogPost>> CreatePostAsync(BlogPost post)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeletePostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<BlogPost>> GetPostByIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<BlogPost>>> GetPostsAsync(string title, string authorId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<BlogPost>> UpdatePostAsync(int postId, BlogPost updatedPost)
        {
            throw new NotImplementedException();
        }
    }
}
