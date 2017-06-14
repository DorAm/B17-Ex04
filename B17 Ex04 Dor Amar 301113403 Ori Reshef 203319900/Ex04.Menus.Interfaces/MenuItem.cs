﻿using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private const int v_Quit = 0;
        private MenuItem m_ParentItem;
        private List<MenuItem> m_ChildItems;
        private string m_Option;
        private bool m_IsActionItem = false;
        private IPickObserver m_PickObserver;

        // Getters 
        public string Option { get => m_Option; }

        public List<MenuItem> ChildItems { get => m_ChildItems; }

        public bool IsActionItem { get => m_IsActionItem; }

        public IPickObserver PickObserver { get => m_PickObserver; }

        // Methods
        public MenuItem(string i_Option, MenuItem i_ParentItem)
        {
            m_Option = i_Option;
            m_ParentItem = i_ParentItem;
            m_ChildItems = new List<MenuItem>();
        }

        public MenuItem(string i_Option, MenuItem i_ParentItem, IPickObserver i_PickObserver)
        : this(i_Option, i_ParentItem)
        {
            m_PickObserver = i_PickObserver;
            m_IsActionItem = true;
        }

        public void Show()
        {
            ushort choice = v_Quit;
            do
            {
                Console.Clear();
                Console.WriteLine("========== {0} ==========", m_Option);
                DisplayOptions();
                choice = getUsersChoice();
                if (choice == v_Quit)
                {
                    return;
                }
                else
                {
                    MenuItem chosenMenuItem = m_ChildItems[choice - 1];
                    if (chosenMenuItem.IsActionItem)
                    {
                        chosenMenuItem.PickObserver.ReportPicked(chosenMenuItem.Option);
                        Console.ReadLine();
                    }
                    else
                    {
                        chosenMenuItem.Show();
                    }
                }
            }
            while (choice != v_Quit);
        }

        public void DisplayOptions()
        {
            if (IsActionItem == false)
            {
                Console.WriteLine("0. {0}", m_ParentItem == null ? "Exit" : "Back");
                ushort index = 1;

                foreach (MenuItem item in m_ChildItems)
                {
                    Console.WriteLine("{0}. {1}", index, item.Option);
                    index += 1;
                }

                Console.WriteLine("Please choose one of the above options");
                Console.WriteLine("======================================");
            }
        }

        private ushort getUsersChoice()
        {
            string input = Console.ReadLine();
            ushort usersChoice;
            while (validateAndParse(input, out usersChoice) == false)
            {
                Console.WriteLine("Invalid Input, Please re-enter your choice:");
                input = Console.ReadLine();
            }

            return usersChoice;
        }

        private bool validateAndParse(string i_UsersChoice, out ushort o_UsersChoice)
        {
            bool isValid = ushort.TryParse(i_UsersChoice, out o_UsersChoice);

            if (isValid)
            {
                isValid = o_UsersChoice >= 0 && o_UsersChoice <= m_ChildItems.Count;
            }

            return isValid;
        }
    }
}
