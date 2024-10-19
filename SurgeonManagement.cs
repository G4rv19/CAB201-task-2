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
        Check check = new Check();
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

            // Filter patients assigned to the surgeon
            managementTools.CheckedInPatientList(patient => patient.surgeonassigned == user.Name, SurgeryPatients);

            if (SurgeryPatients == null || SurgeryPatients.Count == 0){
                Console.WriteLine("You do not have any patients assigned.");
                return;
            }

            // Sort patients by surgery date in descending order with null checks
            var sortedPatients = SurgeryPatients
                .Where(patientEmail => !string.IsNullOrEmpty(patientEmail)) // Ensure the email is not null
                .OrderByDescending(patientEmail => {
                    var patient = Register.GetUser(patientEmail);
                    if (patient == null || string.IsNullOrEmpty(patient.surgeryDateTime)) {
                        // Handle invalid patients or surgery dates
                        return DateTime.MinValue; // or skip this patient based on your logic
                    }
                    return DateTime.ParseExact(patient.surgeryDateTime, "HH:mm dd/MM/yyyy", null);
                })
                .ToList();

            // Display the sorted list
            int index = 1;
            foreach (var patientEmail in sortedPatients){
                var selectedPatient = Register.GetUser(patientEmail);
                if (selectedPatient != null) {
                    Console.WriteLine($"{index}. Performing surgery on patient {selectedPatient.Name} on {selectedPatient.surgeryDateTime}");
                }
                index++;
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
                int choice = 0;
                bool valid = false;
                while(!valid){
                    Console.WriteLine($"Please enter a choice between 1 and {PatientAssignedToSurgeon.Count}.");
                    choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 && choice <= PatientAssignedToSurgeon.Count){
                        string patientEmail = managementTools.patientEmailMap[choice];
                        User selectedPatient = Register.GetUser(patientEmail);
                        selectedPatient.SurgeryPerformed = true;
                        selectedPatient.surgeryDateTime = null;
                        selectedPatient.surgeonassigned = null;
                        Console.WriteLine($"Surgery performed on {selectedPatient.Name} by {user.Name}.");
                        valid = true;
                        return;
                    }
                    else{
                        check.ErrorInvalid("Supplied value is out of range, please try again.");
                        valid = false;
                    }
                }
            }

        }

    }
}