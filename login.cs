using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp{
    public class Login{
        private string email; // email of the user
        Check check = new Check();

        public User Login_User(){ // Login method for the user
            Console.WriteLine("Login Menu.");
            if (Register.users.Count == 0){
                check.ErrorInvalid("There are no people registered."); // Error message if no users are registered
                return null;
            }
            else{
                Console.WriteLine("Please enter in your email:"); 
                string input = Console.ReadLine() ?? string.Empty; // Read the email input
                email = input ?? string.Empty;
                User user = Register.GetUser(email!); // Get the user from the email
                if (user == null){
                    check.ErrorInvalid("Email is not registered."); 
                    return null; // Error message if email is not registered
                }
                else{
                    Console.WriteLine("Please enter in your password:"); 
                    string input2 = Console.ReadLine() ?? string.Empty; // Read the password input
                    string password = input2 ?? string.Empty;
                    if (user.Password == password){ // Check if the password is correct
                        Console.WriteLine($"Hello {user.Name} welcome back.");
                        login_menu(); // Run the login menu
                    }
                    else{
                        check.ErrorInvalid("Wrong Password.");
                    }
                }
                return user; // return the user when logged in.
            }
        
        }
        
        private void login_menu(){ // Login menu for the user
            User user = Register.GetUser(email); // Get the user from the email
            ManagementTools management = new ManagementTools(); // ManagementTools object
            SurgeonManagement surgeonManagement = new SurgeonManagement(user);
            FloorManagerManagement floormanagement = new FloorManagerManagement();
            if (user.is_staff == false){ // Check if the user is a patient or not with the is_staff attribute
                bool is_patient_logged_in = true; // Set the patient logged in to true
                while(is_patient_logged_in){   // While the patient is logged in
                    PatientMenu(user);   // Run the patient menu
                    if (!int.TryParse(Console.ReadLine(), out int choice)){
                        check.ErrorInvalid("Invalid choice.");
                        continue;
                    }
                    switch (choice){
                        case 1:
                            management.show_details(user); // Show the details of the user
                            break;
                        case 2:
                            management.change_password(user); // Change the password of the user
                            break;
                        case 3:
                            management.CheckIn(user.Checked_in, user); // Check in the user
                            break;
                        case 4:
                            management.PatientSeeRoom(user); // See the room of the user
                            break;
                        case 5:
                            management.SurgeonAssigned(user); // See the surgeon assigned to the user
                            break;
                        case 6: 
                            management.ShowSurgeryDate(user); // Show the surgery date of the user
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
            else if(user.is_staff == true){ // Check if the user is a staff member
                if (user.Floor_number != null) { // check if the staff member is a floor manager or not
                    bool is_staff_logged_in = true;
                    while(is_staff_logged_in){
                        FloorManagerMenu(user);
                        if(!int.TryParse(Console.ReadLine(), out int choice)){
                            check.ErrorInvalid("Invalid choice.");
                            continue;
                        }
                        switch(choice){
                            case 1:
                                management.show_details(user);  // Show the details of the user
                                break;
                            case 2:
                                management.change_password(user); // Change the password of the user
                                break;
                            case 3:
                            if(!check.AllRoomsFull()){ // Check if all rooms are full or not if they not full the execute the following code
                                floormanagement.AssigningRoom(user);  // Assign room to the patient
                            }
                            else{
                                check.ErrorInvalid("All rooms on this floor are assigned."); // Error message if all rooms are full
                            }
                                break;
                            case 4:
                                floormanagement.AssignSurgery(user); // Assign surgery to the patient
                                break;
                            case 5:
                                floormanagement.UnassignRoom(user); // Unassign room from the patient
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
                else if(user.Surgeon_speciality != null){ // Check if the staff member is a surgeon or not
                    bool is_surgeon_logged_in = true; // Set the surgeon logged in to true
                    while(is_surgeon_logged_in){
                        SurgeonMenu(user);
                        if(!int.TryParse(Console.ReadLine(), out int choice)){
                            check.ErrorInvalid("Invalid choice.");
                            continue;
                        }
                        switch(choice){
                            case 1:
                                management.show_details(user); 
                                break;
                            case 2:
                                management.change_password(user);
                                break;
                            case 3:
                                surgeonManagement.PatientAssignedForSurgeon(user); // Show the patients assigned to the surgeon
                                break;
                            case 4:
                                surgeonManagement.SeeSurgery(user); // Show the surgery schedule of the surgeon
                                break;
                            case 5:
                                surgeonManagement.PerformSurgery(user); // Perform surgery on the patient
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
        private void PatientMenu(User user){ // Patient menu
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

        private void FloorManagerMenu(User user){ // Floor manager menu
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
        private void SurgeonMenu(User user){ // Surgeon menu
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