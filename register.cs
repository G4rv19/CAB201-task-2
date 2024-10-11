using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Myapp {
    public class Register_menu {
        private string[] usertype;
        public Register_menu(){
            usertype = new string[] {
                "1. Patient",
                "2. Staff",
                "3. Return to the first menu"
            };
        }
        public void RegisterOptions(){
            Console.WriteLine("Register as which type of user:");
            foreach (string type in usertype){
                Console.WriteLine(type);
            }
            Console.WriteLine("Please enter a choice between 1 and 3.");
        }

        public void RunRegister(){
            int choice;
            RegisterOptions();
            choice = Convert.ToInt16(Console.ReadLine());
            switch (choice){
                case 1:
                    Console.WriteLine("Registering as a patient.");
                    Register register = new Register();
                    register.Register_patient();
                    register.Useris = "Patient";

                    break;
                case 2:
                    Register register_staff = new Register();
                    register_staff.staff_type();
                    break;      
                case 3:
                    Console.WriteLine("");
                    break;
                default:
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Invalid Menu Option, please try again.");
                    Console.WriteLine("#####");
                    break;
            } 
        }

    }
    /// <summary>
    /// Register patient registers the patient with the details
    /// </summary>
    public class Register {
        public string Useris;
        public static Dictionary<string, User> users = new Dictionary<string, User>();
        public void Error(string message) {
            Console.WriteLine("#####");
            Console.WriteLine($"#Error - Supplied {message} is invalid, please try again.");
            Console.WriteLine("#####");
        }
        public User Register_function() {
        Check check = new Check();
        bool valid = false;
        string name = string.Empty;
        int age = 0;
        string mobile = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        
        // Name Check
        while (!valid) {
            Console.WriteLine("Please enter in your name:");
            name = Console.ReadLine() ?? string.Empty;
            check.NameCheck(name);

            if (check.NameCheck(name) == true) {
                valid = true; // Only set to true if the name is valid
            } else {
                Error("name");
                valid = false; // Only set to true if the name is valid
            }
        }

        // Reset valid flag for age check
        valid = false;

        // Age Check
        while (!valid) {
            Console.WriteLine("Please enter in your age:");
            age = Convert.ToInt16(Console.ReadLine());
            check.AgeCheck(Useris, age);
            if(check.AgeCheck(Useris, age) == true) {
                valid = true;
            }
            else {
                Error("age");
                valid = false;
            }
        }

        // Reset valid flag for mobile check
        valid = false;

        // Mobile Check
        while (!valid) {
            Console.WriteLine("Please enter in your mobile number:");
            mobile = Console.ReadLine() ?? string.Empty;
            check.MobileCheck(mobile);
            if (check.MobileCheck(mobile) == true) {
                valid = true;
            }
            else {
                Error("mobile number");
                valid = false;
            }
        }

        // Email Check
        valid = false;
        while (!valid) {
            Console.WriteLine("Please enter in your email:");
            email = Console.ReadLine() ?? string.Empty;
            check.EmailCheck(email);
            if (check.EmailCheck(email) == true) {
                valid = true;
            }
            else {
                Error("email");
                valid = false;
            }
        }

        // Password Check
        valid = false;
        while (!valid) {
            Console.WriteLine("Please enter in your password:");
            password = Console.ReadLine() ?? string.Empty;
            check.PasswordCheck(password);
            if (check.PasswordCheck(password) == true) {
                valid = true;
            }
            else {
                Error("password");
                valid = false;
            }
        }

        // Create user after all validations are passed
        User user = new User(name, age, mobile, email, password);
        return user;
        }
        
        public void Register_patient() {
            User user = Register_function();
            user.is_staff = false;

            if (!users.ContainsKey(user.Email)) {
                users[user.Email] = user;
                Console.WriteLine($"{user.Name} is registered as a patient.");
            }
            else {
                Console.WriteLine("User already exists.");
            }
        }
        public void Register_floor_manager() {
            
            User staff = Register_function();
            staff.is_staff = true;
            Check check = new Check();
            bool valid = false;
            int id = 0;
            int floor = 0;
            while (!valid) {
                Console.WriteLine("Please enter in your staff ID:");
                id = Convert.ToInt16(Console.ReadLine());
                check.StaffIdCheck(id);
                if (check.StaffIdCheck(id) == true) {
                    valid = true;
                }
                else {
                    Error("staff ID");
                    valid = false;
                }
            }
            staff.Staff_id = id;
            valid = false;  
            while (!valid){
                Console.WriteLine("Please enter in your floor number:");
                floor = Convert.ToInt16(Console.ReadLine());
                check.FloorCheck(floor);
                if (check.FloorCheck(floor) == true) {
                    valid = true;
                }
                else {
                    Error("floor number");
                    valid = false;
                }
            } 
            staff.Floor_number = floor;

            if(!users.ContainsKey(staff.Email)) {
                users[staff.Email] = staff;
                Console.WriteLine($"{staff.Name} is registered as a floor manager.");
            }
            else {
                Console.WriteLine("User already exists.");
            }
        }
        public void staff_type() {
            List<string> staff_type = new List<string>();
            staff_type.Add("1. Floor manager");
            staff_type.Add("2. Surgeon");
            staff_type.Add("3. Return to the first menu");
            Console.WriteLine("Register as which type of staff:");
            foreach (string type in staff_type){
                Console.WriteLine(type);
            }
            Console.WriteLine("Please enter a choice between 1 and 3.");
            int choice = Convert.ToInt16(Console.ReadLine());
            if (choice == 1){
                Useris = "Floor";
            }
            else if (choice == 2)
            {
                Useris = "Surgeon";
            }
            switch (choice){
                case 1:
                    Console.WriteLine("Registering as a floor manager.");
                    Register_floor_manager();
                    break;
                case 2:
                    Console.WriteLine("Registering as a surgeon.");
                    surgeon_register();
                    break;
                case 3:
                    Console.WriteLine("");
                    break;
                default:
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Invalid Menu Option, please try again.");
                    Console.WriteLine("#####");
                    break;
            }
            
        }
        public void surgeon_register() {
            User staff = Register_function();
            staff.is_staff = true;
            Check check = new Check();
            int id = 0;
            bool valid = false;
            while (!valid) {
                Console.WriteLine("Please enter in your staff ID:");
                id = Convert.ToInt16(Console.ReadLine());
                check.StaffIdCheck(id);
                if (check.StaffIdCheck(id) == true) {
                    valid = true;
                }
                else {
                    Error("staff ID");
                    valid = false;
                }
            }
            staff.Staff_id = id;

            List<string> speciality = new List<string>();
            speciality.Add("1. General Surgeon");
            speciality.Add("2. Orthopaedic Surgeon");
            speciality.Add("3. Cardiothoracic Surgeon");
            speciality.Add("4. Neurosurgeon");
            bool ValidChoice = false;
            while (!ValidChoice){
                Console.WriteLine("Please choose your speciality:");
                foreach (string type in speciality){
                    Console.WriteLine(type);
                }
                Console.WriteLine("Please enter a choice between 1 and 4.");
                int choice = Convert.ToInt16(Console.ReadLine());
                switch (choice){
                    case 1:
                        staff.Surgeon_speciality = "General Surgeon";
                        ValidChoice = true;
                        break;
                    case 2:
                        staff.Surgeon_speciality = "Orthopaedic Surgeon";
                        ValidChoice = true;
                        break;
                    case 3:
                        staff.Surgeon_speciality = "Cardiothoracic Surgeon";
                        ValidChoice = true;
                        break;
                    case 4:
                        staff.Surgeon_speciality = "Neurosurgeon";  
                        ValidChoice = true;
                        break;
                    default:
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - Non-valid speciality type, please try again.");
                        Console.WriteLine("#####");
                        break;
                }
            }

            if (!users.ContainsKey(staff.Email)) {
                users[staff.Email] = staff;
                Console.WriteLine($"{staff.Name} is registered as a surgeon.");
            }
            else {
                Console.WriteLine("User already exists.");
            }
        }

        public static User? GetUser(string email) {
            return users.TryGetValue(email, out User user) ? user : null;
        }
    }
}
