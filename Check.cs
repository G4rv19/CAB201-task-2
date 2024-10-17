using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
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
            List<string> emails = new List<string>();   
            foreach (var user in Register.users){
                emails.Add(user.Value.Email);
            }
            if (match.Success == true && !emails.Contains(email)) {
                return true;
            }
            else {
                if (emails.Contains(email)){
                    ErrorInvalid("Email is already registered, please try again.");
                }
                else{
                    ErrorInvalid("Supplied email is invalid, please try again.");
                }
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
            List<int> staff = new List<int>();
            foreach( var user in Register.users){
                if (user.Value.Staff_id.HasValue)
                {
                    staff.Add(user.Value.Staff_id.Value);
                }
            }
            if (staffId >= 100 && staffId <= 999) {
                if (staff.Contains(staffId)){
                    ErrorInvalid("Staff ID is already registered, please try again.");
                    return false;
                }
                else{
                    return true;
                }
            }
            else {
                ErrorInvalid("Supplied staff identification number is invalid, please try again.");
                return false;
            }
        }

        // Floor number must be between 1 and 6
        public bool FloorCheck(int floor) {
            if (floor >= 1 && floor <= 6) {
                foreach(var manager in Register.users){
                    if (manager.Value.Floor_number.HasValue && manager.Value.Floor_number == floor){
                        ErrorInvalid("Floor has been assigned to another floor manager, please try again.");
                        return false;
                    }
                }
                return true;
            }
            else {
                ErrorInvalid("Supplied floor is invalid, please try again.");
                return false;
            }
        }

        public bool DateTimeCheck(string date){
            string pattern = @"^([01]\d|2[0-3]):([0-5]\d)\s(0[1-9]|[12]\d|3[01])/(0[1-9]|1[0-2])/(\d{4})$";
            Match match = Regex.Match(date, pattern);
            if (match.Success == true) {
                return true;
            }
            else{
                return false;
            }
        }

        public bool RoomCheck(int room, User user){
            if (room < 1 || room > 10){
                return false;
            }
            else{
                foreach(var patient in Register.users){
                    if (patient.Value.Room == room && patient.Value.Email == user.Email){
                        ErrorInvalid("Supplied value is out of range, please try again.");
                        return false;
                    }
                }
            }
            return true;
        }

        public void ErrorInvalid(string message) {
            Console.WriteLine("#####");
            Console.WriteLine($"#Error - {message}");
            Console.WriteLine("#####");
        }
    }

}