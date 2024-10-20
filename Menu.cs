using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Myapp; 

namespace Myapp {
    public class Menu {
        Check check = new Check(); // Check class object
        UserInputService userInputService = new UserInputService(); // UserInputService class object
        public void RunMenu() {
            int choice;
            displayHeader(); // Display the header
            do {
                ShowOptions(); // Display the menu options
                choice = userInputService.GetIntInput() ?? 0; // Get the user input
                switch (choice){
                    case 1:
                        Login login = new Login();
                        login.Login_User(); // Run the login menu
                        break;
                    case 2:
                        Register register = new Register();
                        register.RunRegister(); // Run the register menu
                        break;
                    case 3:
                        Console.WriteLine("Goodbye. Please stay safe."); // Exit the program
                        break; 
                    default:
                        check.ErrorInvalid("Invalid Menu Option, please try again.");
                        break; // Error message for invalid input
                } 
            } while (choice != 3);
            
        }
        private void displayHeader(){
            Console.WriteLine("=================================");
            Console.WriteLine("Welcome to Gardens Point Hospital");
            Console.WriteLine("=================================");
        }

        private List<string> options(){
            List<string> options = new List<string>();
            options.Add("1. Login as a registered user");
            options.Add("2. Register as a new user");
            options.Add("3. Exit");
            return options;
        }
        private void ShowOptions(){
            Console.WriteLine("Please choose from the menu below:");
            foreach (string option in options()){
                Console.WriteLine(option);
            }
            Console.WriteLine("Please enter a choice between 1 and 3.");
        }

    }
}

