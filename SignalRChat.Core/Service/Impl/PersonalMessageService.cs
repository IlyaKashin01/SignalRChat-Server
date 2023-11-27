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
        private readonly IPersonRepository _personRepository;

        public PersonalMessageService(IPersonalMessageRepository personalMessageRepository, IMapper mapper, IPersonRepository personRepository)
        {
            _personalMessageRepository = personalMessageRepository;
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<PersonResponse>> GetAllDialogsAsync(int personId)
        {
            var persons = await _personalMessageRepository.GetAllPersonalDialogsAsync(personId);
            return _mapper.Map<IEnumerable<PersonResponse>>(persons);
        }

        public async Task<IEnumerable<PersonalMessageDto>> GetAllMessageInDialogAsync(int myId, int personId)
        {
            var messages = await _personalMessageRepository.GetAllMessagesInDialogAsync(myId, personId);
            var response = _mapper.Map<IEnumerable<PersonalMessageDto>>(messages);

            foreach (var message in response)
            {
                var person = await _personRepository.GetByIdAsync(message.SenderId);
                if (person != null) message.SenderLogin = person.Login;
            }

            return response;
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
