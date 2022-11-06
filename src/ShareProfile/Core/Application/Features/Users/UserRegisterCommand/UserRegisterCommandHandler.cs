using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.UserRegisterCommand
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommandRequest, UserRegisterCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserRegisterCommandHandler(IUserRepository userRepository, IOperationClaimRepository operationClaimRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<UserRegisterCommandResponse> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
            var user = new User();
            user.FirstName = request.FirstName;
            user.Status = true;
            user.LastName = request.Lastname;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            var addedUser = await _userRepository.AddAsync(user);
            var roles = await _operationClaimRepository.GetListAsync(x => x.UserOperationClaims.Any(y => y.UserId == addedUser.Id));
            var accessToken = _tokenHelper.CreateToken(addedUser, roles.Items);
            var refreshToken = _tokenHelper.CreateRefreshToken(addedUser);
            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            return new UserRegisterCommandResponse(
                    accessToken: accessToken.Token,
                    accessTokenExpiration: accessToken.Expiration,
                    refreshToken: refreshToken.Token,
                    refreshTokenExpiration: refreshToken.Expires
                );
        }
    }
}
