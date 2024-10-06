using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class Management {

        public static List<string> CheckedInPatients = new List<string>(); // list of all patients who are checked in 
        public static User selectedPatient; // the current patient selected by the floor manager.
        User user;
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


        public void Assign_room(User user){
            DisplayCheckedInPatients();
            Console.WriteLine("Please enter your room (1-10):");
            int room = Convert.ToInt32(Console.ReadLine());
            selectedPatient.Room = room;
            Console.WriteLine($"Patient {selectedPatient.Name} has been assigned to room number {room} on floor number {user.Floor_number}.");
        }        


        public void DisplayCheckedInPatients(){
            CheckedInPatients.Clear();
            Console.WriteLine("Please select your patient:");
            int index = 1;
            foreach (var patient in Register.users){
                if(patient.Value.Checked_in){
                    CheckedInPatients.Add(patient.Value.Name);
                    Console.WriteLine($"{index}. {patient.Value.Name}");
                    index++;
                }
            }
            Console.WriteLine($"Please enter a choice betweem 1 and {CheckedInPatients.Count}:");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice > CheckedInPatients.Count){
                Console.WriteLine("Invalid choice.");
            }
            else{
            selectedPatient = Register.GetUser(CheckedInPatients[choice - 1]);
            }
        }
    }

}