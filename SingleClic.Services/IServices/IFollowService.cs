using SigleClic.Data.Responses;
using SingleClic.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Services.IServices
{
    public interface IFollowService
    {
        Task<BaseResponse<string>> FollowUser(string followerId, string followeeId);
        Task<BaseResponse<string>> UnfollowUser(string followerId, string followeeId);
        Task<BaseResponse<userfollowlistDto>> GetUserWithFollowersAndFollowingsAsync(string currentUserId);
    }
}
