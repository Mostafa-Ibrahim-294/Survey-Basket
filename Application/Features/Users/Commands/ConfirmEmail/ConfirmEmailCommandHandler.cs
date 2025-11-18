using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Constants;
using Domain.Entites;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using OneOf;

namespace Application.Features.Users.Commands.ConfirmEmail
{
    internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, OneOf<bool, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<OneOf<bool, Error>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return UserErrors.UserNotFound;
            }
            if (user.EmailConfirmed)
            {
                return UserErrors.EmailAlreadyConfirmed;
            }
            var code = request.Code;
            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            }
            catch
            {
                return false;
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Member);
            }
            return result.Succeeded;
        }
    }
}
