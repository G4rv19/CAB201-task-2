using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
namespace Myapp{
    public class UserInputService{
        Check check = new Check();
        public string? GetStringInput(){
            return Console.ReadLine();
        }
        public int? GetIntInput(){
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result)){
                return result;
            }
            else{
                return null;
            }

        }

    }
}
