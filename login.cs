using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp{
    public class Login{
        private string email;
        Check check = new Check();

        public User Login_User(){
            Console.WriteLine("Login Menu.");
            if (Register.users.Count == 0){
                check.ErrorInvalid("There are no people registered.");
                return null;
            }
            else{
                Console.WriteLine("Please enter in your email:");
                email = Console.ReadLine();
                User user = Register.GetUser(email);
                if (user == null){
                    check.ErrorInvalid("Email is not registered.");
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
                        check.ErrorInvalid("Wrong Password.");
                    }
                }
                return user;
            }
        
        }
        
        private void login_menu(){
            User user = Register.GetUser(email);
            ManagementTools management = new ManagementTools();
            SurgeonManagement surgeonManagement = new SurgeonManagement(user);
            FloorManagerManagement floormanagement = new FloorManagerManagement();
            if (user.is_staff == false){
                bool is_patient_logged_in = true;
                while(is_patient_logged_in){  
                    PatientMenu(user);          
                    int choice = Convert.ToInt16(Console.ReadLine());
                    switch (choice){
                        case 1:
                            management.show_details(user);
                            break;
                        case 2:
                            management.change_password(user);
                            break;
                        case 3:
                            management.CheckIn(user.Checked_in, user);
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
                        default:
                            check.ErrorInvalid("Invalid choice.");
                            break;
                    }
                }

            }
            else if(user.is_staff == true){
                if (user.Floor_number != null) {
                    bool is_staff_logged_in = true;
                    while(is_staff_logged_in){
                        FloorManagerMenu(user);
                        int choice = Convert.ToInt16(Console.ReadLine());
                        switch(choice){
                            case 1:
                                management.show_details(user); 
                                break;
                            case 2:
                                management.change_password(user);
                                break;
                            case 3:
                            if(!check.AllRoomsFull()){
                                floormanagement.AssigningRoom(user);
                            }
                            else{
                                check.ErrorInvalid("All rooms on this floor are assigned.");
                            }
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
                            default:
                                check.ErrorInvalid("Invalid choice.");
                                break;

                        }
                    }
                }
                else if(user.Surgeon_speciality != null){
                    bool is_surgeon_logged_in = true;
                    while(is_surgeon_logged_in){
                        SurgeonMenu(user);
                        int choice = Convert.ToInt16(Console.ReadLine());
                        switch(choice){
                            case 1:
                                management.show_details(user);
                                break;
                            case 2:
                                management.change_password(user);
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
                            default:
                                check.ErrorInvalid("Invalid choice.");
                                break;
                        }
                    }
                }
            }
        }
        private void PatientMenu(User user){
            Console.WriteLine("Patient Menu.");
            Console.WriteLine("Please choose from the menu below:");
            Console.WriteLine("1. Display my details");
            Console.WriteLine("2. Change password");
            if (user.SurgeryPerformed == true){
                if (user.Checked_in == true){
                    Console.WriteLine("3. Check out");
                }
                else{
                    Console.WriteLine("3. Check in");
                }
            }
            else{
                if (!user.Checked_in){
                    Console.WriteLine("3. Check in");
                }
                else{
                    Console.WriteLine("3. Check out");
                }
            }
            Console.WriteLine("4. See room");
            Console.WriteLine("5. See surgeon");
            Console.WriteLine("6. See surgery date and time");
            Console.WriteLine("7. Log out");
            Console.WriteLine("Please enter a choice between 1 and 7.");
            
            
        }

        private void FloorManagerMenu(User user){
            Console.WriteLine("Floor Manager Menu.");
            Console.WriteLine("Please choose from the menu below:");
            Console.WriteLine("1. Display my details");
            Console.WriteLine("2. Change password");
            Console.WriteLine("3. Assign room to patient");
            Console.WriteLine("4. Assign surgery");
            Console.WriteLine("5. Unassign room");
            Console.WriteLine("6. Log out");
            Console.WriteLine("Please enter a choice between 1 and 6.");
        }
        private void SurgeonMenu(User user){
            Console.WriteLine("Surgeon Menu.");
            Console.WriteLine("Please choose from the menu below:");
            Console.WriteLine("1. Display my details");
            Console.WriteLine("2. Change password");
            Console.WriteLine("3. See your list of patients");
            Console.WriteLine("4. See your schedule");
            Console.WriteLine("5. Perform surgery");
            Console.WriteLine("6. Log out");
            Console.WriteLine("Please enter a choice between 1 and 6.");
        }

    }
}