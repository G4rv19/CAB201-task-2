using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace Myapp {
    public class Menu {
        public void RunMenu() {
            MenuDisplay display = new MenuDisplay();
            MenuOptions menuOptions = new MenuOptions();

            display.header();
            int choice;
            do{
                menuOptions.ShowOptions();
                choice = Convert.ToInt16(Console.ReadLine());
                switch (choice){
                    case 1:
                        Console.WriteLine("You have chosen to login as a registered user.");
                        break;
                    case 2:
                        RegisterType register = new RegisterType();
                        register.RunRegister();
                        break;
                    case 3:
                        Console.WriteLine("Goodbye. Please stay safe.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a choice between 1 and 3.");
                        break;
                }
            } while (choice != 3);
        }
    }

/// <summary>
///  header function to write the header once the program is run.
/// </summary>
    public class MenuDisplay {
        public void header() {
            Console.WriteLine("=================================");
            Console.WriteLine("Welcome to Gardens Point Hospital");
            Console.WriteLine("=================================");
        }
    }
/// <summary>
/// MenuOption is used in loop for options.
/// </summary>
    public class MenuOptions {
        private string[] options;

        public MenuOptions(){
            options = new string[] {
                "1. Login as a registered user",
                "2. Register as a new user",
                "3. Exit"
            };
        }
        public void ShowOptions(){
            Console.WriteLine("Please choose from the menu below:");
            foreach (string option in options){
                Console.WriteLine(option);
            }
            Console.WriteLine("Please enter a choice between 1 and 3.");
        }

    };
}

/// <summary>
/// RegisterType class is used to know the register type so they can go to the respective classes.
/// </summary>
public class RegisterType {
    private string[] usertype;
    public RegisterType(){
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
        do{
            RegisterOptions();
            choice = Convert.ToInt16(Console.ReadLine());
            switch (choice){
                case 1:
                    RegisterPatient registerPatient = new RegisterPatient();
                    registerPatient.Register();
                    break;
                case 2:
                    Console.WriteLine("You have chosen to register as a staff.");
                    break;      
                case 3:
                    Console.WriteLine("");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a choice between 1 and 3.");
                    break;
            }
        } while(choice != 3);   
    }

}
/// <summary>
/// Register patient registers the patient with the details
/// </summary>
public class RegisterPatient {
    public void Register() {
        Console.WriteLine("Please enter in your name:");
        string name = Console.ReadLine();
        Console.WriteLine("Please enter in your age:");
        int age = Convert.ToInt16(Console.ReadLine());
        Console.WriteLine("Please enter in your mobile number:");
        int mobile = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Please enter in your email:");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter in your password:");
        string password = Console.ReadLine();
        Console.WriteLine($"{name} is registered as a patient.");
    }
}