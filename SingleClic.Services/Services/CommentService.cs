using AutoMapper;
using SigleClic.Data.IRepos;
using SigleClic.Data.Responses;
using SingleClic.Core.DTOs;
using SingleClic.Core.Models;
using SingleClic.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepo _commentRepo;
        private readonly IBlogPostRepo _blogPostRepo;
        private readonly IUserRepository _userRepository;

        public CommentService(IMapper mapper,ICommentRepo commentRepo,IBlogPostRepo blogPostRepo,IUserRepository userRepository)
        {
            _mapper = mapper;
            _commentRepo = commentRepo;
            _blogPostRepo = blogPostRepo;
            _userRepository = userRepository;
        }
        public async Task<BaseResponse<CommentAddedDto>> CreateComment(CreateCommentDto commentDto, string id)
        {
            var post =await _blogPostRepo.GetByIdAsync(commentDto.BlogPostId);
            var user = await _userRepository.GetByIdAsync(id);
            if(post == null)
            {
                return new BaseResponse<CommentAddedDto>
                {
                    Success = false,
                    Message = "Failed to create Comment there is no post with this id."
                };

            }
            var CommentToCreate = _mapper.Map<Comment>(commentDto);
            CommentToCreate.CreationDate = DateTime.Now;
            CommentToCreate.User = user;
            CommentToCreate.BlogPost = post;
            CommentToCreate.UserId=id;
            
            var createdComment = await _commentRepo.AddAsync(CommentToCreate);


           
            if (createdComment == null)
            {
                return new BaseResponse<CommentAddedDto>
                {
                    Success = false,
                    Message = "Failed to create Comment."
                };
            }

            var comment = _mapper.Map<CommentAddedDto>(CommentToCreate);
          
            return new BaseResponse<CommentAddedDto>
            {
                Success = true,
                Message = "Comment created successfully.",
                Result = comment
            };
        }
    

        public async Task<BaseResponse<bool>> DeleteComment(int CommentId, string userId)
        {
            var comment = await _commentRepo.GetByIdAsync(CommentId);
            if (comment == null)
            {
                return new BaseResponse<bool> { Success = false, Message = "comment not found." };
            }
            if (comment.User.Id != userId && comment.BlogPost.AuthorId != userId) //author of comment or author of the post 
            {
                return new BaseResponse<bool> { Success = false, Message = "You are not authorized to delete this comment." };
            }

            var deleted = await _commentRepo.DeleteAsync(comment);
            if (!deleted)
            {
                return new BaseResponse<bool> { Success = false, Message = "Failed to delete comment." };
            }

            return new BaseResponse<bool> { Success = true, Message = "comment deleted successfully.", Result = true };
        }

        public async Task<BaseResponse<CommentAddedDto>> UpdateComment(UpdateCommentDto dto, string userId)
        {
            var comment = await _commentRepo.GetByIdAsync(dto.CommentId);
            if (comment == null)
            {
                return new BaseResponse<CommentAddedDto>
                {
                    Success = false,
                    Message = "Comment not found."
                };
            }

            if (comment.UserId != userId)
            {
                return new BaseResponse<CommentAddedDto>
                {
                    Success = false,
                    Message = "You are not authorized to update this comment."
                };
            }

            comment.Text = dto.Text;
            var updatedComment = await _commentRepo.UpdateAsync(comment);
            if (updatedComment == null)
            {
                return new BaseResponse<CommentAddedDto>
                {
                    Success = false,
                    Message = "Failed to update comment."
                };
            }

            var commentDto = _mapper.Map<CommentAddedDto>(comment);
            return new BaseResponse<CommentAddedDto>
            {
                Success = true,
                Message = "Comment updated successfully.",
                Result = commentDto
            };
        }

    }
}
