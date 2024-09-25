using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Myapp {
    public class Login {
        /// <summary>
        /// Login user is just a function to verify the login process with email and password.
        /// </summary>
        string current_email;
        public void LoginUser(){
            Console.WriteLine("Login Menu.");    
            Console.WriteLine("Please enter in your email:");
            int index = -1;
            current_email = Console.ReadLine();
            for (int i = 0; i < Register.emails.Count; i++){
                if (Register.emails[i] == current_email){
                    index = i;
                    break;
                }
            }
            if (index != -1){
                Console.WriteLine("Please enter in your password:");
                string enteredPassword = Console.ReadLine();
                if (enteredPassword == Register.passwords[index]){
                    Console.WriteLine($"Hello {Register.names[index]} welcome back.");
                } else {
                    Console.WriteLine("Incorrect password. Please try again.");
                }
            }
        }

        public void login_staff() {
            int index;
            if (Register.staff_email.Contains(current_email)) {
                index = Register.staff_email.IndexOf(current_email);
                Console.WriteLine($"{Register.staff_id[index]} is logged in.");
                
            }
            else {
                login_patient_menu();
            }
        }


        public void login_patient_menu() {
            List<string> menu_list = new List<string>();
            menu_list.Add("1. Display my details");
            menu_list.Add("2. Change password");
            menu_list.Add("3. Check in");
            menu_list.Add("4. See room");
            menu_list.Add("5. See surgeon");
            menu_list.Add("6. See surgery date and time");
            menu_list.Add("7. log out");

            Console.WriteLine("Patient Menu.");
            Console.WriteLine("Please choose from the menu below:");
            foreach (string option in menu_list){
                Console.WriteLine(option);
            }
            Console.WriteLine("Please enter a choice between 1 and 7.");
            int choice = Convert.ToInt16(Console.ReadLine());
            switch (choice){
                case 1:
                    Console.WriteLine("Displaying details.");
                    break;
                case 2:
                    Console.WriteLine("Changing password.");
                    break;  
                case 3:
                    Console.WriteLine("Checking in.");
                    break; 
                case 4:
                    Console.WriteLine("Seeing room.");
                    break;
                case 5: 
                    Console.WriteLine("Seeing surgeon.");
                    break;
                case 6:
                    Console.WriteLine("Seeing surgery date and time.");
                    break;
                case 7:
                    Console.WriteLine("Logging out.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a choice between 1 and 7.");
                    break;

            }
        }
        
    }
   
}