using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SignalRChat.Common.Email;
using SignalRChat.Common.OperationResult;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Domain.Entities;
using System.Collections.Concurrent;

namespace SignalRChat.Core.Service.Impl
{
    public class EmailService: IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async Task<OperationResult<bool>> SendEmailMessageAsync(string email, string message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("WarehoseMaster", _emailOptions.Email));
            emailMessage.To.Add(new MailboxAddress("", email));

            emailMessage.Subject = "Сброс пароля в приложении WarehoseMaster";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Ваш код для сброса пароля в приложении WarehoseMaster {message}. Никому его не сообщайте."
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync(_emailOptions.Email, _emailOptions.Password);
                if (client.IsAuthenticated) await client.SendAsync(emailMessage);
                else return OperationResult<bool>.Fail(OperationCode.Unauthorized, "Ошибка авторизации Email");
                await client.DisconnectAsync(true);
            }
            return new OperationResult<bool>(true);
        }
    }
}
