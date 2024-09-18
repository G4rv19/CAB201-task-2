using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

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
                        Console.WriteLine("You have chosen to register as a new user.");
                        break;
                    case 3:
                        Console.WriteLine("You have chosen to exit the program.");
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
                "1. Login as a registered user.",
                "2. Register as a new user.",
                "3. Exit."
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