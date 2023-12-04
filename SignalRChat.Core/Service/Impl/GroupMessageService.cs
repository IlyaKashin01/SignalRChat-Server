using AutoMapper;
using SignalRChat.Core.DTO;
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
        public async Task<IEnumerable<GroupMessageDto>> GetAllGroupMessagesAsync(int groupId)
        {
            var messages = await _groupMessageRepository.GetAllMessageInGroupAsync(groupId);
            return _mapper.Map<IEnumerable<GroupMessageDto>>(messages);
        }

        public async Task<int> SaveGroupMessageAsync(GroupMessageDto request)
        {
            var group = _mapper.Map<GroupMessage>(request);
            var existingGroup = await _groupRepository.GetByIdAsync(request.GroupId);
            if(existingGroup != null) group.Group = existingGroup; 
            return await _groupMessageRepository.CreateAsync(group);
        }
    }
}
