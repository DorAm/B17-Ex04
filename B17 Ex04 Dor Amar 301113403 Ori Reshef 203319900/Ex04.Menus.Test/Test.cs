using System;
using System.Text.RegularExpressions;
using System.Threading;
using Ex04.Menus.Interfaces;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    public class Test : IPickObserver
    {
        [Flags]
        private enum eOptions
        {
            DisplayVersion = 1,
            CountSpaces,
            CharsCount,
            ShowDate,
            ShowTime
        }

        private Interfaces.MainMenu m_InterfacesMainMenu;
        private Delegates.MainMenu m_DelegatesMainMenu;

        public Interfaces.MainMenu InterfacesMainMenu
        {
            get { return m_InterfacesMainMenu; }
        }

        public Delegates.MainMenu DelegatesMainMenu
        {
            get { return m_DelegatesMainMenu; }
        }

        public Test()
        {
            m_InterfacesMainMenu = new Interfaces.MainMenu();
            initInterfacesMenus();

            m_DelegatesMainMenu = new Delegates.MainMenu();
            initDelegatesMenus();
        }

        private void initInterfacesMenus()
        {
            m_InterfacesMainMenu.AddMenuItem("Main Menu", "Actions and Info");
            m_InterfacesMainMenu.AddMenuItem("Actions and Info", "Display Version", this);
            m_InterfacesMainMenu.AddMenuItem("Actions and Info", "Actions");
            m_InterfacesMainMenu.AddMenuItem("Actions", "Count Spaces", this);
            m_InterfacesMainMenu.AddMenuItem("Actions", "Chars Count", this);
            m_InterfacesMainMenu.AddMenuItem("Main Menu", "Show Date/Time");
            m_InterfacesMainMenu.AddMenuItem("Show Date/Time", "Show Time", this);
            m_InterfacesMainMenu.AddMenuItem("Show Date/Time", "Show Date", this);
        }

        private void initDelegatesMenus()
        {
            m_DelegatesMainMenu.AddMenuItem("Main Menu", "Actions and Info");
            m_DelegatesMainMenu.AddMenuItem("Actions and Info", "Display Version", new PickHandler(MenuItem_DisplayVersion));
            m_DelegatesMainMenu.AddMenuItem("Actions and Info", "Actions");
            m_DelegatesMainMenu.AddMenuItem("Actions", "Count Spaces", new PickHandler(MenuItem_CountSpaces));
            m_DelegatesMainMenu.AddMenuItem("Actions", "Chars Count", new PickHandler(MenuItem_CharsCount));
            m_DelegatesMainMenu.AddMenuItem("Main Menu", "Show Date/Time");
            m_DelegatesMainMenu.AddMenuItem("Show Date/Time", "Show Time", new PickHandler(MenuItem_ShowTime));
            m_DelegatesMainMenu.AddMenuItem("Show Date/Time", "Show Date", new PickHandler(MenuItem_ShowDate));
        }

        // TODO: ORI - neads exception handling
        public void ReportPicked(string i_Option)
        {
            bool v_IgnoreCase = true;
            eOptions chosenOption = (eOptions)Enum.Parse(typeof(eOptions), i_Option.Replace(" ", string.Empty), v_IgnoreCase);
            routeToFunction(chosenOption);
        }

        private void routeToFunction(eOptions i_ChosenOption)
        {
            switch (i_ChosenOption)
            {
                case eOptions.CharsCount:
                    MenuItem_CharsCount();
                    break;
                case eOptions.CountSpaces:
                    MenuItem_CountSpaces();
                    break;
                case eOptions.DisplayVersion:
                    MenuItem_DisplayVersion();
                    break;
                case eOptions.ShowDate:
                    MenuItem_ShowDate();
                    break;
                case eOptions.ShowTime:
                    MenuItem_ShowTime();
                    break;
                default:
                    break;
            }
        }

        private void MenuItem_DisplayVersion()
        {
            Console.WriteLine("App Version: 17.2.4.0");
        }

        private void MenuItem_CountSpaces()
        {
            Console.WriteLine("Please enter a sentence");
            string userInput = Console.ReadLine();
            int occurences = Regex.Matches(userInput, " ").Count;
            Console.WriteLine("There are {0} whitespaces", occurences);
        }

        private void MenuItem_CharsCount()
        {
            Console.WriteLine("Please enter a sentence");
            string userInput = Console.ReadLine();
            Console.WriteLine("There are {0} characters", userInput.Length);
        }

        private void MenuItem_ShowDate()
        {
            DateTime today = DateTime.Today;
            Console.WriteLine("Today is: {0}", today.ToString("d"));
        }

        private void MenuItem_ShowTime()
        {
            DateTime localDate = DateTime.Now;
            Console.WriteLine("The time is: {0}", localDate.ToString("h:mm:ss tt"));
        }
    }
}
