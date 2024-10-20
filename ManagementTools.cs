using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class ManagementTools {
        public Dictionary<int, string> patientEmailMap = new Dictionary<int, string>();
        public void CheckedInPatientList(Func<User, bool> condition, List<string> SavingList){ // method to list all user with required condition.
            SavingList.Clear();
            patientEmailMap.Clear();
            int index = 1;
            foreach (var patient in Register.users){
                if (condition(patient.Value)){
                    SavingList.Add(patient.Value.Name);
                    patientEmailMap.Add(index, patient.Value.Email);  
                    index++;
                }
            }
        }

        public void ShowSurgeryDate(User user){ // method to show the surgery date
            if (user.surgeryDateTime == null){
                Console.WriteLine("You do not have assigned surgery.");
                return;
            }
            else{
                Console.WriteLine($"Your surgery time is {user.surgeryDateTime}.");
                return;
            }
        }

        public void SurgeonAssigned(User user){ // method to show the assigned surgeon
            if (user.surgeonassigned == null){
                Console.WriteLine("You do not have an assigned surgeon.");
                return;
            }
            else{
                Console.WriteLine($"Your surgeon is {user.surgeonassigned}.");
                return;
            }
        }

        public void CheckIn(bool isCheckedIn, User user){  // method to check in and check out
            if (user.SurgeryPerformed == true){
                if (isCheckedIn == true){
                    user.Checked_in = false;
                    Console.WriteLine($"Patient {user.Name} has been checked out.");
                    return;
                }
                else{
                    Console.WriteLine("You are unable to check in at this time.");
                    return;
                }
            }
            else{
                if(!isCheckedIn){
                    user.Checked_in = true;
                    Console.WriteLine($"Patient {user.Name} has been checked in.");
                    return;
                }
                else{
                    Console.WriteLine("You are unable to check out at this time.");
                    return;
                }
            }

        }

        public void PatientSeeRoom(User user){ // method to show the room number
            if(user.Room == null){
                Console.WriteLine("You do not have an assigned room.");
                return;
            }
            else{
                Console.WriteLine($"Your room is number {user.Room} on floor {user.Floor}.");
                return;
            }
        }

        public void show_details(User user) { // method to show the user details
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
        public void change_password(User user){ // method to change the password
            Console.WriteLine("Enter new password:");
            string new_password = Console.ReadLine();
            user.Password = new_password;
            Console.WriteLine("Password has been changed.");
        }
    }
}