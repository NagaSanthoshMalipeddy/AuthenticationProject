using Authentication.Model;
using Authentication.Provider.SignUpProvider;

namespace Authentication.Services.SignUpService
{
    public class GeneralSignUpService(ISignUpProvider provider): ISignUpService
    {
        public bool CompleteSignUp(User user)
        {
            return provider.CompleteSignUp(user);
        }
    }
}
