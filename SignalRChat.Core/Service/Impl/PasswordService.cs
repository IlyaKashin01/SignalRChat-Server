using SignalRChat.Common.OperationResult;
using SignalRChat.Core.DTO.Auth;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace SignalRChat.Core.Service.Impl
{
    public class PasswordService : IPasswordService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IEmailService _emailService;

        public PasswordService(IPersonRepository personRepository, IEmailService emailService)
        {
            _personRepository = personRepository;
            _emailService = emailService;
        }

        public async Task<OperationResult<bool>> ChangePasswordAsync(ChangePassRequest request)
        {
            var person = await _personRepository.FindByLoginAsync(request.Login);
            if (person != null)
            {
                var result = await _personRepository.ResetPasswordAsync(person.Id, BC.HashPassword(request.NewPassword));
                if (result) return new OperationResult<bool>(true);
                return OperationResult<bool>.Fail(OperationCode.Error, "Не удалось изменить пароль");
            }
            return OperationResult<bool>.Fail(OperationCode.Unauthorized, "Пользователь не найден");
        }

        public async Task<OperationResult<bool>> CheckResetCodeAsync(CodeRequest request)
        {
            var person = await _personRepository.FindByLoginAsync(request.Login);
            if (person != null)
            {
                if (person.ResetPassCode == request.ResetCode) return new OperationResult<bool>(true); 
                else return OperationResult<bool>.Fail(OperationCode.Error, $"Не правильный код сброса пароля");
            }
            else return OperationResult<bool>.Fail(OperationCode.Unauthorized, "Пользователь не найден");
        }

        public async Task<OperationResult<bool>> GetResetCodeAsync(string login)
        {
            var person = await _personRepository.FindByLoginAsync(login);
            if (person != null)
            {
                Random random = new Random();
                int code = random.Next(100000, 1000000);
                var result = await _personRepository.SaveResetPassCodeAsync(person.Id, code);
                if (result) {
                    var sendStatus = await _emailService.SendEmailMessageAsync(person.Email, code.ToString());
                    if (sendStatus.Success) return new OperationResult<bool>(true);
                    return OperationResult<bool>.Fail(OperationCode.Error, $"Ошибка отправки кода сброса пароля по email: {sendStatus.StackTrace}");
                }
                return OperationResult<bool>.Fail(OperationCode.Error, "Не удалось сохранить код сброса пароля");
            }
            return OperationResult<bool>.Fail(OperationCode.Unauthorized, "Пользователь не найден");
        }
    }
}
