using AutoMapper;
using SignalRChat.Core.DTO.Messages;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Impl
{
    public class GroupMessageService : IGroupMessageService
    {
        private readonly IGroupMessageRepository _groupMessageRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        public GroupMessageService(IGroupMessageRepository groupMessageRepository, IMapper mapper, IGroupRepository groupRepository)
        {
            _groupMessageRepository = groupMessageRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }
        public async Task<IEnumerable<GroupMessageResponse>> GetAllGroupMessagesAsync(int groupId)
        {
            var messages = await _groupMessageRepository.GetAllMessageInGroupAsync(groupId);
            return _mapper.Map<IEnumerable<GroupMessageResponse>>(messages);
        }

        public async Task<GroupMessageResponse> SaveGroupMessageAsync(GroupMessageRequest request)
        {
            var groupMessage = _mapper.Map<GroupMessage>(request);
            
            var existingGroup = await _groupRepository.GetByIdAsync(request.GroupId);
            if(existingGroup != null) groupMessage.Group = existingGroup; 
            await _groupMessageRepository.CreateAsync(groupMessage);
            return _mapper.Map<GroupMessageResponse>(groupMessage);
        }
    }
}
