using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class ManagementTools {
        public Dictionary<int, string> patientEmailMap = new Dictionary<int, string>();
        public void CheckedInPatientList(Func<User, bool> condition, List<string> SavingList){
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

        public void ShowSurgeryDate(User user){
            if (user.surgeryDateTime == null){
                Console.WriteLine("You do not have assigned surgery.");
                return;
            }
            else{
                Console.WriteLine($"Your surgery time is {user.surgeryDateTime}.");
                return;
            }
        }

        public void SurgeonAssigned(User user){
            if (user.surgeonassigned == null){
                Console.WriteLine("You do not have an assigned surgeon.");
                return;
            }
            else{
                Console.WriteLine($"Your surgeon is {user.surgeonassigned}.");
                return;
            }
        }

        public void CheckIn(bool isCheckedIn, List<string> menu, User user){
            if (user.SurgeryPerformed == true){ 
                if (isCheckedIn){
                    Console.WriteLine("You cant check in rn.");
                    return;
                }
                else if (!isCheckedIn){
                    Console.WriteLine("You cant check out rn.");
                    return;
                }
            }
            else if (user.SurgeryPerformed == false){
            if (isCheckedIn){
                menu[2] = "3. Check out";
            }
            else if (!isCheckedIn){
                menu[2] = "3. Check in";
            }
        }
        }

        public void PatientSeeRoom(User user){
            if(user.Room == null){
                Console.WriteLine("You do not have an assigned room.");
                return;
            }
            else{
                Console.WriteLine($"Your room is number {user.Room} on floor {user.Floor}.");
                return;
            }
        }
    }
}