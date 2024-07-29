using AutoMapper;
using SingleClic.Core.DTOs;
using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Core.MappingProfiles
{
    public class CommentProfiles:Profile
    {
        public CommentProfiles() {
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment,CommentAddedDto>().ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.AuthorEmail, opt => opt.MapFrom(src => src.User.Email));
        }
    }
}
