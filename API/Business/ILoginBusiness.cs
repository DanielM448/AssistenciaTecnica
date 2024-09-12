using API.Data.VO;

namespace API.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(LoginVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
