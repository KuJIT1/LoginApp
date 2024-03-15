using LoginApp.Model;
using LoginApp.Services;
using LoginApp.Services.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LoginApp.ViewModels
{
    public class ApplicationViewModel
    {
        private RelayCommand? _loginCommand;
        private RelayCommand? _logupCommand;

        private IServiceProvider _serviceProvider;

        public User CurrentUser { get; private set; } = new User();

        public string Tooltip  =>  "Для пароля, конечно же, нужно использовать \"PasswordBox\", но на него не работает \r\n        привязка и нужен отдельный механизм для работы с такого типа полем, поэтому я оставил так';";

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                  (_loginCommand = new RelayCommand((o) =>
                  {
                      this.DoLogin();
                  }));
            }
        }

        public RelayCommand LogupCommand
        {
            get
            {
                return _logupCommand ??
                  (_logupCommand = new RelayCommand((o) =>
                  {
                      this.DoLogup();
                  }));
            }
        }

        public ApplicationViewModel(IServiceProvider services)
        {
            _serviceProvider = services;
        }

        private void DoLogin()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userService = _serviceProvider.GetService<IUserService>();
                if (!userService.VerifyUser(CurrentUser))
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }
            }

            MessageBox.Show("Всё получилось");
        }

        private void DoLogup()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userService = _serviceProvider.GetService<IUserService>();
                try
                {
                    userService.RegisterUser(CurrentUser);
                    CurrentUser.Password = string.Empty;
                }
                catch(AlreadyExistsException)
                {
                    MessageBox.Show("Этот логин занят");
                    return;
                }

                MessageBox.Show("Всё получилось");
            }
        }
    }
}
