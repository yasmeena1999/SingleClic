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
    public class PostProfiles:Profile
    {
        public PostProfiles()
        {
            CreateMap<BlogPostDto,PostRetrievedDto>().ReverseMap();
            CreateMap<BlogPostDto, BlogPost>();
            CreateMap<BlogPost,PostRetrievedDto>().ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.UserName));

        }
    }
}
