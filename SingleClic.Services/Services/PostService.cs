using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SigleClic.Data.IRepos;
using SigleClic.Data.Repos;
using SigleClic.Data.Responses;
using SingleClic.Core.DTOs;
using SingleClic.Core.Models;
using SingleClic.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;




namespace SingleClic.Services.Services
{
    public class PostService : IPostService
    {
        private readonly IBlogPostRepo _blogPostRepo;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public PostService(IBlogPostRepo blogPostRepo,IUserRepository userRepository,IMapper mapper, UserManager<User> userManager)
        {
            _blogPostRepo = blogPostRepo;
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<BaseResponse<BlogPost>> CreatePostAsync(BlogPostDto post, ClaimsPrincipal userPrincipal)
        {
            // Retrieve author ID from claims
            var authorClaim = userPrincipal.FindFirstValue("UserGuid");
           
            if (authorClaim == null )
            {
                return new BaseResponse<BlogPost>
                {
                    Message = "AuthError",
                    Success = false
                };
            }

            

            // Map DTO to BlogPost entity
            var postToCreate = _mapper.Map<BlogPost>(post);
            postToCreate.CreationDate = DateTime.Now;
            postToCreate.AuthorId = authorClaim; // Directly use authorId as a string

            // Add the post to the repository
            var createdPost = await _blogPostRepo.AddAsync(postToCreate);

            if (createdPost == null)
            {
                return new BaseResponse<BlogPost>
                {
                    Success = false,
                    Message = "Failed to create post."
                };
            }

            // Return successful response
            return new BaseResponse<BlogPost>
            {
                Success = true,
                Message = "Post created successfully.",
                Result = postToCreate
            };
        }



        public async  Task<BaseResponse<bool>> DeletePostAsync(int postId,string currentUserId)
        {
            var post = await _blogPostRepo.GetByIdAsync(postId);
            if (post == null)
            {
                return new BaseResponse<bool> { Success = false, Message = "Post not found." };
            }
            if (post.AuthorId != currentUserId)
            {
                return new BaseResponse<bool> { Success = false, Message = "You are not authorized to delete this post." };
            }

            var deleted = await _blogPostRepo.DeleteAsync(post);
            if (!deleted)
            {
                return new BaseResponse<bool> { Success = false, Message = "Failed to delete post." };
            }

            return new BaseResponse<bool> { Success = true, Message = "Post deleted successfully.", Result = true };
        }
    

        public async Task<BaseResponse<BlogPost>> GetPostByIdAsync(int postId)
        {
            var post = await  _blogPostRepo.GetByIdAsync(postId);
            if (post == null)
            {
                return new BaseResponse<BlogPost> { Success = false, Message = "Post not found." };
            }

            return new BaseResponse<BlogPost> { Success = true, Message = "Post retrieved successfully.", Result = post };
        }

        public async  Task<BaseResponse<BlogPost>> UpdatePostAsync(int postId, BlogPost updatedPost,string currentUserId)
        {
            var post = await _blogPostRepo.GetByIdAsync(postId);
            if (post == null)
            {
                return new BaseResponse<BlogPost> { Success = false, Message = "Post not found." };
            }
            if (post.AuthorId != currentUserId)
            {
                return new BaseResponse<BlogPost> { Success = false, Message = "You are not authorized to edit this post." };
            }

            _mapper.Map(updatedPost, post);
            var updated = await _blogPostRepo.UpdateAsync(post);
            if (!updated)
            {
                return new BaseResponse<BlogPost> { Success = false, Message = "Failed to update post." };
            }

            return new BaseResponse<BlogPost> { Success = true, Message = "Post updated successfully.", Result = post };
        }
        public async  Task<BaseResponse<IEnumerable<BlogPost>>> GetPostsAsync(string title, string authorname)
        {
            var posts = await _blogPostRepo.SearchPostsAsync(title, authorname);
            return new BaseResponse<IEnumerable<BlogPost>> { Success = true, Message = "Posts retrieved successfully.", Result = posts };
        }

       
    }
}
