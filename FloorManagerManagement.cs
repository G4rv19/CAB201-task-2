using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class FloorManagerManagement {
        Check check = new Check(); 
        private List<string> patients = new List<string>();  // list of all patients who are checked in and have a room assigned
        private List<string> Users = new List<string>(); // list of all patients who are checked in
        private List<string> surgeons = new List<string>(); // list of all surgeons
        ManagementTools managementTools = new ManagementTools();
        UserInputService userInputService = new UserInputService();
        public void AssigningRoom(User user){ // Assigning room to patient
            Users.Clear();
            patients.Clear();
            managementTools.CheckedInPatientList(patient => patient.Checked_in == true && patient.Room == null && patient.Floor == null, patients);
            if (patients.Count == 0){
                managementTools.CheckedInPatientList(patient => patient.Name != null && patient.Email != null && patient.is_staff == false, Users);
                if (Users.Count != 0){
                    Console.WriteLine("There are no checked in patients.");
                    return;
                }
                else{
                    Console.WriteLine("There are no registered patients.");
                    return;
                }
            }
            else{
                Console.WriteLine("Please select your patient: ");
                int index = 1;
                foreach (string patient in patients){
                    Console.WriteLine($"{index}. {patient}");
                    index++;
                }
                bool valid = false;
                int choice = 0;
                while(!valid){
                    Console.WriteLine($"Please enter a choice between 1 and {patients.Count}.");
                    string input = userInputService?.GetStringInput() ?? string.Empty;
                    if (int.TryParse(input, out choice) && choice > 0 && choice <= patients.Count){
                        valid = true;
                    }
                    else{
                        check.ErrorInvalid("Supplied value is out of range, please try again.");
                    }
                }
                
                if (managementTools.patientEmailMap.TryGetValue(choice, out string selectedPatientEmail)){
                    User selectedPatient = Register.GetUser(selectedPatientEmail);
                    if (selectedPatient.Room != null){
                        Console.WriteLine("Patient already has a room assigned.");
                        return;
                    }
                    else{
                        valid = false;
                        int room = 0;
                        while(!valid){
                            Console.WriteLine("Please enter your room (1-10):");
                            room = userInputService?.GetIntInput() ?? 0;
                            if(check.RoomCheck(room, user) == true){
                                valid = true;
                                selectedPatient.Room = room;
                                selectedPatient.Floor = user.Floor_number;
                            }
                            else{
                                valid = false;
                            }
                        }
                        Console.WriteLine($"Patient {selectedPatient.Name} has been assigned to room number {room} on floor {user.Floor_number}.");
                    }    
                }
                else{
                    check.ErrorInvalid("Error in retrieving patient.");
                }

            }
        }
        public void UnassignRoom(User user){ // Unassigning room from patient
            managementTools.CheckedInPatientList(patient => patient.Checked_in == false && patient.Room != null && patient.Floor != null, patients);
            if (patients.Count == 0){
                Console.WriteLine("There are no patients ready to have their rooms unassigned.");
                return;
            }
            else{
                Console.WriteLine("Please select your patient: ");
                int index = 1;
                foreach (string patient in patients){
                    Console.WriteLine($"{index}. {patient}");
                    index++;
                }
                Console.WriteLine($"Please enter a choice between 1 and {patients.Count}.");
                int choice = userInputService?.GetIntInput() ?? 0;
                if (choice <1 || choice > patients.Count){
                    Console.WriteLine("Invalid choice.");
                    return;
                }
                else{
                    string selectedPatientEmail = managementTools.patientEmailMap[choice];
                    User selectedPatient = Register.GetUser(selectedPatientEmail);
                    Console.WriteLine($"Room number {selectedPatient.Room} on floor {selectedPatient.Floor} has been unassigned.");
                    selectedPatient.Room = null;
                    selectedPatient.Floor = null;
                }
                return;
            }
        }
        
        public void AssignSurgery(User user){ // Assigning surgery to patient
            Users.Clear();
            managementTools.CheckedInPatientList(patient => patient.surgeonassigned == null && patient.surgeryDateTime == null && patient.Checked_in == true && patient.Room != null, patients);
            if (patients.Count == 0){
                managementTools.CheckedInPatientList(patient => patient.Name != null && patient.Email != null && patient.is_staff == false, Users);
                if (Users.Count != 0){
                    Console.WriteLine("There are no patients ready for surgery.");
                    return;
                }
                else{
                    Console.WriteLine("There are no registered patients."); 
                    return;
                }
            }
            else{
                Console.WriteLine("Please select your patient: ");
                int index = 1;
                foreach (string patient in patients){
                    Console.WriteLine($"{index}. {patient}");
                    index++;
                }
                int choice = 0;
                bool valid = false;
                while(!valid){
                    Console.WriteLine($"Please enter a choice between 1 and {patients.Count}.");
                    choice = userInputService?.GetIntInput() ?? 0;
                    if (choice <1 || choice > patients.Count){
                        check.ErrorInvalid("Supplied value is out of range, please try again.");
                        valid = false;
                    }
                    else{
                        valid = true;
                    }
                }
                string selectedPatientEmail = managementTools.patientEmailMap[choice];
                User selectedPatient = Register.GetUser(selectedPatientEmail);
                Console.WriteLine("Please select your surgeon: ");
                managementTools.CheckedInPatientList(patient => patient.Staff_id != null && patient.Surgeon_speciality != null, surgeons);
                if (surgeons.Count == 0){
                    Console.WriteLine("No surgeons are available.");
                    return;
                }
                else{
                    int index2 = 1;
                    foreach (string surgeon in surgeons){
                        Console.WriteLine($"{index2}. {surgeon}");
                        index2++;
                    }
                    valid = false;
                    int surgeonChoice = 0;
                    while(!valid){
                        Console.WriteLine($"Please enter a choice between 1 and {surgeons.Count}.");
                        surgeonChoice = userInputService?.GetIntInput() ?? 0;
                        if (surgeonChoice <1 || surgeonChoice > surgeons.Count){
                            check.ErrorInvalid("Supplied value is out of range, please try again.");
                            valid = false;
                        }
                        else{
                            valid = true;
                        }
                    }
                    string selectedSurgeonEmail = managementTools.patientEmailMap[surgeonChoice];
                    User selectedSurgeon = Register.GetUser(selectedSurgeonEmail);
                    selectedPatient.surgeonassigned = selectedSurgeon.Name;
                    string surgeryDateTime;
                    valid = false;
                    while(!valid){
                        Console.WriteLine("Please enter a date and time (e.g. 14:30 31/01/2024).");
                        surgeryDateTime = userInputService?.GetStringInput() ?? string.Empty;
                        check.DateTimeCheck(surgeryDateTime);
                        bool isDateTimeTaken = false;
                        foreach (var otherUser in Register.users){
                            if (otherUser.Value.surgeryDateTime == surgeryDateTime){
                                isDateTimeTaken = true;
                                break;
                            }
                        }
                        if (isDateTimeTaken){
                            check.ErrorInvalid("Surgery date and time is already taken.");
                            valid = false;
                        }
                        else{
                            if(check.DateTimeCheck(surgeryDateTime) == true){
                                valid = true;
                                selectedPatient.surgeryDateTime = surgeryDateTime;
                            }
                            else{
                                check.ErrorInvalid("Supplied value is not a valid DateTime.");
                                valid = false;
                            }
                        }
                    }
                    Console.WriteLine($"Surgeon {selectedSurgeon.Name} has been assigned to patient {selectedPatient.Name}.");
                    Console.WriteLine($"Surgery will take place on {selectedPatient.surgeryDateTime}.");
                }
                    
                
            }
        }
    
    }
}