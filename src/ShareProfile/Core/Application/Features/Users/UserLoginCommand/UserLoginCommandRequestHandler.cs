using Application.Services.Repositories;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Users.UserLoginCommand
{
    public class UserLoginCommandRequestHandler : IRequestHandler<UserLoginCommandRequest,UserLoginCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserLoginCommandRequestHandler
            (IUserRepository userRepository, IOperationClaimRepository operationClaimRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<UserLoginCommandResponse> Handle(UserLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var usertocheck = await _userRepository.GetAsync(u => u.Email == request.Email);
            if (usertocheck != null)
            {
                if (HashingHelper.VerifyPasswordHash(request.Password, usertocheck.PasswordHash, usertocheck.PasswordSalt))
                {
                    var roles = await _operationClaimRepository.GetListAsync(x => x.UserOperationClaims.Any(y => y.UserId == usertocheck.Id));
                    var accessToken = _tokenHelper.CreateToken(usertocheck, roles.Items);
                    var refreshToken = await _refreshTokenRepository.GetAsync(x => x.UserId == usertocheck.Id);
                    if (refreshToken == null || refreshToken.Expires < DateTime.Now)
                    {
                        refreshToken = await _refreshTokenRepository.AddAsync(_tokenHelper.CreateRefreshToken(usertocheck));
                    }
                    return new UserLoginCommandResponse(
                    accessToken: accessToken.Token,
                    accessTokenExpiration: accessToken.Expiration,
                    refreshToken: refreshToken.Token,
                    refreshTokenExpiration: refreshToken.Expires
                );
                }
            }
            return null;
        }
    }//yok yok behaviour daki yere nu
}
