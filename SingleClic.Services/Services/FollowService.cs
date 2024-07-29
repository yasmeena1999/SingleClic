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
    public class FollowService: IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FollowService(IFollowRepository followRepository,IUserRepository userRepository,IMapper mapper)
        {
            _followRepository = followRepository;
           _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<string>> FollowUser(string followerId, string followeeId)
        {
            if (followerId == followeeId)
            {
                return new BaseResponse<string>
                {
                    Success = false,
                    Message = "You cannot follow yourself."
                };
            }
            var checkonuser= await _userRepository.GetByIdAsync(followeeId);
            if (checkonuser == null)
            {
                return new BaseResponse<string>
                {
                    Success = false,
                    Message = "there is no user with this id."
                };
            }

            var existingFollow = await _followRepository.GetFollowAsync(followerId, followeeId);
            if (existingFollow != null)
            {
                return new BaseResponse<string>
                {
                    Success = false,
                    Message = "You are already following this user."
                };
            }

            var follow = new Follow
            {
                FollowerId = followerId,
                FolloweeId = followeeId
            };

            await _followRepository.AddAsync(follow);

            return new BaseResponse<string>
            {
                Success = true,
                Message = "You are now following this user."
            };
        }

        public async Task<BaseResponse<string>> UnfollowUser(string followerId, string followeeId)
        {
            var follow = await _followRepository.GetFollowAsync(followerId, followeeId);
            if (follow == null)
            {
                return new BaseResponse<string>
                {
                    Success = false,
                    Message = "You are not following this user."
                };
            }
            var checkonuser = await _userRepository.GetByIdAsync(followeeId);
            if (checkonuser == null)
            {
                return new BaseResponse<string>
                {
                    Success = false,
                    Message = "there is no user with this id."
                };
            }

            await _followRepository.DeleteAsync(follow);

            return new BaseResponse<string>
            {
                Success = true,
                Message = "You have unfollowed this user."
            };
        }
        public async Task<BaseResponse<userfollowlistDto>> GetUserWithFollowersAndFollowingsAsync(string currentUserId)
        {
            var user = await _userRepository.GetByIdAsync(currentUserId);
            if (user == null)
            {
                return new BaseResponse<userfollowlistDto>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var followers = await _followRepository.GetFollowersByUserIdAsync(currentUserId);
            var followings = await _followRepository.GetFollowingsByUserIdAsync(currentUserId);

            var dto = new userfollowlistDto
            {
                Name = user.UserName,
                Followers = _mapper.Map<List<string>>(followers.Select(f => f.UserName).ToList()),
                Followings = _mapper.Map<List<string>>(followings.Select(f => f.UserName).ToList())
            };

            return new BaseResponse<userfollowlistDto>
            {
                Success = true,
                Message = "User followers and followings retrieved successfully.",
                Result = dto
            };
        }

    }
}
