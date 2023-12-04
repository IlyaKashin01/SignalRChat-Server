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

        public async Task<IEnumerable<Dialog>> GetAllDialogsAsync(int personId)
        {
            var persons = await _personalMessageRepository.GetAllPersonalDialogsAsync(personId);
            var response = _mapper.Map<IEnumerable<Dialog>>(persons);
            foreach (var dialog in response)
            {
                var lastMessage = await _personalMessageRepository.GetLastPersonalMessageAsync(dialog.Id, personId); 
                if (lastMessage != null)
                {
                    dialog.LastMessage = lastMessage.Content;
                    dialog.DateTime = lastMessage.SentAt;
                    dialog.IsCheck = lastMessage.IsCheck;
                    dialog.IsGroup = false;
                }    
            }
            return response;
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

        public Task<PersonalMessageDto> GetLastPersonalMessageByIdAsync(int senderId, int recipientId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SavePersonalMessageAsync(PersonalMessageDto request)
        {
            var message = _mapper.Map<PersonalMessage>(request);
            
            var recipient = await _personRepository.GetByIdAsync(request.RecipientId); 
            if (recipient != null)
                message.Recipient = recipient;

            return await _personalMessageRepository.CreateAsync(message);
        }

        public Task<bool> UpdatePersonalMessageAsync(PersonalMessageDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PersonalMessageDto>> ChangeStatusIncomingMessagesAsync(int senderId, int recipientId)
        {
            var messages = await _personalMessageRepository.ChangeStatusIncomingMessagesAsync(senderId, recipientId);
            var response = _mapper.Map<IEnumerable<PersonalMessageDto>>(messages);
            foreach (var message in response)
            {
                var person = await _personRepository.GetByIdAsync(message.SenderId);
                if (person != null) message.SenderLogin = person.Login;
            }
            return response;
        }
    }
}
