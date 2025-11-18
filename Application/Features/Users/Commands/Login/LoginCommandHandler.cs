using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Features.Users.Dtos;
using Domain.Entites;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;

namespace Application.Features.Users.Commands.Login
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, OneOf<AuthResponse, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
        }
        public async Task<OneOf<AuthResponse, Error>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return UserErrors.InvalidCredentials;
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid || !user.EmailConfirmed)
            {
                return UserErrors.InvalidCredentials;
            }
            var (token, expiresIn) = _jwtProvider.GenerateToken(user);
            var (refreshToken, refreshTokenExpiresOn) = _jwtProvider.GenerateRefreshToken();
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiresOn,
            });
            await _userManager.UpdateAsync(user);
            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token,
                ExpiresIn = expiresIn , 
                RefreshToken = refreshToken,
                RefreshTokenExpiresOn = refreshTokenExpiresOn
            };
        }
    }
}
