using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace Application.Features.Users.Commands.ResendConfirmationEmail
{
    internal class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand , bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ResendConfirmationEmailCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null || user.EmailConfirmed)
            {
                return false;
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            return true;
        }
    }
}
