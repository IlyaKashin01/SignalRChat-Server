using AutoMapper;
using SignalRChat.Core.DTO.Messages;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Data.Repositories.Impl;
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
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public GroupMessageService(IGroupMessageRepository groupMessageRepository, IMapper mapper, IGroupRepository groupRepository, IPersonRepository personRepository)
        {
            _groupMessageRepository = groupMessageRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
            _personRepository = personRepository;
        }
        public async Task<IEnumerable<GroupMessageResponse>> GetAllGroupMessagesAsync(int groupId, int? personId)
        {
            var messages = await _groupMessageRepository.GetAllMessageInGroupAsync(groupId, personId);
            var response = _mapper.Map<IEnumerable<GroupMessageResponse>>(messages);
            foreach (var message in response)
            {
                var personLogin = await _personRepository.GetLoginByIdAsync(message.SenderId);
                if (personLogin != null) message.SenderLogin = personLogin;
            }
            return response;
        }

        public async Task<GroupMessageResponse> SaveGroupMessageAsync(GroupMessageRequest request)
        {
            var groupMessage = _mapper.Map<GroupMessage>(request);
            
            var existingGroup = await _groupRepository.GetByIdAsync(request.GroupId);
            if(existingGroup != null) groupMessage.Group = existingGroup; 
            await _groupMessageRepository.CreateAsync(groupMessage);
            var message = _mapper.Map<GroupMessageResponse>(groupMessage);
            var person = await _personRepository.GetByIdAsync(message.SenderId);
            if (person != null) message.SenderLogin = person.Login;
            return message;
        }
        public async Task<IEnumerable<GroupMessageResponse>> ChangeStatusIncomingMessagesAsync(int groupId, int senderId)
        {
            var messages = await _groupMessageRepository.ChangeStatusIncomingMessagesAsync(groupId, senderId);
            var response = _mapper.Map<IEnumerable<GroupMessageResponse>>(messages);
            foreach (var message in response)
            {
                var personLogin = await _personRepository.GetLoginByIdAsync(message.SenderId);
                if (personLogin != null) message.SenderLogin = personLogin;
            }
            return response;
        }
    }
}
