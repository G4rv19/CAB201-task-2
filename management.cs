using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class Management {

        public static List<string> CheckedInPatients = new List<string>(); // list of all patients who are checked in  
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
            CheckedInPatients.Clear();
            Dictionary<int, string> patientEmailMap = new Dictionary<int, string>();
            int index = 1;  
            foreach (var patient in Register.users){
                if (patient.Value.Checked_in){
                    CheckedInPatients.Add(patient.Value.Name);
                    patientEmailMap.Add(index, patient.Value.Email);
                    Console.WriteLine($"{index}. {patient.Value.Name}");
                    index++;
                }
            }

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
            Console.WriteLine($"Patient {selectedPatient.Name} has been assigned to room {selectedPatient.Floor}.");
        }

        public void PatientSeeRoom(User user){
            if(user.Room == null){
                Console.WriteLine("You have not been assigned a room.");
            }
            else{
                Console.WriteLine($"Your room is nunber {user.Room} on floor {user.Floor}.");
            }
        }

    }

}
