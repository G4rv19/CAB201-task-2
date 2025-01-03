using System;
using System.Collections.Generic;
using System.Dynamic;
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
        public string Mobile { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public bool is_staff { get; set; }
        public int? Staff_id { get; set; }
        public int? Floor_number { get; set; }
        public string? Surgeon_speciality { get; set; }
        public bool Checked_in { get; set; }
        public int? Room { get; set; }
        public int? Floor { get; set; }
        public string? surgeonassigned { get; set; }
        public string? surgeryDateTime { get; set; }
        public bool? SurgeryPerformed { get; set; }

    
    public User(string name, int age, string mobile, string email, string password) { // every user shoud have a name, age, mobile, email and password but if the user is a staff member, they should have a staff id and respective fields.
        Name = name;
        Age = age;
        Mobile = mobile;
        Email = email;
        Password = password;
        Checked_in = false;
        Room = null;
        Floor = null;
        surgeonassigned = null;
        surgeryDateTime = null;
        
        } 
    }

}