using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Contracts.Repositories;
using Application.Features.Users.Dtos;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands.Refresh
{
    internal class RefreshCommandHandler : IRequestHandler<RefreshCommand, AuthResponse?>
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
        public async Task<AuthResponse?> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken, cancellationToken);
            if (refreshToken == null)
            {
                return null;
            }
            if(!refreshToken.IsActive)
            {
                return null;
            }
            var user = await _userManager.FindByIdAsync(refreshToken.UserId);
            if (user == null)
            {
                return null;
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            var newJwtToken = _jwtProvider.GenerateToken(user);
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
