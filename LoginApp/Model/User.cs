using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LoginApp.Model
{
    public class User : INotifyPropertyChanged
    {
        private string _login;
        private string _password;

        public int Id { get; set; }

        public string Login
        {
            get { return _login; }

            set 
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get { return _password; }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
