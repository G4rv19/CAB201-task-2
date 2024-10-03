using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Myapp {
    public class Management {
        public void CheckIn(bool check_in, List<string> menu){
            if (check_in){
                menu[2] = "3. Check out";
            }
            else if (!check_in){
                menu[2] = "3. Check in";
            }
        }
    }
}