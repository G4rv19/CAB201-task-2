using System;
using System.Collections.Generic;
using System.IO;
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
        public static Dictionary<string, User> users = new Dictionary<string, User>();
        public User Register_function() {

            Console.WriteLine("Please enter in your name:");
            string name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Please enter in your age:");
            int age = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Please enter in your mobile number:");
            string mobile = Console.ReadLine();

            Console.WriteLine("Please enter in your email:");
            string email = Console.ReadLine() ?? string.Empty; 

            Console.WriteLine("Please enter in your password:");
            string password = Console.ReadLine() ?? string.Empty;

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

            Console.WriteLine("Please enter in your staff ID:");
            int id = Convert.ToInt16(Console.ReadLine());
            staff.Staff_id = id;

            Console.WriteLine("Please enter in your floor number:");
            int floor = Convert.ToInt16(Console.ReadLine());    
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

            Console.WriteLine("Please enter in your staff ID:");
            int id = Convert.ToInt16(Console.ReadLine());
            staff.Staff_id = id;

            List<string> speciality = new List<string>();
            speciality.Add("1. General Surgeon");
            speciality.Add("2. Orthopaedic Surgeon");
            speciality.Add("3. Cardiothoracic Surgeon");
            speciality.Add("4. Neurosurgeon");
            Console.WriteLine("Please choose your speciality:");
            foreach (string type in speciality){
                Console.WriteLine(type);
            }
            Console.WriteLine("Please enter a choice between 1 and 4.");
            if(!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 4) {
                return;
            }
            if (choice == 1){
                staff.Surgeon_speciality = "General Surgeon";
            }
            else if (choice == 2){
                staff.Surgeon_speciality = "Orthopaedic Surgeon";
            }
            else if (choice == 3){
                staff.Surgeon_speciality = "Cardiothoracic Surgeon";
            }
            else if (choice == 4){
                staff.Surgeon_speciality = "Neurosurgeon";
            }
            else {
                Console.WriteLine("Invalid choice. Please enter a choice between 1 and 4.");
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