using AutoMapper;
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
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PersonResponse> FindPersonByLoginAcync(string login)
        {
            var response = await _personRepository.FindByLoginAsync(login);
            return _mapper.Map<PersonResponse>(response);
        }

        public async Task<IEnumerable<PersonResponse>> GetAllUsersAsync(int personId)
        {
            var response = await _personRepository.GetAllUsersAsync(personId);
            return _mapper.Map<IEnumerable<PersonResponse>>(response);
        }

        public async Task<IEnumerable<PersonResponse>> GetAllUsersToAddGroupAsync(int groupId, int personId)
        {
            var response = await _personRepository.GetAllUsersToAddGroupAsync(groupId, personId);
            return _mapper.Map<IEnumerable<PersonResponse>>(response);
        }

        public async Task<PersonResponse> GetPersonByIdAsync(int personId)
        {
            var person = await _personRepository.GetByIdAsync(personId);
            return _mapper.Map<PersonResponse>(person);
        }
    }
}
