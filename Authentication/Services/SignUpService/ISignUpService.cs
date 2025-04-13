using Authentication.Model;

namespace Authentication.Services.SignUpService
{
    public interface ISignUpService
    {
        bool CompleteSignUp(User user);
    }
}
