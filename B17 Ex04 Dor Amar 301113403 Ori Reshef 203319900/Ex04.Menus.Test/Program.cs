using System;

namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            displayMenus();
        }

        private static void displayMenus()
        {
            Test menusTest = new Test();
            menusTest.InterfacesMainMenu.Show();
            Console.WriteLine("Press any key to proceed to the Delegates Main Menu");
            Console.ReadLine();
            menusTest.DelegatesMainMenu.Show();
        }
    }
}
