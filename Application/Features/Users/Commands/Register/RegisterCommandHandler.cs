using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Helpers;
using AutoMapper;
using Domain.Entites;
using Domain.Errors;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using OneOf;

namespace Application.Features.Users.Commands.Register
{

    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, OneOf<bool, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper,
            IEmailSender emailSender , IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<OneOf<bool, Error>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var isUniqueEmail = await _userManager.FindByEmailAsync(request.Email);

            if (isUniqueEmail != null)
            {
                return UserErrors.EmailAlreadyExists;
            }
            var user = _mapper.Map<ApplicationUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                // in production save front end url in appsettings
                var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

                var message = EmailBodyHelper.GenerateEmailBody("EmailTemplate", new Dictionary<string, string>
                {
                    { "{{name}}", user.FirstName },
                    // TODO: change the url according to front-end route
                    { "{{action_url}}", $"{origin}/identity/confirm-email?userId={user.Id}&code={code}" }
                });
                BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email, "Confirm your email", message));
                return result.Succeeded;

            }
            return false;
        }
    }
}
