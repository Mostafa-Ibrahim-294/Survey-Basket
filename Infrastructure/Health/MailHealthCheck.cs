using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Infrastructure.Health
{
    public class MailHealthCheck : IHealthCheck
    {
        private readonly IOptions<MailOptions> _mailOptions;
        public MailHealthCheck(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailOptions.Value.Host, _mailOptions.Value.Port, SecureSocketOptions.StartTls, cancellationToken);
                await smtp.AuthenticateAsync(_mailOptions.Value.Mail, _mailOptions.Value.Password, cancellationToken);
                await smtp.DisconnectAsync(true, cancellationToken);
                return HealthCheckResult.Healthy("Mail server is reachable");
            }
            catch
            {
                return HealthCheckResult.Unhealthy("Cannot connect to mail server");
            }
            

        }
    }
}
