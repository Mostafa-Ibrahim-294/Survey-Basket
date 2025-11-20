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
        private readonly SignInManager<ApplicationUser> _signInManager;
        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider
            , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _signInManager = signInManager;
        }
        public async Task<OneOf<AuthResponse, Error>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return UserErrors.UserNotFound;
            }
            if (user.IsDisabled)
            {
                return UserErrors.UserDisabled;
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var (token, expiresIn) = _jwtProvider.GenerateToken(user, roles);
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
                    ExpiresIn = expiresIn,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiresOn = refreshTokenExpiresOn
                };
            }
            if(result.IsLockedOut)
            {
                return UserErrors.LockedUser;
            }
            if (result.IsNotAllowed)
            {
                return UserErrors.NotConfirmedEmail;
            }
            return UserErrors.InvalidCredentials;
        }
    }
}
