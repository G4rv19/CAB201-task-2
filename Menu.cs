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
                        Login login = new Login();
                        login.LoginUser();
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
        RegisterOptions();
        choice = Convert.ToInt16(Console.ReadLine());
        switch (choice){
            case 1:
                Console.WriteLine("Registering as a patient.");
                Register register = new Register();
                register.Register_function();
                register.Patient_register();
                break;
            case 2:
                Register register_staff = new Register();
                register_staff.staff_type();
                break;      
            case 3:
                Console.WriteLine("");
                break;
            default:
                Console.WriteLine("Invalid choice. Please enter a choice between 1 and 3.");
                break;
        } 
    }

}
/// <summary>
/// Register patient registers the patient with the details
/// </summary>
public class Register {
    public static List<string> names = new List<string>(); 
    public static List<int> ages = new List<int>();
    public static List<int> mobiles = new List<int>();
    public static List<string> emails = new List<string>();
    public static List<string> passwords = new List<string>();
    public static List<int> staff_id = new List<int>();
    public static List<int> floor_number = new List<int>();
    private static List<string> staff_email = new List<string>();
    private static List<string> staff_speciality = new List<string>();

    private string current_name;
    private string current_email;

    public void Register_function() {
        Console.WriteLine("Please enter in your name:");
        string name = Console.ReadLine();
        current_name = name;
        names.Add(current_name);

        Console.WriteLine("Please enter in your age:");
        int age = Convert.ToInt16(Console.ReadLine());
        ages.Add(age);

        Console.WriteLine("Please enter in your mobile number:");
        int mobile = Convert.ToInt32(Console.ReadLine());
        mobiles.Add(mobile);

        Console.WriteLine("Please enter in your email:");
        string email = Console.ReadLine();
        current_email = email;
        emails.Add(current_email);

        Console.WriteLine("Please enter in your password:");
        string password = Console.ReadLine();
        passwords.Add(password);
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
                Register_function();
                floor_register();
                break;
            case 2:
                Console.WriteLine("Registering as a surgeon.");
                break;
            case 3:
                Console.WriteLine("");
                break;
            default:
                Console.WriteLine("Invalid choice. Please enter a choice between 1 and 3.");
                break;
        }

    }
    public void Patient_register() {
        Console.WriteLine($"{current_name} is registered as a patient.");
    }

    public void floor_register() {
        Console.WriteLine("Please enter in your staff ID:");
        int id = Convert.ToInt16(Console.ReadLine());
        staff_id.Add(id);
        staff_email.Add(current_email);
        Console.WriteLine("Please enter in your floor number:");
        int floor = Convert.ToInt16(Console.ReadLine());
        floor_number.Add(floor);
        Console.WriteLine($"{current_name} is registered as a floor manager.");

    }

    public void surgeon_register() {
        Console.WriteLine("Please enter in your staff ID:");
        int id = Convert.ToInt16(Console.ReadLine());
        staff_id.Add(id);
        staff_email.Add(current_email);
        List<string> speciality = new List<string>();
        speciality.Add("1. General Surgeon");
        speciality.Add("2. Orthopaedic Surgeon");
        speciality.Add("3. Cardiothoracic Surgeon");
        speciality.Add("4. Neurosurgeon");
        Console.WriteLine("Please enter your speciality:");
        foreach (string type in speciality){
            Console.WriteLine(type);
        }
        Console.WriteLine("Please enter a choice between 1 and 4.");
        int choice = Convert.ToInt16(Console.ReadLine());
        switch(choice){
            case 1:
                staff_speciality.Add(speciality[0]);
                Console.WriteLine($"{current_name} is registered as a Surgeon.");
                break;
            case 2:
                staff_speciality.Add(speciality[1]);
                Console.WriteLine($"{current_name} is registered as a Surgeon.");
                break;
            case 3:
                staff_speciality.Add(speciality[2]);
                Console.WriteLine($"{current_name} is registered as a Surgeon.");
                break;
            case 4:
                staff_speciality.Add(speciality[3]);
                Console.WriteLine($"{current_name} is registered as a Surgeon.");
                break;
            default:
                Console.WriteLine("Invalid choice. Please enter a choice between 1 and 4.");
                break;
        }
    }


    

}



public class Login {
    public void LoginUser(){
        Console.WriteLine("Login Menu.");    
        Console.WriteLine("Please enter in your email:");
        int index = -1;
        string email = Console.ReadLine();
        for (int i = 0; i < Register.emails.Count; i++){
            if (Register.emails[i] == email){
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
}