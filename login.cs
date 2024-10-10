using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp{
    public class Login{
        public string email;

        public User Login_User(){
            Console.WriteLine("Login Menu.");
            if (Register.users.Count == 0){
                Console.WriteLine("#####");
                Console.WriteLine("#Error - There are no people registered.");
                Console.WriteLine("#####");
                return null;
            }
            else{
                Console.WriteLine("Please enter in your email:");
                email = Console.ReadLine();
                User user = Register.GetUser(email);
                if (user == null){
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Email is not registered.");
                    Console.WriteLine("#####");
                    return null;
                }
                else{
                    Console.WriteLine("Please enter in your password:");
                    string password = Console.ReadLine();
                    if (user.Password == password){
                        Console.WriteLine($"Hello {user.Name} welcome back.");
                        login_menu();
                    }
                    else{
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - Wrong Password.");
                        Console.WriteLine("#####");
                    }
                }
                return user;
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

        public static List<string> surgeon_menu = new List<string>(){
            "1. Display my details",
            "2. Change password",
            "3. See your list of patients",
            "4. See your schedule",
            "5. Perform surgery",
            "6. Log out",
        };

        public void login_menu(){
            User user = Register.GetUser(email);
            ManagementTools management = new ManagementTools();
            SurgeonManagement surgeonManagement = new SurgeonManagement(user);
            FloorManagerManagement floormanagement = new FloorManagerManagement();
            if (user.is_staff == false){
                bool is_patient_logged_in = true;
                while(is_patient_logged_in){
                    Console.WriteLine("Patient Menu.");
                    Console.WriteLine("Please choose from the menu below:");
                    management.CheckIn(user.Checked_in, menu);
                    foreach (string option in menu){
                        Console.WriteLine(option);
                    }
                    Console.WriteLine("Please enter a choice between 1 and 7.");    
                    int choice = Convert.ToInt16(Console.ReadLine());
                    switch (choice){
                        case 1:
                            show_details();
                            break;
                        case 2:
                            change_password();
                            break;
                        case 3:
                            user.Checked_in = !user.Checked_in;
                            if (user.Checked_in){
                                Console.WriteLine($"Patient {user.Name} has been checked in.");
                            }
                            else{
                                Console.WriteLine($"Patient {user.Name} has been checked out.");
                            }
                            break;
                        case 4:
                            management.PatientSeeRoom(user);
                            break;
                        case 5:
                            management.SurgeonAssigned(user);
                            break;
                        case 6: 
                            management.ShowSurgeryDate(user);
                            break;
                        case 7:
                            Console.WriteLine($"Patient {user.Name} has logged out.");
                            is_patient_logged_in = false;
                            break;
                    }
                }

            }
            else if(user.is_staff == true){
                if (user.Floor_number != null) {
                    bool is_staff_logged_in = true;
                    while(is_staff_logged_in){
                        Console.WriteLine("Floor Manager Menu.");
                        Console.WriteLine("Please choose from the menu below:");
                        foreach (string option in floor_manager_menu){
                            Console.WriteLine(option);
                        }
                        Console.WriteLine("Please enter a choice between 1 and 6.");
                        int choice = Convert.ToInt16(Console.ReadLine());
                        switch(choice){
                            case 1:
                                show_details(); 
                                break;
                            case 2:
                                change_password();
                                break;
                            case 3:
                                floormanagement.AssigningRoom(user);
                                break;
                            case 4:
                                floormanagement.AssignSurgery(user);
                                break;
                            case 5:
                                floormanagement.UnassignRoom(user);
                                break;
                            case 6:
                                Console.WriteLine($"Floor manager {user.Name} has logged out.");
                                is_staff_logged_in = false;
                                break;

                        }
                    }
                }
                else if(user.Surgeon_speciality != null){
                    bool is_surgeon_logged_in = true;
                    while(is_surgeon_logged_in){
                        Console.WriteLine("Surgeon Menu.");
                        Console.WriteLine("Please choose from the menu below:");
                        foreach (string option in surgeon_menu){
                            Console.WriteLine(option);
                        }
                        Console.WriteLine("Please enter a choice between 1 and 6.");
                        int choice = Convert.ToInt16(Console.ReadLine());
                        switch(choice){
                            case 1:
                                show_details();
                                break;
                            case 2:
                                change_password();
                                break;
                            case 3:
                                surgeonManagement.PatientAssignedForSurgeon(user);
                                break;
                            case 4:
                                surgeonManagement.SeeSurgery(user);
                                break;
                            case 5:
                                surgeonManagement.PerformSurgery(user);
                                break;
                            case 6:
                                Console.WriteLine($"Surgeon {user.Name} has logged out.");
                                is_surgeon_logged_in = false;
                                break;
                        }
                    }
                }
            }
        }
        public void show_details() {
            User user = Register.GetUser(email);
            if (user != null && user.is_staff == false){
                Console.WriteLine("Your details.");
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Age: {user.Age}");
                Console.WriteLine($"Mobile phone: {user.Mobile}");
                Console.WriteLine($"Email: {user.Email}");
            }
            else if (user != null && user.is_staff == true){
                if (user.Floor_number != null){
                    Console.WriteLine("Your details.");
                    Console.WriteLine($"Name: {user.Name}");
                    Console.WriteLine($"Age: {user.Age}");
                    Console.WriteLine($"Mobile phone: {user.Mobile}");
                    Console.WriteLine($"Email: {user.Email}");
                    Console.WriteLine($"Staff ID: {user.Staff_id}");    
                    Console.WriteLine($"Floor: {user.Floor_number}.");
                }
                else {
                    Console.WriteLine("Your details.");
                    Console.WriteLine($"Name: {user.Name}");
                    Console.WriteLine($"Age: {user.Age}");
                    Console.WriteLine($"Mobile phone: {user.Mobile}");
                    Console.WriteLine($"Email: {user.Email}");
                    Console.WriteLine($"Staff ID: {user.Staff_id}");
                    Console.WriteLine($"Speciality: {user.Surgeon_speciality}"); 
                }

            }
        }
        public void change_password(){
            Console.WriteLine("Enter new password:");
            string new_password = Console.ReadLine();
            User user = Register.GetUser(email);
            user.Password = new_password;
            Console.WriteLine("Password has been changed.");
        }
        public static List<string> floor_manager_menu = new List<string>(){
            "1. Display my details",
            "2. Change password",
            "3. Assign room to patient",
            "4. Assign surgery",
            "5. Unassign room",
            "6. Log out",
        };

    }
}