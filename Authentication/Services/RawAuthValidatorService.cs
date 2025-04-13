using Authentication.Model;
using Authentication.Provider;

namespace Authentication.Services
{
    public class RawAuthValidatorService(IAuthProvider provider): IAuthService
    {
        public bool Validate(User user)
        {
            return provider.FetchCredential(user.MailId) == user.Password;
        }
    }
}
