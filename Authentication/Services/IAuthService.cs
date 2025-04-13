using Authentication.Model;

namespace Authentication.Services;

public interface IAuthService
{
    bool Validate(User user);
}