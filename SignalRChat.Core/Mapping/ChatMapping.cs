using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO;
using SignalRChat.Core.DTO.Group;
using SignalRChat.Core.DTO.Members;
using SignalRChat.Core.DTO.Messages;
using SignalRChat.Domain.Entities;

namespace SignalRChat.Core.Mapping
{
    public class ChatMapping: Profile
    {
        public ChatMapping()
        {
            CreateMap<PersonalMessageRequest, PersonalMessage>()
                .ForMember(dest => dest.IsCheck, opt => opt.MapFrom(src => false));
                //.ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<PersonalMessage, PersonalMessageResponse>();

            CreateMap<Person, Dialog>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Login));

            CreateMap<PersonalMessage, Dialog>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt))
            .ForMember(dest => dest.IsGroup, opt => opt.MapFrom(src => false));

            CreateMap<GroupChatRoom, Dialog>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.IsGroup, opt => opt.MapFrom(src => true));

            CreateMap<GroupMessage, Dialog>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt));

            CreateMap<GroupRequest, GroupChatRoom>();
            CreateMap<GroupChatRoom, GroupResponse>();

            CreateMap<GroupMessageRequest, GroupMessage>()
                .ForMember(dest => dest.IsCheck, opt => opt.MapFrom(src => false));
                //.ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<GroupMessage, GroupMessageResponse>();

            CreateMap<MemberRequest, GroupMember>();
                //.ForMember(dest => dest.AddedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<GroupMember, MemberInGroup>();
        }
    }
}