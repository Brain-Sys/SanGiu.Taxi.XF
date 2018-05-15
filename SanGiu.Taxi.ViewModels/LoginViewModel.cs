using SanGiu.Taxi.Auth;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace SanGiu.Taxi.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private Timer timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
        public DateTime CurrentTime { get; set; }

        public LoginViewModel()
        {
            this.Username = "corso";
            this.Password = "macerata2";
            this.CurrentTime = DateTime.Now;

            timer = new Timer(updateTimer, null, 0, 1000);
        }

        private void updateTimer(object obj)
        {
            this.CurrentTime = DateTime.Now;
            Debug.WriteLine(this.CurrentTime);

            this.PropertyChanged?.Invoke
                (this, new PropertyChangedEventArgs("CurrentTime"));
        }

        public void CheckLogin()
        {
            var lr = Authentication.Check(this.Username, this.Password);
        }
    }
}