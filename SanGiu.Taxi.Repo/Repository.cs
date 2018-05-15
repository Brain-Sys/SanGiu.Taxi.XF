using System;

namespace SanGiu.Taxi.Repo
{
    public class Repository
    {
        public string ServerName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Repository(string username)
        {
            this.Username = username;
        }
    }
}