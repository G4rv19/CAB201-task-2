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
        Check check = new Check();
        public void RunMenu() {
            int choice;
            do {
                ShowOptions();
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice){
                    case 1:
                        Login login = new Login();
                        login.Login_User();
                        break;
                    case 2:
                        Register register = new Register();
                        register.RunRegister();
                        break;
                    case 3:
                        Console.WriteLine("Goodbye. Please stay safe.");
                        break;
                    default:
                        check.ErrorInvalid("Invalid Menu Option, please try again.");
                        break;
                }
            } while (choice != 3);
            
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

