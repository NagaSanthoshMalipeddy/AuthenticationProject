using Authentication.Model;

namespace Authentication.Provider.SignUpProvider
{
    public interface ISignUpProvider
    {
        bool CompleteSignUp(User user);
    }
}
