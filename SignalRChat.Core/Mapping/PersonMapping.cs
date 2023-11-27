using AutoMapper;
using SignalRChat.Core.Dto;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Mapping
{
    public class PersonMapping: Profile
    {
        public PersonMapping()
        {
            CreateMap<AuthRequest, Person>();
            CreateMap<SignupRequest, Person>();

            CreateMap<Person, AuthResponse>();
            CreateMap<Person, PersonResponse>();

            CreateMap<PersonResponse, PersonalMessageDto>()
                .ForMember(dest => dest.SenderLogin, opt => opt.MapFrom(src => src.Login));
        }
    }
}
