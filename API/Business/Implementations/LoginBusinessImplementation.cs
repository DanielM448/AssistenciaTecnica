using API.Configurations;
using API.Data.VO;
using API.Repositories.User;
using API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Business.Implementations
{
    public class LoginBusinessImplementation : ILoginBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _configuration;
        private IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public LoginBusinessImplementation(TokenConfiguration configuration, IUserRepository repository, ITokenService tokenService)
        {
            _configuration = configuration;
            _repository = repository;
            _tokenService = tokenService;
        }

        public TokenVO ValidateCredentials(LoginVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return null;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
            };
            claims.AddRange(user.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.RoleName)));

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();


            user.RefreshToken = refreshToken;
            var days = _configuration.DaysToExpiry;

            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry);

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }
        public TokenVO ValidateCredentials(TokenVO token)
        {
            var accessToken = token.AccesToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var username = principal.Identity.Name;

            var user = _repository.ValidateCredentials(username);

            if (user == null ||
                user.RefreshToken != refreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now) return null;            

            var claims = principal.Claims.ToList();

            var testeRoels = user.UserRoles.Select(ur => new Claim(ClaimTypes.Role, ur.Role.RoleName));
            // Adicione as permissões como claims
            claims.AddRange(testeRoels);

            accessToken = _tokenService.GenerateAccessToken(claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }
        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}
