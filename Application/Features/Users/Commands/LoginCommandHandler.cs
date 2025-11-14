using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Features.Users.Dtos;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand , AuthResponse?>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }
        public async Task<AuthResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                return null;
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if(!isPasswordValid)
            {
                return null;
            }
            var (token, expiresIn) = _jwtProvider.GenerateToken(user);
            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token,
                ExpiresIn = expiresIn
            };
        }
    }
}
