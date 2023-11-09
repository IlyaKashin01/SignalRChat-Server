using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SignalRChat.Core.Dto;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Core.Mapping
{
    public class ChatMapping: Profile
    {
        public ChatMapping()
        {
            CreateMap<PersonalMessageDto, PersonalMessage>();
            CreateMap<PersonalMessage, PersonalMessageDto>();
            CreateMap<GroupRequest, GroupChatRoom>();
        }
    }
}