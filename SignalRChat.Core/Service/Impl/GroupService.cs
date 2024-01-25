using AutoMapper;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO;
using SignalRChat.Core.DTO.Group;
using SignalRChat.Core.DTO.Members;
using SignalRChat.Core.DTO.Messages;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Data.Repositories.Impl;
using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignalRChat.Core.Service.Impl
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupMemberRepository _memberRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IGroupMessageRepository _groupMessageRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, IGroupMemberRepository memberRepository, IPersonRepository personRepository, IGroupMessageRepository groupMessageRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _memberRepository = memberRepository;
            _personRepository = personRepository;
            _groupMessageRepository = groupMessageRepository;
        }

        public async Task<OperationResult<GroupMessageResponse>> AddPersonToGroupAsync(MemberRequest request)
        {
            var member = _mapper.Map<GroupMember>(request);
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person != null)
            {
                member.Person = person;
                if (await _memberRepository.CreateAsync(member) != 0)
                {
                    var addedPersonLogin = await _personRepository.GetLoginByIdAsync(request.AddedByPerson);
                    if (addedPersonLogin != "")
                    {
                        var existingGroup = await _groupRepository.GetByIdAsync(request.GroupId);
                        if (existingGroup != null)
                        {
                            var messageRequest = new GroupMessage { Content = $"{addedPersonLogin} добавил пользователя {person.Login} в группу", GroupId = request.GroupId, Group = existingGroup, SenderId = 0 };
                            if (await _groupMessageRepository.CreateAsync(messageRequest) != 0)
                                return new OperationResult<GroupMessageResponse>(_mapper.Map<GroupMessageResponse>(messageRequest));
                        }
                        return OperationResult<GroupMessageResponse>.Fail(OperationCode.Error, "Ошибка при сохранении сообщения о добавлении участника");
                    }
                }
                return OperationResult<GroupMessageResponse>.Fail(OperationCode.Error, "Ошибка при добавлении участника");
            }
            return OperationResult<GroupMessageResponse>.Fail(OperationCode.Error, "Попытка добавить не существующего пользователя в группу");
        }

        public async Task<OperationResult<Dialog>> CreateGroupAsync(GroupRequest request)
        {
            var group = _mapper.Map<GroupChatRoom>(request);
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if(person != null)  group.Person = person; 
            if( await _groupRepository.CreateAsync(group) != 0)
            {
                var response = _mapper.Map<Dialog>(group);
                var lastMessage = await _groupRepository.GetLastGroupMessageAsync(group.Id);
                if (lastMessage != null)
                {
                    _mapper.Map(lastMessage, group);
                }
                return new OperationResult<Dialog>(response);
            }
            return OperationResult<Dialog>.Fail(OperationCode.Error, "Ошибка при создании группы в БД");
        }

        public async Task<IEnumerable<Dialog>> GetAllGroupsAsync(int personId)
        {
            var groups = await _groupRepository.GetAllGroupAsync(personId);
            var response = _mapper.Map<IEnumerable<Dialog>>(groups);
            foreach (var group in response)
            {
                var lastMessage = await _groupRepository.GetLastGroupMessageAsync(group.Id);
                if (lastMessage != null)
                {
                    _mapper.Map(lastMessage, group);
                    group.CountMembers = await _groupRepository.GetCountMembersInGroupAsync(group.Id);
                    group.CreatorLogin = await _groupRepository.GetCreatorLoginAsync(group.Id);
                    group.CountUnreadMessages = await _groupRepository.GetCountUnreadMessagesInGroupAsync(group.Id);
                }
            }
            return response;
        }

        public async Task<OperationResult<Dialog>> GetGroupDialogByIdAsync(int groupId)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);
            if(group != null)
            {
                var response = _mapper.Map<Dialog>(group);
                var lastMessage = await _groupRepository.GetLastGroupMessageAsync(group.Id);
                if (lastMessage != null)
                {
                    _mapper.Map(lastMessage, response);
                    response.CountMembers = await _groupRepository.GetCountMembersInGroupAsync(group.Id);
                    response.CreatorLogin = await _groupRepository.GetCreatorLoginAsync(group.Id);
                    response.CountUnreadMessages = await _groupRepository.GetCountUnreadMessagesInGroupAsync(group.Id);
                }
                return new OperationResult<Dialog>(response);
            }
            
            return OperationResult<Dialog>.Fail(OperationCode.EntityWasNotFound, "Группа не найдена");
        }

        public async Task<GroupResponse> GetGroupByIdAsync(int groupId)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);
            return _mapper.Map<GroupResponse>(group);
        }

        public async Task<OperationResult<MemberResponse>> GetAllMembersInGroupAsync(int groupId)
        {
            var members = await _memberRepository.GetAllGroupMembersAsync(groupId);
            if (members != null)
            {
                var groupMembers = new List<MemberInGroup>();
                foreach (var member in members)
                {
                    var memberLogin = await _personRepository.GetLoginByIdAsync(member.PersonId);
                    var addedByPersonLogin = await _personRepository.GetLoginByIdAsync(member.AddedByPerson);
                    if (memberLogin != null && addedByPersonLogin != null)
                    {
                        var memberResponse = _mapper.Map<MemberInGroup>(member);
                        memberResponse.MemberLogin = memberLogin;
                        memberResponse.AddedByPersonLogin = addedByPersonLogin;
                        groupMembers.Add(memberResponse);
                    }
                }
                var creatorLogin = await _groupRepository.GetCreatorLoginAsync(groupId);
                if (creatorLogin != null)
                {
                    var response = new MemberResponse { CreatorLogin = creatorLogin, GroupMembers = groupMembers };
                    return new OperationResult<MemberResponse>(response);
                }
                else
                    OperationResult<MemberResponse>.Fail(OperationCode.EntityWasNotFound, "Группа не найдена");
            }
            return OperationResult<MemberResponse>.Fail(OperationCode.EntityWasNotFound, "Участники не найдены");
        }

        public async Task<bool> LeaveGroupAsync(int groupId, int personId)
        {
            return await _groupRepository.LeaveGroupAsync(groupId, personId);
        }

        public async Task<bool> ReturnToGroupAsync(int groupId, int personId)
        {
            return await _groupRepository.ReturnToGroupAsync(groupId, personId);
        }
    }
}
