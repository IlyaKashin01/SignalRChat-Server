using AutoMapper;
using SignalRChat.Core.Dto;
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
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, IGroupMemberRepository memberRepository)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        public async Task<int> AddPersonToGroup(MemberRequest request)
        {
            var member = _mapper.Map<GroupMember>(request);
            return await _memberRepository.CreateAsync(member);
        }

        public async Task<int> CreateGroupAsync(GroupRequest request)
        {
            var group = _mapper.Map<GroupChatRoom>(request);
            return await _groupRepository.CreateAsync(group);
        }

        public async Task<IEnumerable<GroupResponse>> GetAllGroupsAsync(int personId)
        {
            var groups = await _groupRepository.GetAllGroupAsync(personId);
            return _mapper.Map<IEnumerable<GroupResponse>>(groups);
        }
    }
}
