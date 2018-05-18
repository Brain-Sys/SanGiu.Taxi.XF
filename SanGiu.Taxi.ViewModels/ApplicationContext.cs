using SanGiu.Taxi.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanGiu.Taxi.ViewModels
{
    public class ApplicationContext
    {
        private static ApplicationContext instance;
        public static ApplicationContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationContext();
                }

                return instance;
            }
        }

        public LoginResult CurrentUser { get; set; }

        private ApplicationContext()
        {

        }
    }
}