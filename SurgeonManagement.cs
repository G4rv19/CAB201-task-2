using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class SurgeonManagement {
        private List<string> SurgeryPatients = new List<string>(); // list of all patients who are checked in and have a surgery scheduled
        private List<string> PatientAssignedToSurgeon = new List<string>(); // list of all patients assigned to the surgeon
        ManagementTools managementTools = new ManagementTools();
        private User user; // current user
        Check check = new Check(); // object to check for invalid inputs
        UserInputService userInputService = new UserInputService(); // object to get user inputs
        public SurgeonManagement(User user){
            this.user = user;
        }

        public void PatientAssignedForSurgeon(User user){ //  method to assign patient to surgeon
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

        public void SeeSurgery(User user){ // method to see the surgery schedule
        Console.WriteLine("Your schedule.");

        // Retrieve patients assigned to the surgeon
        managementTools.CheckedInPatientList(patient => patient.surgeonassigned == user.Name, SurgeryPatients);

        if (SurgeryPatients.Count == 0) {
            Console.WriteLine("You do not have any patients assigned.");
            return;
        }

        // List to hold valid patients with surgery dates
        List<User> validPatients = new List<User>();

        int index = 1;
        foreach (string patientEmail in SurgeryPatients) {
            string selectedPatientEmail = managementTools.patientEmailMap[index];
            User selectedPatient = Register.GetUser(selectedPatientEmail);

            // Check if surgeryDateTime is valid before adding
            if (!string.IsNullOrEmpty(selectedPatient.surgeryDateTime)) {
                validPatients.Add(selectedPatient);
            }

            index++;
        }

        // Sort patients by surgery date (ascending order, i.e., earliest first)
        var sortedPatients = validPatients.OrderBy(patient => DateTime.ParseExact(patient.surgeryDateTime, "HH:mm dd/MM/yyyy", null)).ToList();

        // Display the sorted surgery schedule
        index = 1;
        foreach (User sortedPatient in sortedPatients) {
            Console.WriteLine($"Performing surgery on patient {sortedPatient.Name} on {sortedPatient.surgeryDateTime}");
            index++;
        }

        return;
    }


        public void PerformSurgery(User user){ // method to perform surgery
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
                    choice = userInputService.GetIntInput() ?? 0;
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