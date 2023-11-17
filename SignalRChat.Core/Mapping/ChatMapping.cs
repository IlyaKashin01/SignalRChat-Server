using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SignalRChat.Core.Dto;
using SignalRChat.Core.DTO;
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
            CreateMap<GroupChatRoom, GroupResponse>();

            CreateMap<GroupMessageDto, GroupMessage>();
            CreateMap<GroupMessage, GroupMessageDto>();

            CreateMap<MemberRequest, GroupMember>();
            CreateMap<GroupMember, MemberResponse>();
        }
    }
}