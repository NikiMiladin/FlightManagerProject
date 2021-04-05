using System;

namespace Data.Entity
{
    public class User
    {
        public int Id { get; set;}
        public string Username { get; set;}
        public string Password { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public int EGN { get; set;}
        public string Address { get; set;}
        public int PhoneNumber { get; set;}
        public string Role { get; set;}
    }
    
}
