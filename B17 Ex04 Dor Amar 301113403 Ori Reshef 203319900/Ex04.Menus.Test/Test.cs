using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    class Test : IPickObserver
    {
        private enum eOptions
        {
            DisplayVersion = 1,
            CountSpaces,
            CharsCount,
            ShowDate,
            ShowTime
        }

        // TODO: 
        //1. input validations
        //2. try \ catch
        //3. testing

        private MainMenu m_MainMenu;

        public Test()
        {
            m_MainMenu = new MainMenu();
            initMenus();
            m_MainMenu.show();
        }

        private void initMenus()
        {
            m_MainMenu.addMenuItem("Main Menu", "Actions and Info");
            m_MainMenu.addMenuItem("Actions and Info", "Display Version", this);
            m_MainMenu.addMenuItem("Actions and Info", "Actions");
            m_MainMenu.addMenuItem("Actions", "Count Spaces", this);
            m_MainMenu.addMenuItem("Actions", "Chars count", this);
            m_MainMenu.addMenuItem("Main Menu", "Show Date/Time");
            m_MainMenu.addMenuItem("Show Date/Time", "Show Time", this);
            m_MainMenu.addMenuItem("Show Date/Time", "Show Date", this);
        }

        public void ReportPicked(string i_Option)
        {
            bool v_IgnoreCase = true;
            eOptions chosenOption = (eOptions)Enum.Parse(typeof(eOptions), i_Option.Replace(" ", String.Empty), v_IgnoreCase);
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
            Console.WriteLine("Today is: {0}", today.Date);
        }

        private void showTime()
        {
            DateTime today = DateTime.Today;
            Console.WriteLine("Today is: {0}", today.TimeOfDay);
        }
    }
}
