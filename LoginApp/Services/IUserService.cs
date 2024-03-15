using LoginApp.Model;

namespace LoginApp.Services
{
    public interface IUserService
    {
        bool VerifyUser(User user);

        void RegisterUser(User user);
    }
}
