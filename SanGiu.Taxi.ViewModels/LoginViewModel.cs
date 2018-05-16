using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SanGiu.Taxi.Auth;
using SanGiu.Taxi.Interfaces;
using SanGiu.Taxi.ViewModels.Messages;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SanGiu.Taxi.ViewModels
{
    public class LoginViewModel : ApplicationViewModelBase
    {
        private Timer timer;

        // Non sono adatte ad un viewmodel
        //public string Username { get; set; }
        //public string Password { get; set; }
        //public bool Remember { get; set; }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                base.RaisePropertyChanged();
                this.LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                base.RaisePropertyChanged();
                this.LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private bool remember;
        public bool Remember
        {
            get { return remember; }
            set
            {
                remember = value;
                base.RaisePropertyChanged();
            }
        }

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
            set
            {
                message = value;
                base.RaisePropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            // Init dei command
            this.LoginCommand = new RelayCommand(loginCommandExecute, loginCommandCanExecute);

            // Init delle proprietà
            this.Error = false;
            //this.Username = "corso";
            //this.Password = "macerata2";
            this.loadCredentials();
            this.CurrentTime = DateTime.Now;
            this.Remember = true;
            timer = new Timer(updateTimer, null, 0, 1000);
        }

        private void loadCredentials()
        {
            var msg = new DependencyMessage<IStorage>();

            // Funzione di callback
            msg.Resolved = (IStorage resolver) => {
                if (resolver == null) return;

                var dir = resolver.GetCurrentDirectory();
                dir = $"{dir}{Path.DirectorySeparatorChar}Cred.txt";

                if (File.Exists(dir))
                {
                    string content = File.ReadAllText(dir);
                    string[] parts = content.Split(new string[] { "|||" },
                        StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length == 2)
                    {
                        this.Username = parts[0];
                        this.Password = parts[1];
                    }
                }
            };

            // Spedisco il messaggio
            Messenger.Default.Send<DependencyMessage<IStorage>>(msg);
        }

        private void updateTimer(object obj)
        {
            this.CurrentTime = DateTime.Now;
            Debug.WriteLine(this.CurrentTime);
        }

        private bool loginCommandCanExecute()
        {
            if (string.IsNullOrEmpty(this.Username)) return false;
            if (string.IsNullOrEmpty(this.Password)) return false;

            return true;
        }

        private async void loginCommandExecute()
        {
            this.IsBusy = true;

            await Task.Run(async () =>
            {
#if DEBUG
                await Task.Delay(2000);
#endif

                var lr = Authentication.Check(this.Username, this.Password);

                #region Success == false
                if (lr.Success == false)
                {
                    this.Error = true;
                    this.Message = "Login fallito!";

                    Messenger.Default.Send<ShowDialogMessage>
                        (new ShowDialogMessage()
                        {
                            Title = "Login Fallito",
                            Message = "Controlla le credenziali!"
                        });
                }
                #endregion
                #region Success == true
                else
                {
                    this.Error = false;
                    this.Message = string.Empty;

                    if (this.Remember)
                    {
                        saveCredentialsOnFile();
                    }

                    Messenger.Default.Send<OpenViewMessage>
                        (new OpenViewMessage()
                        {
                            NewPage = "MenuPage",
                            Parameter = lr
                        });
                }
                #endregion
            });

            this.IsBusy = false;
        }

        private void saveCredentialsOnFile()
        {
            // Salvo su file le credenziali
            var msg = new DependencyMessage<IStorage>();
            msg.Resolved = saveFile;
            // Posso evitare di specificare il generics
            Messenger.Default.Send(msg);
        }

        private void saveFile(IStorage resolver)
        {
            if (resolver != null)
            {
                var dir = resolver.GetCurrentDirectory();
                dir = $"{dir}{Path.DirectorySeparatorChar}Cred.txt";
                File.WriteAllText(dir, $"{this.Username}|||{this.Password}");
            }
        }
    }
}