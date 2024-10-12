using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
namespace Myapp {
    public class Check {
    // Name must consist of only letters and spaces, at least one letter
        public bool NameCheck(string name) {
            string pattern = @"^[A-Za-z]+(?:\s[A-Za-z]+)*$";  // Allows letters and spaces
            Match match = Regex.Match(name, pattern);
            if (match.Success == true) {
                return true;
            }
            else {
            return false;
            }
        }

        // Age check based on user type
        public bool AgeCheck(string userRole, int age) {
            bool result = false;
             if (userRole == "Floor") {
                if(age >= 21 && age <= 70){
                    result = true;
                    return true;
                }
                else{
                    result = false;
                    return false;
                }
            } else if (userRole == "Surgeon") {
                if(age >= 30 && age <= 75){
                    result = true;
                    return true;
                }
                else{
                    result = false;
                    return false;
                }
            }
            else{
                if (age >= 0 && age <= 100){
                    result = true;
                    return true;
                }
                else{
                    result = false;
                }
            }
            return result;
        }

        // Mobile number must be exactly 10 digits long and start with a 0
        public bool MobileCheck(string mobile) {
            string pattern = @"^0\d{9}$";  // Leading 0 followed by exactly 9 digits
            Match match = Regex.Match(mobile, pattern);
            if (match.Success == true) {
                return true;
            }
            else {
                return false;
            }
        }

        // Email must contain an "@" with at least one character on both sides
        public bool EmailCheck(string email) {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";  // Basic email format check
            Match match = Regex.Match(email, pattern);
            if (match.Success == true) {
                return true;
            }
            else {
                return false;
            }
        }

        // Password must be at least 8 characters long and contain mixed case and numbers
        public bool PasswordCheck(string password) {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$";  // At least 1 lower, 1 upper, 1 digit, and 8+ chars
            Match match = Regex.Match(password, pattern);
            if (match.Success == true) {
                return true;
            }
            else {
                return false;
            }
        }

        // Staff ID must be between 100 and 999
        public bool StaffIdCheck(int staffId) {
            if (staffId >= 100 && staffId <= 999) {
                return true;
            }
            else {
                return false;
            }
        }

        // Floor number must be between 1 and 6
        public bool FloorCheck(int floor) {
            if (floor >= 1 && floor <= 6) {
                return true;
            }
            else {
                return false;
            }
        }

        public void ErrorInvalid(string message) {
            Console.WriteLine("#####");
            Console.WriteLine($"#Error - {message}");
            Console.WriteLine("#####");
        }
    }

}