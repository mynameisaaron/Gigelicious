using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Gigelicious.Controllers.Api;
using Gigelicious.Dto;
using Gigelicious.Models;

namespace Gigelicious.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Notification, NotificationDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<ApplicationUser, UserDto>();
            
        }
    }
}