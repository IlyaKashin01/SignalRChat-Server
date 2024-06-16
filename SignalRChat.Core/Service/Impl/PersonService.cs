using AutoMapper;
using Microsoft.AspNetCore.Http;
using SignalRChat.Common.Auth;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Dto.Auth;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Data.Repositories.Interfaces;

namespace SignalRChat.Core.Service.Impl
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IDecodingJWT _decodingJwt;

        public PersonService(IPersonRepository personRepository, IMapper mapper, IDecodingJWT decodingJwt)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _decodingJwt = decodingJwt;
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

        public async Task<OperationResult<string>> GetLoginByIdAsync(int personId)
        {
            var login = await _personRepository.GetLoginByIdAsync(personId);
            if (!string.IsNullOrEmpty(login))
                return new OperationResult<string>(login);
            return OperationResult<string>.Fail(OperationCode.EntityWasNotFound, "Пользователя не существует");
        }

        public async Task<OperationResult<bool>> AddAvatarAsync(IFormFile avatar, string token)
        {
            var personId = _decodingJwt.getJWTTokenClaim(token, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid");
            if (personId != null)
            {
                byte[] avatarBytes;
                using (var memory = new MemoryStream())
                {
                    avatar.CopyTo(memory);
                    avatarBytes = memory.ToArray();
                }
                var result = await _personRepository.AddAvatarAsync(Int32.Parse(personId), Convert.ToBase64String(avatarBytes));
                if (!result)
                {
                    return OperationResult<bool>.Fail(OperationCode.EntityWasNotFound, "Не удалось добавить аватар");
                }
                return new OperationResult<bool>(result);
            }
            return OperationResult<bool>.Fail(OperationCode.EntityWasNotFound, "Пользователь не найден");
        }
    }
}
