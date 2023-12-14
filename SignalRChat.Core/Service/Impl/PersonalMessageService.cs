using AutoMapper;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.DTO;
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
                    _mapper.Map(lastMessage, dialog);
                }    
            }
            return response;
        }

        public async Task<IEnumerable<PersonalMessageResponse>> GetAllMessageInDialogAsync(int myId, int personId)
        {
            var messages = await _personalMessageRepository.GetAllMessagesInDialogAsync(myId, personId);
            var response = _mapper.Map<IEnumerable<PersonalMessageResponse>>(messages);

            foreach (var message in response)
            {
                var person = await _personRepository.GetByIdAsync(message.SenderId);
                if (person != null) message.SenderLogin = person.Login;
            }

            return response;
        }

        public Task<PersonalMessageResponse> GetLastPersonalMessageByIdAsync(int senderId, int recipientId)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonalMessageResponse> SavePersonalMessageAsync(PersonalMessageRequest request)
        {
            var message = _mapper.Map<PersonalMessage>(request);
            var recipient = await _personRepository.GetByIdAsync(request.RecipientId); 
            if (recipient != null)
                message.Recipient = recipient;

            await _personalMessageRepository.CreateAsync(message);
            var response = _mapper.Map<PersonalMessageResponse>(message);
            var person = await _personRepository.GetByIdAsync(request.SenderId);
            if (person != null)
                response.SenderLogin = person.Login;
            return response;
        }

        public Task<bool> UpdatePersonalMessageAsync(PersonalMessageResponse request)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PersonalMessageResponse>> ChangeStatusIncomingMessagesAsync(int senderId, int recipientId)
        {
            var messages = await _personalMessageRepository.ChangeStatusIncomingMessagesAsync(senderId, recipientId);
            var response = _mapper.Map<IEnumerable<PersonalMessageResponse>>(messages);
            foreach (var message in response)
            {
                var person = await _personRepository.GetByIdAsync(message.SenderId);
                if (person != null) message.SenderLogin = person.Login;
            }
            return response;
        }
    }
}
