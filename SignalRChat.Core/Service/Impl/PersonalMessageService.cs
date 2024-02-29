using AutoMapper;
using SignalRChat.Common.OperationResult;
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
                    dialog.CountUnreadMessages = await _personalMessageRepository.GetCountUnreadMessagesInPersonalDialogAsync(personId, dialog.Name);
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

        public async Task<OperationResult<PersonalMessageResponse>> SavePersonalMessageAsync(PersonalMessageRequest request)
        {
            var message = _mapper.Map<PersonalMessage>(request);
            var recipient = await _personRepository.GetByIdAsync(request.RecipientId); 
            if (recipient != null)
                message.Recipient = recipient;

            if (await _personalMessageRepository.CreateAsync(message) != 0)
            {
                var response = _mapper.Map<PersonalMessageResponse>(message);
                response.Id = message.Id;
                var person = await _personRepository.GetByIdAsync(request.SenderId);
                if (person != null)
                    response.SenderLogin = person.Login;
                var lastMessage = await _personalMessageRepository.GetLastPersonalMessageAsync(response.SenderId, response.RecipientId);
                if (lastMessage != null)
                {
                    _mapper.Map(lastMessage, response);
                }
                return new OperationResult<PersonalMessageResponse>(response);
            }
            return OperationResult<PersonalMessageResponse>.Fail(OperationCode.Error, "Не удалось сохранить сообщение в БД");

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

        public async Task<IEnumerable<Dialog>> SavePersonalMessageWithCreateDialogAsync(PersonalMessageRequest request)
        {
            var response = new List<Dialog>();
            var message = await SavePersonalMessageAsync(request);
            if (message.Result != null)
            {
                var persons = await _personalMessageRepository.GetUsersInPersonalDialogAsync(message.Result.SenderId, message.Result.RecipientId);

                var dialogs = _mapper.Map<IEnumerable<Dialog>>(persons);
                foreach (var dialog in dialogs)
                {
                    var lastMessage = await _personalMessageRepository.GetLastPersonalMessageAsync(message.Result.SenderId, message.Result.RecipientId);
                    if (lastMessage != null)
                    {
                        response.Add(_mapper.Map(lastMessage, dialog));
                        dialog.CountUnreadMessages = await _personalMessageRepository.GetCountUnreadMessagesInPersonalDialogAsync(message.Result.RecipientId, dialog.Name);
                    }
                }
            }
            return response;
        }
    }
}
