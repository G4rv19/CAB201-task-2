using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Myapp {
    public class Register { 
        public string Useris; // User type
        public static Dictionary<string, User> users = new Dictionary<string, User>();
        private string[] usertype;
        UserInputService inputService = new UserInputService();
        public Register(){
            Useris = string.Empty;
            usertype = new string[] {
                "1. Patient",
                "2. Staff",
                "3. Return to the first menu"
            };
        }
        public void RegisterOptions(){ // Register options
            Console.WriteLine("Register as which type of user:");
            foreach (string type in usertype){
                Console.WriteLine(type);
            }
            Console.WriteLine("Please enter a choice between 1 and 3.");
        }
        public void RunRegister(){ // Run register
            int choice;
            RegisterOptions();
            choice = inputService.GetIntInput() ?? 0;
            Check check = new Check();
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
                    check.ErrorInvalid("Invalid Menu Option, please try again.");
                    break;
            } 
        }
        public User Register_function() { // Register function
        Check check = new Check();
        bool valid = false;
        string name = string.Empty;
        int age = 0;
        string mobile = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        
        // Name Check
        while (!valid) { // validation for name
            Console.WriteLine("Please enter in your name:");
            name = inputService.GetStringInput() ?? string.Empty;
            check.NameCheck(name);

            if (check.NameCheck(name) == true) {
                valid = true; // Only set to true if the name is valid
            } else {
                check.ErrorInvalid("Supplied name is invalid, please try again.");
                valid = false; // Only set to true if the name is valid
            }
        }

        // Reset valid flag for age check
        valid = false;

        // Age Check
        while (!valid) { // validation for age
            Console.WriteLine("Please enter in your age:");
            if(int.TryParse(Console.ReadLine(), out age)) {
                if(check.AgeCheck(Useris, age) == true) { // Assuming the method is named 'AgeCheck'
                    valid = true;
                }
                else {
                    check.ErrorInvalid("Supplied age is invalid, please try again.");
                }
            }
            else {
                check.ErrorInvalid("Supplied value is not an integer, please try again.");
            }
        }

        // Reset valid flag for mobile check
        valid = false;

        // Mobile Check
        while (!valid) { // check the validation for mobile number
            Console.WriteLine("Please enter in your mobile number:");
            mobile = inputService.GetStringInput() ?? string.Empty;
            if (check.MobileCheck(mobile) == true) {
                valid = true;
            }
            else {
                check.ErrorInvalid("Supplied mobile number is invalid, please try again.");
                valid = false;
            }
        }

        // Email Check
        valid = false;
        while (!valid) { // validation for email
            Console.WriteLine("Please enter in your email:");
            email = inputService.GetStringInput() ?? string.Empty;
            if (check.EmailCheck(email) == true) {
                valid = true;
            }
            else {
                valid = false;
            }
        }

        // Password Check
        valid = false;
        while (!valid) { // validation for password
            Console.WriteLine("Please enter in your password:");
            password = inputService.GetStringInput() ?? string.Empty;
            check.PasswordCheck(password);
            if (check.PasswordCheck(password) == true) {
                valid = true;
            }
            else {
                check.ErrorInvalid("Supplied password is invalid, please try again.");
                valid = false;
            }
        }

        // Create user after all validations are passed
        User user = new User(name, age, mobile, email, password);
        return user;
        }
        
        private void Register_patient() { // Register patient
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
        private void Register_floor_manager() {
            
            User staff = Register_function(); // Register function
            staff.is_staff = true; // Set staff to true
            Check check = new Check(); 
            bool valid = false;
            int id = 0;
            int floor = 0;
            while (!valid) { // validation for staff ID
                Console.WriteLine("Please enter in your staff ID:");
                id = inputService.GetIntInput() ?? 0;
                if (check.StaffIdCheck(id) == true) {
                    valid = true;
                }
                else {
                    valid = false;
                }
            }
            staff.Staff_id = id;
            valid = false;  
            while (!valid){ // validation for floor number
                Console.WriteLine("Please enter in your floor number:");
                floor = inputService.GetIntInput() ?? 0;
                if (check.FloorCheck(floor) == true) {
                    valid = true;
                }
                else {
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
        private void staff_type() { 
            List<string> staff_type = new List<string>(); 
            staff_type.Add("1. Floor manager");
            staff_type.Add("2. Surgeon");
            staff_type.Add("3. Return to the first menu");
            Console.WriteLine("Register as which type of staff:");
            Check check = new Check();
            foreach (string type in staff_type){
                Console.WriteLine(type);
            }
            Console.WriteLine("Please enter a choice between 1 and 3.");
            int choice = inputService.GetIntInput() ?? 0;
            if (choice == 1){
                Useris = "Floor"; // Set user type to floor
            }
            else if (choice == 2)
            {
                Useris = "Surgeon"; // Set user type to surgeon
            }
            switch (choice){
                case 1:
                if (!check.AllFloorsFull()){ // Check if all floors are full otherwise lead to register floor manager
                    Console.WriteLine("Registering as a floor manager.");
                    Register_floor_manager();
                }
                else{
                    check.ErrorInvalid("All floors are assigned.");
                }
                break;
                case 2: 
                    Console.WriteLine("Registering as a surgeon.");
                    surgeon_register();
                    break;
                case 3:
                    Console.WriteLine("");
                    break;
                default:
                    check.ErrorInvalid("Invalid Menu Option, please try again.");
                    break;
            }
            
        }
        private void surgeon_register() { 
            User staff = Register_function();
            staff.is_staff = true;
            Check check = new Check();
            int id = 0;
            bool valid = false;
            while (!valid) { // validation for staff ID
                Console.WriteLine("Please enter in your staff ID:");
                id = inputService.GetIntInput() ?? 0;
                if (check.StaffIdCheck(id) == true) {
                    valid = true;
                }
                else {
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
                int choice = inputService.GetIntInput() ?? 0;
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
                        check.ErrorInvalid("Non-valid speciality type, please try again.");
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
            User user;
            if (users.TryGetValue(email, out user))
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
