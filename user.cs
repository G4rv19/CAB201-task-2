using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Myapp {
    /// <summary>
    /// user clas to store the user details.
    /// </summary>
    public class User {
        public string Name { get; set; } 
        public int Age { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public bool is_staff { get; set; }
        public int? Staff_id { get; set; }
        public int? Floor_number { get; set; }
        public string? Surgeon_speciality { get; set; }

    
    public User(string name, int age, int mobile, string email, string password) {
        Name = name;
        Age = age;
        Mobile = mobile;
        Email = email;
        Password = password;
    } 
    }

}