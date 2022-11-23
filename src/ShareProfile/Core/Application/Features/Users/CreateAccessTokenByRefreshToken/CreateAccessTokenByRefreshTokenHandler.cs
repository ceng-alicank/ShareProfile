using Application.Interfaces.Repositories;
using Application.Wrappers;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.CreateAccessTokenByRefreshToken
{
    public class CreateAccessTokenByRefreshTokenHandler : IRequestHandler<CreateAccessTokenByRefreshTokenRequest, Response<CreateAccessTokenByRefreshTokenResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public CreateAccessTokenByRefreshTokenHandler(IUserRepository userRepository, IOperationClaimRepository operationClaimRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Response<CreateAccessTokenByRefreshTokenResponse>> Handle(CreateAccessTokenByRefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var existRefreshToken = await _refreshTokenRepository.GetAsync(x => x.Token == request.Token);
            if (existRefreshToken != null)
            {
                var user = await _userRepository.GetAsync(u => u.Id == existRefreshToken.UserId);
                if (user != null)
                {
                    if (existRefreshToken.Expires > DateTime.Now)
                    {
                        var roles = await _operationClaimRepository.GetListAsync(x => x.UserOperationClaims.Any(y => y.UserId == user.Id));
                        var accessToken = _tokenHelper.CreateToken(user, roles.Items);
                        var responseData = new CreateAccessTokenByRefreshTokenResponse(
                                    accessToken: accessToken.Token,
                                    accessTokenExpiration: accessToken.Expiration
                                );
                        return new Response<CreateAccessTokenByRefreshTokenResponse>(responseData);
                    }
                }
            }
            return new Response<CreateAccessTokenByRefreshTokenResponse>("Token is not valid for any users");
        }
    }
}
