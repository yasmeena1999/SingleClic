using AutoMapper;
using SingleClic.Core.DTOs;
using SingleClic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SingleClic.Core.MappingProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile() {
            CreateMap<RegisterRequestDto, User>();
               
        }
    }
}
