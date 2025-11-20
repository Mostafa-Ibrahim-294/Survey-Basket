using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Contracts.Repositories;
using Application.Features.Users.Dtos;
using Domain.Entites;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;

namespace Application.Features.Users.Commands.Refresh
{
    internal class RefreshCommandHandler : IRequestHandler<RefreshCommand, OneOf<AuthResponse , Error>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        public RefreshCommandHandler(IRefreshTokenRepository refreshTokenRepository, IJwtProvider jwtProvider, UserManager<ApplicationUser> userManager)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtProvider = jwtProvider;
            _userManager = userManager;
        }
        public async Task<OneOf<AuthResponse , Error>> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken, cancellationToken);
            if (refreshToken == null || !refreshToken.IsActive)
            {
                return UserErrors.InvalidRefreshToken;
            }
            var user = await _userManager.FindByIdAsync(refreshToken.UserId);
            if (user == null)
            {
                return UserErrors.UserNotFound;
            }
            if (user.IsDisabled)
            {
                return UserErrors.UserDisabled;
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            var roles = await _userManager.GetRolesAsync(user);
            var newJwtToken = _jwtProvider.GenerateToken(user, roles);
            var newRefreshToken = _jwtProvider.GenerateRefreshToken();
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken.token,
                ExpiresOn = newRefreshToken.expiresOn,
            });
            await _userManager.UpdateAsync(user);
            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = newJwtToken.token,
                ExpiresIn = newJwtToken.expiresIn,
                RefreshToken = newRefreshToken.token,
                RefreshTokenExpiresOn = newRefreshToken.expiresOn,
            };

        }
    }
}
