using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp{
    public class Login{
        public string email;

        public void Login_User(){
            Console.WriteLine("Please enter in your email:");
            email = Console.ReadLine();
            User user = Register.GetUser(email);
            if (user == null){
                Console.WriteLine("User not found.");
            } // Add the closing brace here
            else{
                Console.WriteLine("Please enter in your password:");
                string password = Console.ReadLine();
                if (user.Password == password){
                    Console.WriteLine($"Hello {user.Name} welcome back.");
                    Console.WriteLine();
                    Console.WriteLine("Login Menu.");
                    login_menu();
                }
                else{
                    Console.WriteLine("Incorrect password.");
                }
            }
        }
        
        public static List<string> menu = new List<string>(){
            "1. Display my details ",
            "2. Change password",
            "3. Check in",
            "4. See room",
            "5. See surgeon",
            "6. See surgery date and time",
            "7. Log out",
        };
        
        public void login_menu(){
            User user = Register.GetUser(email);
            if (user.is_staff == false){
                Console.WriteLine("Patient Menu.");
                Console.WriteLine("please choose from the menu below:");
                foreach (string option in menu){
                    Console.WriteLine(option);
                }
                Console.WriteLine("Please enter a choice between 1 and 7.");    
                int choice = Convert.ToInt16(Console.ReadLine());
                switch (choice){
                    case 1:
                        Console.WriteLine("Your details.");
                        Console.WriteLine($"Name: {user.Name}");
                        Console.WriteLine($"Age: {user.Age}");
                        Console.WriteLine($"Mobile phone: {user.Mobile}");
                        Console.WriteLine($"Email: {user.Email}");
                        break;
                    case 2:
                        Console.WriteLine("Change password.");
                        break;
                    case 3:
                        Console.WriteLine("Check in.");
                        break;
                    case 4:
                        Console.WriteLine("See room.");
                        break;
                    case 5:
                        Console.WriteLine("See surgeon.");
                        break;
                    case 6: 
                        Console.WriteLine("See surgery date and time.");
                        break;
                    case 7:
                        Console.WriteLine($"Patient {user.Name} has logged out.");
                        break;
                }

            }
            else{
                Console.WriteLine("Staff Menu");
            }
        
        }
    }
}