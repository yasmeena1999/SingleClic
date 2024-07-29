using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Update;
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
        public async Task<BaseResponse<PostRetrievedDto>> CreatePostAsync(BlogPostDto post,string userid)
        {
           
            var postToCreate = _mapper.Map<BlogPost>(post);
            postToCreate.CreationDate = DateTime.Now;
            postToCreate.AuthorId = userid;
            var user = await _userRepository.GetByIdAsync(userid);
            postToCreate.Author = user;

            var createdPost = await _blogPostRepo.AddAsync(postToCreate);

            if (createdPost == null)
            {
                return new BaseResponse<PostRetrievedDto>
                {
                    Success = false,
                    Message = "Failed to create post."
                };
            }

            var retrieved = _mapper.Map<PostRetrievedDto>(postToCreate);
            return new BaseResponse<PostRetrievedDto>
            {
                Success = true,
                Message = "Post created successfully.",
                Result = retrieved
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
    

        public async Task<BaseResponse<PostRetrievedDto>> GetPostByIdAsync(int postId)
        {
            var post = await  _blogPostRepo.GetByIdAsync(postId);
            var retrieved= _mapper.Map<PostRetrievedDto>(post);
            if (post == null)
            {
                return new BaseResponse<PostRetrievedDto> { Success = false, Message = "Post not found." };
            }

            return new BaseResponse<PostRetrievedDto> { Success = true, Message = "Post retrieved successfully.", Result = retrieved };
        }

        public async Task<BaseResponse<PostRetrievedDto>> UpdatePostAsync(int postId, BlogPostDto updatedPost, string currentUserId)
        {
            var post = await _blogPostRepo.GetByIdAsync(postId);
            if (post == null)
            {
                return new BaseResponse<PostRetrievedDto> { Success = false, Message = "Post not found." };
            }
            if (post.AuthorId != currentUserId)
            {
                return new BaseResponse<PostRetrievedDto> { Success = false, Message = "You are not authorized to edit this post." };
            }

            
            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;
       

            var updated = await _blogPostRepo.UpdateAsync(post);
            if (!updated)
            {
                return new BaseResponse<PostRetrievedDto> { Success = false, Message = "Failed to update post." };
            }

            var result = _mapper.Map<PostRetrievedDto>(post);
            return new BaseResponse<PostRetrievedDto> { Success = true, Message = "Post updated successfully.", Result = result };
        }

        public async  Task<BaseResponse<IEnumerable<PostRetrievedDto>>> GetPostsAsync(string title, string authorname)
        {
            var posts = await _blogPostRepo.SearchPostsAsync(title, authorname);
            var result= _mapper.Map<IEnumerable<PostRetrievedDto>>(posts);
            return new BaseResponse<IEnumerable<PostRetrievedDto>> { Success = true, Message = "Posts retrieved successfully.", Result = result };
        }
        public async Task<BaseResponse<IEnumerable<PostRetrievedDto>>> GetAllPostsAsync()
        {
            var posts = await _blogPostRepo.ListAllAsync();
            if (posts == null)
            {
                return new BaseResponse<IEnumerable<PostRetrievedDto>> { Success = false, Message = "there is no matching" };
            }
            var ResultS = _mapper.Map<IEnumerable<PostRetrievedDto>>(posts);

            return new BaseResponse<IEnumerable<PostRetrievedDto>> { Success = true, Message = "all posts retrieved", Result = ResultS };
        }



    }
}
