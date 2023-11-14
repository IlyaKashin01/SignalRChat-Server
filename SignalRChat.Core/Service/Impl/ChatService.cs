using AutoMapper;
using SignalRChat.Core.Dto;
using SignalRChat.Core.DTO;
using SignalRChat.Core.Services.Interfaces;
using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Core.Services.Impl
{
    public class ChatService : IChatService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupMessageRepository _groupMessageRepository;
        private readonly IPersonalMessageRepository _privateMessageRepository;
        private readonly IMapper _mapper;

        public ChatService(IGroupRepository groupRepository, IMapper mapper, IGroupMessageRepository groupMessageRepository, IPersonalMessageRepository privateMessageRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _groupMessageRepository = groupMessageRepository;
            _privateMessageRepository = privateMessageRepository;
        }

        public Task<bool> DeleteMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PrivateMessageResponse>> GetMessagesBetweenUsersAsync(int user1Id, int user2Id)
        {
            var messages =  await _privateMessageRepository.GetAllMessagesInDialogAsync(user1Id, user2Id);
            return _mapper.Map<IEnumerable<PrivateMessageResponse>>(messages);
        }

        public Task<IEnumerable<PrivateMessageResponse>> GetMessagesInGroupAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person?>> GetUsersInGroupAsync(int groupId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> JoinGroupAsync(GroupRequest request)
        {
            var group = _mapper.Map<GroupChatRoom>(request);
            return await _groupRepository.CreateAsync(group);
        }

        public Task LeaveGroupAsync(int userId, string groupName)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SendMessageAsync(PersonalMessageDto request)
        {
            var newMessage = _mapper.Map<PersonalMessage>(request);
            return await _privateMessageRepository.CreateAsync(newMessage);
        }

        public async Task<int> SendMessageToGroupAsync(GroupMessageDto request)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);
            if (group is not null)
            {
                var newMessage = _mapper.Map<GroupMessage>(request);
                return await _groupMessageRepository.CreateAsync(newMessage);
            }
            return 0;
        }
    }
}
