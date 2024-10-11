using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class SurgeonManagement {
        public List<string> SurgeryPatients = new List<string>(); // list of all patients who are checked in and have a surgery scheduled
        public List<string> PatientAssignedToSurgeon = new List<string>();
        ManagementTools managementTools = new ManagementTools();
        private User user;
        public SurgeonManagement(User user){
            this.user = user;
        }

        public void PatientAssignedForSurgeon(User user){
            PatientAssignedToSurgeon.Clear();   
            Console.WriteLine("Your Patients.");
            managementTools.CheckedInPatientList(patient => patient.surgeonassigned == user.Name, PatientAssignedToSurgeon);
            if (PatientAssignedToSurgeon.Count == 0){
                Console.WriteLine("You do not have any patients assigned.");
                return;
            }
            else{
                int index = 1;
                foreach (string patient in PatientAssignedToSurgeon){
                    Console.WriteLine($"{index}. {patient}");
                    index++;
                }
                return;
            }

        }

        public void SeeSurgery(User user){
            Console.WriteLine("Your schedule.");
            managementTools.CheckedInPatientList(patient => patient.surgeonassigned == user.Name, SurgeryPatients);
            if (SurgeryPatients.Count == 0){
                Console.WriteLine("You do not have any patients assigned.");
                return;
            }
            else{
                int index = 1;
                foreach (string patient in SurgeryPatients){
                    string PatientEmail = managementTools.patientEmailMap[index];
                    User selectedPatient = Register.GetUser(PatientEmail);
                    Console.WriteLine($"Performing surgery on patient {selectedPatient.Name} on {selectedPatient.surgeryDateTime}");
                    index++;
                }
                return;
            }

        }

        public void PerformSurgery(User user){
            Console.WriteLine("Please select your patient: ");
            managementTools.CheckedInPatientList(patient => patient.surgeonassigned == user.Name, PatientAssignedToSurgeon);
            if (PatientAssignedToSurgeon.Count == 0){
                Console.WriteLine("You have no patients assigned.");
                return;
            }
            else{
                int index = 1;
                foreach (string patient in PatientAssignedToSurgeon){
                    Console.WriteLine($"{index}. {patient}");
                    index++;
                }
                Console.WriteLine($"Please enter a choice between 1 and {PatientAssignedToSurgeon.Count}.");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice < 1 || choice > PatientAssignedToSurgeon.Count){
                    Console.WriteLine("Invalid choice.");
                    return;
                }
                else{

                    string patientEmail = managementTools.patientEmailMap[choice];
                    User selectedPatient = Register.GetUser(patientEmail);  
                    selectedPatient.surgeryDateTime = null;
                    selectedPatient.surgeonassigned = null;
                    selectedPatient.SurgeryPerformed = true;
                    Console.WriteLine($"Surgery performed on {selectedPatient.Name} by {user.Name}.");
                    return;
                }
            }

        }

    }
}