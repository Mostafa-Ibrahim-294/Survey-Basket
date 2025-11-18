using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Helpers;
using Domain.Entites;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using OneOf;

namespace Application.Features.Users.Commands.ResendConfirmationEmail
{
    internal class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand, OneOf<bool, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;

        public ResendConfirmationEmailCommandHandler(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }
        public async Task<OneOf<bool, Error>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                return UserErrors.UserNotFound;
            }
            if(user.EmailConfirmed)
            {
                return UserErrors.EmailAlreadyConfirmed;
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
            var message = EmailBodyHelper.GenerateEmailBody("EmailTemplate", new Dictionary<string, string>
                {
                    { "{{name}}", user.FirstName },
                    { "{{action_url}}", $"{origin}/identity/confirm-email?userId={user.Id}&code={code}" }
                });
            await _emailSender.SendEmailAsync(user.Email, "Confirm your email", message);
            return true;
        }
    }
}
