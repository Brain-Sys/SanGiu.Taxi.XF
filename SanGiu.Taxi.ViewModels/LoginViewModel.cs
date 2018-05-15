using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using SanGiu.Taxi.Auth;
using SanGiu.Taxi.ViewModels.Messages;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace SanGiu.Taxi.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private Timer timer;

        public string Username { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }

        private DateTime currentTime;
        public DateTime CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                base.RaisePropertyChanged();
            }
        }

        private bool? error;
        public bool? Error
        {
            get { return error; }
            set
            {
                error = value;
                base.RaisePropertyChanged();
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value;
                base.RaisePropertyChanged();
            }
        }

        public LoginViewModel()
        {
            this.Error = false;
            this.Username = "corso";
            this.Password = "macerata2";
            this.CurrentTime = DateTime.Now;

            timer = new Timer(updateTimer, null, 0, 1000);
        }

        private void updateTimer(object obj)
        {
            this.CurrentTime = DateTime.Now;
            Debug.WriteLine(this.CurrentTime);
        }

        public void CheckLogin()
        {
            var lr = Authentication.Check(this.Username, this.Password);

            if (lr.Success == false)
            {
                this.Error = true;
                this.Message = "Login fallito!";

                Messenger.Default.Send<ShowDialogMessage>
                    (new ShowDialogMessage() {
                        Title = "Login Fallito",
                        Message = "Controlla le credenziali!"
                    });
            }
            else
            {
                this.Error = false;
                this.Message = string.Empty;

                Messenger.Default.Send<OpenViewMessage>
                    (new OpenViewMessage() {
                        NewPage = "MenuPage",
                        Parameter = lr
                    });
            }
        }
    }
}