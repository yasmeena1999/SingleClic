using SigleClic.Data.Responses;
using SingleClic.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Services.IServices
{
    public interface ICommentService
    {
        Task<BaseResponse<CommentAddedDto>>CreateComment(CreateCommentDto dto, string id);
        Task<BaseResponse<bool>> DeleteComment(int CommentId, string userId);
        Task<BaseResponse<CommentAddedDto>> UpdateComment(UpdateCommentDto dto, string id );

    }
}
