using LoginApp.Model;
using LoginApp.Services.Exceptions;
using LoginApp.ViewModels.Infrastructure;

namespace LoginApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext? _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(UserContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }


        public void RegisterUser(User user)
        {
            var savedUser = _context.Users.FirstOrDefault(u => u.Login == user.Login);
            if (savedUser != null) 
            {
                throw new AlreadyExistsException();
            }

            user.Password = _passwordHasher.HashPassword(user.Password);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool VerifyUser(User user)
        {
            var savedUser = _context.Users.FirstOrDefault(u => u.Login == user.Login);
            if (savedUser == null)
            {
                return false;
            }

            return _passwordHasher.VerifyHashedPassword(savedUser.Password, user.Password ?? string.Empty);
        }
    }
}
