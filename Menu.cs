using System;
using System.Reflection.Metadata.Ecma335;

namespace Myapp {
    public class Menu {
        public void RunMenu() {
            MenuDisplay menuDisplay = new MenuDisplay();
            Console.WriteLine(menuDisplay.Returnmenu());

        }
    }

    public class MenuDisplay {
        private string menu = "Hello";
        public string Returnmenu() {
            return menu;
        }
    }
}