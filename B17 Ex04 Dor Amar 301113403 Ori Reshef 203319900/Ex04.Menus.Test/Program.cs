using System;

namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            Test menusTest = new Test();   
            menusTest.InterfacesMainMenu.show();
            Console.WriteLine("Press any key to proceed to the Delegates Main Menu");
            Console.ReadLine();
            menusTest.DelegatesMainMenu.show();
        }
    }
}
