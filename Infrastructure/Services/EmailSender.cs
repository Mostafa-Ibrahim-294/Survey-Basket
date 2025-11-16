using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<MailOptions> _options;
        public EmailSender(IOptions<MailOptions> options)
        {
            _options = options;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage()
            {
               Sender = MailboxAddress.Parse(_options.Value.Mail) ,
               Subject = subject
            };
            message.To.Add(MailboxAddress.Parse(email));
            var builder = new BodyBuilder()
            {
                HtmlBody = htmlMessage
            };
            message.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_options.Value.Host, _options.Value.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_options.Value.Mail, _options.Value.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
