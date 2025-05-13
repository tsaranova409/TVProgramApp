using System.Collections.Generic;

namespace TVProgramApp
{
    public class User
    {
        public static List<User> Users = new List<User>();
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public User(string username, string password, bool isAdmin)
        {
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
}