using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using AutoMapper;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Profiles
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, ListUserDto>().ForMember(x=>x.Name,opt=>opt.MapFrom(u=>$"{u.FirstName} {u.LastName}")).ReverseMap();
            CreateMap<User, UserListModel>().ReverseMap();
        }
    }
}
