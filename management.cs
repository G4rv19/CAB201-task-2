using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class Management {

        public static List<string> CheckedInPatients = new List<string>(); // list of all patients who are checked in  
        public static List<string> SurgeryPatients = new List<string>(); // list of all patients who are checked in and have a surgery scheduled    
        public static List<string> SurgeonList = new List<string>(); // list of all surgeons who are available to perform surgery
        private Dictionary<int, string> patientEmailMap = new Dictionary<int, string>();
        private User user;
        public Management(User user)
        {
            this.user = user;
        }

        public void CheckIn(bool isCheckedIn, List<string> menu){
            if (user.Checked_in){
                menu[2] = "3. Check out";
            }
            else if (!user.Checked_in){
                menu[2] = "3. Check in";
            }
        }
     

        public void AssigningRoom(User user){
            Console.WriteLine("Please select your patient: ");
            CheckedInPatientList(patient => patient.Checked_in == true && patient.Room == null && patient.Floor == null, CheckedInPatients);
            if (CheckedInPatients.Count == 0){
                Console.WriteLine("No patients are checked in.");
                return;
            }
            Console.WriteLine($"Please enter a choice between 1 and {CheckedInPatients.Count}.");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice <1 || choice > CheckedInPatients.Count){
                Console.WriteLine("Invalid choice.");
                return;
            }

            string selectedPatientEmail = patientEmailMap[choice];
            User selectedPatient = Register.GetUser(selectedPatientEmail);

            if(selectedPatient == null){
                Console.WriteLine("Patient not found.");
                return;
            }
            
            Console.WriteLine("Please enter your room (1-10):");
            int room = Convert.ToInt32(Console.ReadLine());

            selectedPatient.Room = room;
            selectedPatient.Floor = user.Floor_number;
            Console.WriteLine($"Patient {selectedPatient.Name} has been assigned to room number {selectedPatient.Room} on floor {selectedPatient.Floor}.");
        }

        public void PatientSeeRoom(User user){
            if(user.Room == null){
                Console.WriteLine("You have not been assigned a room.");
            }
            else{
                Console.WriteLine($"Your room is number {user.Room} on floor {user.Floor}.");
            }
        }

        public void CheckedInPatientList(Func <User, bool> condition, List<string> SavingList){
            CheckedInPatients.Clear();
            foreach (var patient in Register.users){
                if (condition(patient.Value)){
                    SavingList.Add(patient.Value.Name);
                    patientEmailMap.Add(CheckedInPatients.Count, patient.Value.Email);
                    Console.WriteLine($"{CheckedInPatients.Count}. {patient.Value.Name}");
                }
            }
        }

        public void AsignSurgery(User user){
            Console.WriteLine("Please select a patient to perform surgery on: ");
            CheckedInPatientList(patient => patient.Checked_in == true && patient.Room != null && patient.Floor != null, SurgeryPatients);
            if (SurgeryPatients == null){
                Console.WriteLine("No patients are checked in and assigned to a room.");
                return;
            }
            Console.WriteLine($"Please enter a choice between 1 and {SurgeryPatients.Count}.");
            int choice = Convert.ToInt32(Console.ReadLine());

            string selectedPatientEmail = patientEmailMap[choice];
            User selectedPatient = Register.GetUser(selectedPatientEmail);

            Console.WriteLine("Please select your surgeon: ");
            CheckedInPatientList(patient => patient.Staff_id != null && patient.Surgeon_speciality != null, SurgeonList);
            if (SurgeonList == null){
                Console.WriteLine("No surgeons are available.");
                return;
            }
            Console.WriteLine($"Please enter a choice between 1 and {SurgeonList.Count}.");
            int surgeonChoice = Convert.ToInt32(Console.ReadLine());

            string selectedSurgeonEmail = patientEmailMap[surgeonChoice];
            User selectedSurgeon = Register.GetUser(selectedSurgeonEmail);

            Console.WriteLine("Please enter a date and time (e.g. 14:30 31/01;2024)");
            string surgeryDateTime = Console.ReadLine();
            Console.WriteLine($"Surgeon {selectedSurgeon.Name} has been assigned to patient {selectedPatient.Name}.");
            Console.WriteLine($"Surgery will take place on {surgeryDateTime}.");
        }
    }

}
