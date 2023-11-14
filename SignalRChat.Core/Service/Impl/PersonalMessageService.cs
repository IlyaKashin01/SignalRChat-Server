using AutoMapper;
using SignalRChat.Core.Dto;
using SignalRChat.Core.Dto.Auth;
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
    public class PersonalMessageService : IPersonalMessageService
    {
        private readonly IMapper _mapper;
        private readonly IPersonalMessageRepository _personalMessageRepository;

        public PersonalMessageService(IPersonalMessageRepository personalMessageRepository, IMapper mapper)
        {
            _personalMessageRepository = personalMessageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonResponse>> GetAllDialogsAsync(int personId)
        {
            var persons = await _personalMessageRepository.GetAllPersonalDialogsAsync(personId);
            return _mapper.Map<IEnumerable<PersonResponse>>(persons);
        }

        public async Task<IEnumerable<PersonalMessageDto>> GetAllMessageInDialogAsync(int myId, int personId)
        {
            var messages = await _personalMessageRepository.GetAllMessagesInDialogAsync(myId, personId);
            return _mapper.Map<IEnumerable<PersonalMessageDto>>(messages);
        }

        public async Task<int> SavePersonalMessageAsync(PersonalMessageDto request)
        {
            var person = _mapper.Map<PersonalMessage>(request);
            return await _personalMessageRepository.CreateAsync(person);
        }

        public Task<bool> UpdatePersonalMessageAsync(PersonalMessageDto request)
        {
            throw new NotImplementedException();
        }
    }
}
