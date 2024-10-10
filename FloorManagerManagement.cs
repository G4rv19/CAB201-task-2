using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class FloorManagerManagement {
        public List<string> patients = new List<string>(); 
        public List<string> surgeons = new List<string>();
        ManagementTools managementTools = new ManagementTools();
        public void AssigningRoom(User user){
            managementTools.CheckedInPatientList(patient => patient.Checked_in == true && patient.Room == null && patient.Floor == null, patients);
            if (patients.Count == 0){
                Console.WriteLine("There are no checked in patients.");
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
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice <1 || choice > patients.Count){
                    Console.WriteLine("Invalid choice.");
                    return;
                }
                else{
                    if (managementTools.patientEmailMap.TryGetValue(choice, out string selectedPatientEmail)){
                        User selectedPatient = Register.GetUser(selectedPatientEmail);
                        if (selectedPatient.Room != null){
                            Console.WriteLine("Patient already has a room assigned.");
                            return;
                        }
                        else{
                            Console.WriteLine("Please enter your room (1-10):");
                            int room = Convert.ToInt32(Console.ReadLine());
                            selectedPatient.Room = room;
                            selectedPatient.Floor = user.Floor_number;
                            Console.WriteLine($"Patient {selectedPatient.Name} has been assigned to room number {room} on floor {user.Floor_number}.");
                        }    
                    }
                    else{
                        Console.WriteLine("Error in retrieving patient.");
                    }

                }
            }

        }
        public void UnassignRoom(User user){
            managementTools.CheckedInPatientList(patient => patient.Checked_in == false && patient.Room != null && patient.Floor != null, patients);
            if (patients.Count == 0){
                Console.WriteLine("There are no patients with assigned rooms.");
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
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice <1 || choice > patients.Count){
                    Console.WriteLine("Invalid choice.");
                    return;
                }
                else{
                    string selectedPatientEmail = managementTools.patientEmailMap[choice - 1];
                    User selectedPatient = Register.GetUser(selectedPatientEmail);
                    Console.WriteLine($"Room number {selectedPatient.Room} on floor {selectedPatient.Floor} has been unassigned.");
                    selectedPatient.Room = null;
                    selectedPatient.Floor = null;
                }
                return;
            }
        }
        
        public void AssignSurgery(User user){
            managementTools.CheckedInPatientList(patient => patient.surgeonassigned == null && patient.surgeryDateTime == null && patient.Checked_in == true && patient.Room != null, patients);
            if (patients.Count == 0){
                Console.WriteLine("No patients are checked in and assigned to a room.");
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
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice <1 || choice > patients.Count){
                    Console.WriteLine("Invalid choice.");
                    return;
                }
                else{
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
                        Console.WriteLine($"Please enter a choice between 1 and {surgeons.Count}.");
                        int surgeonChoice = Convert.ToInt32(Console.ReadLine());
                        if (surgeonChoice <1 || surgeonChoice > surgeons.Count){
                            Console.WriteLine("Invalid choice.");
                            return;
                        }
                        else{
                            string selectedSurgeonEmail = managementTools.patientEmailMap[surgeonChoice];
                            User selectedSurgeon = Register.GetUser(selectedSurgeonEmail);
                            selectedPatient.surgeonassigned = selectedSurgeon.Name;
                            Console.WriteLine("Please enter the date and time of the surgery (yyyy-mm-dd hh:mm):");
                            string surgeryDateTime = Console.ReadLine();
                            selectedPatient.surgeryDateTime = surgeryDateTime;
                            Console.WriteLine($"Surgeon {selectedSurgeon.Name} has been assigned to patient {selectedPatient.Name}.");
                            Console.WriteLine($"Surgery will take place on {surgeryDateTime}.");
                        }
    
                            
                            

                    }
                    
                }
            }
        }

    
    }
}