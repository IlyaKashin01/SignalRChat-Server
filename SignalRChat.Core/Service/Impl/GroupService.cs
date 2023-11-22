using AutoMapper;
using SignalRChat.Core.Dto;
using SignalRChat.Core.Dto.Auth;
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
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupMemberRepository _memberRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, IGroupMemberRepository memberRepository, IPersonRepository personRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _memberRepository = memberRepository;
            _personRepository = personRepository;
        }

        public async Task<int> AddPersonToGroupAsync(MemberRequest request)
        {
            var member = _mapper.Map<GroupMember>(request);
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person != null) { 
                member.Person = person; 
                member.AddedDate = DateTime.UtcNow; 
            }
            return await _memberRepository.CreateAsync(member);
        }

        public async Task<int> CreateGroupAsync(GroupRequest request)
        {
            var group = _mapper.Map<GroupChatRoom>(request);
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if(person != null)  group.Person = person; 
            return await _groupRepository.CreateAsync(group);
        }

        public async Task<IEnumerable<GroupResponse>> GetAllGroupsAsync(int personId)
        {
            var groups = await _groupRepository.GetAllGroupAsync(personId);
            return _mapper.Map<IEnumerable<GroupResponse>>(groups);
        }

        public async Task<GroupResponse> GetGroupByIdAsync(int groupId)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);
            return _mapper.Map<GroupResponse>(group);
        }

        public async Task<IEnumerable<MemberInGroup>> GetAllMembersInGroupAsync(int groupId)
        {
            var members = await _memberRepository.GetAllGroupMembersAsync(groupId);
            
            var groupMembers = new List<MemberInGroup>();
            foreach (var member in members)
            {
                var tempMember = await _personRepository.GetByIdAsync(member.PersonId);
                var addedByPerson = await _personRepository.GetByIdAsync(member.AddedByPerson);
                if(tempMember != null && addedByPerson != null)
                {
                    var memberResponse = _mapper.Map<MemberInGroup>(member);
                    memberResponse.MemberLogin = tempMember.Login;
                    memberResponse.AddedByPersonLogin = addedByPerson.Login;
                    groupMembers.Add(memberResponse);
                }
            }
            return groupMembers;
        }

        public async Task<string> GetCreatorLoginAsync(int groupId)
        {
            var creatorId = await _groupRepository.GetCreatorIdAsync(groupId);
            if(creatorId != 0)
            {
                var person = await _personRepository.GetByIdAsync(creatorId);
                if(person != null)
                    return person.Login;
            }
            return "";
        }
    }
}
