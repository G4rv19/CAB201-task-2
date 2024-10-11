using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class Constants {
        public void PatientMenu(User user){
            Console.WriteLine("Patient Menu");
            Console.WriteLine("Please choose from the menu below:");
            Console.WriteLine("1. Display my details");
            Console.WriteLine("2. Change password");
            if (user.SurgeryPerformed == true){
                if (user.Checked_in == true){
                    Console.WriteLine("3. Check out");
                }
                else{
                    Console.WriteLine("3. Check in");
                }
            }
            else{
                if (!user.Checked_in){
                    Console.WriteLine("3. Check in");
                }
                else{
                    Console.WriteLine("3. Check out");
                }
            }
            Console.WriteLine("4. See room");
            Console.WriteLine("5. See surgeon");
            Console.WriteLine("6. See surgery date and time");
            Console.WriteLine("7. Log out");
            Console.WriteLine("Please enter a choice between 1 and 7.");
            
            
        }
    }
}