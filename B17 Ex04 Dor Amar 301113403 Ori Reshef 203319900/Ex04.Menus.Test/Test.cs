using System;
using System.Text.RegularExpressions;
using System.Threading;
using Ex04.Menus.Interfaces;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    public class Test : IPickObserver
    {
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
            m_InterfacesMainMenu.addMenuItem("Main Menu", "Actions and Info");
            m_InterfacesMainMenu.addMenuItem("Actions and Info", "Display Version", this);
            m_InterfacesMainMenu.addMenuItem("Actions and Info", "Actions");
            m_InterfacesMainMenu.addMenuItem("Actions", "Count Spaces", this);
            m_InterfacesMainMenu.addMenuItem("Actions", "Chars count", this);
            m_InterfacesMainMenu.addMenuItem("Main Menu", "Show Date/Time");
            m_InterfacesMainMenu.addMenuItem("Show Date/Time", "Show Time", this);
            m_InterfacesMainMenu.addMenuItem("Show Date/Time", "Show Date", this);
        }

        private void initDelegatesMenus()
        {
            m_DelegatesMainMenu.addMenuItem("Main Menu", "Actions and Info");
            m_DelegatesMainMenu.addMenuItem("Actions and Info", "Display Version", new PickNotifier(displayVersion));
            m_DelegatesMainMenu.addMenuItem("Actions and Info", "Actions");
            m_DelegatesMainMenu.addMenuItem("Actions", "Count Spaces", new PickNotifier(countSpaces));
            m_DelegatesMainMenu.addMenuItem("Actions", "Chars count", new PickNotifier(charsCount));
            m_DelegatesMainMenu.addMenuItem("Main Menu", "Show Date/Time");
            m_DelegatesMainMenu.addMenuItem("Show Date/Time", "Show Time", new PickNotifier(showTime));
            m_DelegatesMainMenu.addMenuItem("Show Date/Time", "Show Date", new PickNotifier(showDate));
        }

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
                    charsCount();
                    break;
                case eOptions.CountSpaces:
                    countSpaces();
                    break;
                case eOptions.DisplayVersion:
                    displayVersion();
                    break;
                case eOptions.ShowDate:
                    showDate();
                    break;
                case eOptions.ShowTime:
                    showTime();
                    break;
                default:
                    break;
            }
        }

        private void displayVersion()
        {
            Console.WriteLine("App Version: 17.2.4.0");
        }

        private void countSpaces()
        {
            Console.WriteLine("Please enter a sentence");
            string userInput = Console.ReadLine();
            int occurences = Regex.Matches(userInput, " ").Count;
            Console.WriteLine("There are {0} whitespaces", occurences);
        }

        private void charsCount()
        {
            Console.WriteLine("Please enter a sentence");
            string userInput = Console.ReadLine();
            Console.WriteLine("There are {0} characters", userInput.Length);
        }

        private void showDate()
        {
            DateTime today = DateTime.Today;
            Console.WriteLine("Today is: {0}", today.ToString("d"));
        }

        private void showTime()
        {
            DateTime localDate = DateTime.Now;
            Console.WriteLine("The time is: {0}", localDate.ToString("h:mm:ss tt"));
        }
    }
}
