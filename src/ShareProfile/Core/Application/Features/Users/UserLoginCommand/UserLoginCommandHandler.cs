using Application.Services.Repositories;
using Application.Wrappers;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Users.UserLoginCommand
{
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommandRequest,Response<UserLoginCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserLoginCommandHandler
            (IUserRepository userRepository, IOperationClaimRepository operationClaimRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<Response<UserLoginCommandResponse>> Handle(UserLoginCommandRequest request, CancellationToken cancellationToken)
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
                    var responseData =  new UserLoginCommandResponse(
                    accessToken: accessToken.Token,
                    accessTokenExpiration: accessToken.Expiration,
                    refreshToken: refreshToken.Token,
                    refreshTokenExpiration: refreshToken.Expires
                    );
                    return new Response<UserLoginCommandResponse>(responseData);
                }
            }
            return new  Response<UserLoginCommandResponse>("Somethings have gone wrong");
        }
    }
}
