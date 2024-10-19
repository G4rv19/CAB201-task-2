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
        
        // Retrieve patients assigned to the surgeon
        managementTools.CheckedInPatientList(patient => patient.surgeonassigned == user.Name, SurgeryPatients);
        
        if (SurgeryPatients.Count == 0) {
            Console.WriteLine("You do not have any patients assigned.");
            return;
        }
        
        // List to hold valid patients with surgery dates
        List<User> validPatients = new List<User>();

        // Process each patient, check for nulls and add valid ones to the list
        foreach (string patientEmail in SurgeryPatients) {
            User patient = Register.GetUser(patientEmail);

            if (patient != null && !string.IsNullOrEmpty(patient.surgeryDateTime)) {
                // Try to parse the surgery date to ensure it's in the correct format
                DateTime surgeryDate;
                if (DateTime.TryParseExact(patient.surgeryDateTime, "HH:mm dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out surgeryDate)) {
                    validPatients.Add(patient);
                }
            }
        }

        // Sort patients by surgery date in ascending order (soonest surgery first)
        var sortedPatients = validPatients.OrderBy(patient => DateTime.ParseExact(patient.surgeryDateTime, "HH:mm dd/MM/yyyy", null)).ToList();
        
        // Display the sorted surgery schedule
        int index = 1;
        foreach (User selectedPatient in sortedPatients) {
            Console.WriteLine($"Performing surgery on patient {selectedPatient.Name} on {selectedPatient.surgeryDateTime}");
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