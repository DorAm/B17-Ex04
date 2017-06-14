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
            m_DelegatesMainMenu.AddMenuItem("Actions and Info", "Display Version", new PickHandler(MenuItemDisplayVersion_Pick));
            m_DelegatesMainMenu.AddMenuItem("Actions and Info", "Actions");
            m_DelegatesMainMenu.AddMenuItem("Actions", "Count Spaces", new PickHandler(MenuItemCountSpaces_Pick));
            m_DelegatesMainMenu.AddMenuItem("Actions", "Chars Count", new PickHandler(MenuItemCharsCount_Pick));
            m_DelegatesMainMenu.AddMenuItem("Main Menu", "Show Date/Time");
            m_DelegatesMainMenu.AddMenuItem("Show Date/Time", "Show Time", new PickHandler(MenuItemShowTime_Pick));
            m_DelegatesMainMenu.AddMenuItem("Show Date/Time", "Show Date", new PickHandler(MenuItemShowDate_Pick));
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
                    MenuItemCharsCount_Pick();
                    break;
                case eOptions.CountSpaces:
                    MenuItemCountSpaces_Pick();
                    break;
                case eOptions.DisplayVersion:
                    MenuItemDisplayVersion_Pick();
                    break;
                case eOptions.ShowDate:
                    MenuItemShowDate_Pick();
                    break;
                case eOptions.ShowTime:
                    MenuItemShowTime_Pick();
                    break;
                default:
                    break;
            }
        }

        private void MenuItemDisplayVersion_Pick()
        {
            Console.WriteLine("App Version: 17.2.4.0");
        }

        private void MenuItemCountSpaces_Pick()
        {
            Console.WriteLine("Please enter a sentence");
            string userInput = Console.ReadLine();
            int occurences = Regex.Matches(userInput, " ").Count;
            Console.WriteLine("There are {0} whitespaces", occurences);
        }

        private void MenuItemCharsCount_Pick()
        {
            Console.WriteLine("Please enter a sentence");
            string userInput = Console.ReadLine();
            Console.WriteLine("There are {0} characters", userInput.Length);
        }

        private void MenuItemShowDate_Pick()
        {
            DateTime today = DateTime.Today;
            Console.WriteLine("Today is: {0}", today.ToString("d"));
        }

        private void MenuItemShowTime_Pick()
        {
            DateTime localDate = DateTime.Now;
            Console.WriteLine("The time is: {0}", localDate.ToString("h:mm:ss tt"));
        }
    }
}
