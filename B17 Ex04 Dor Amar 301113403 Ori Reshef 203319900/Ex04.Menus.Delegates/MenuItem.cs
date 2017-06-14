using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public delegate void PickNotifier();

    public class MenuItem
    {
        private const int v_Quit = 0;
        private MenuItem m_ParentItem;
        private List<MenuItem> m_ChildItems;
        private string m_Option;
        private bool m_IsActionItem = false;

        // Delegates
        public event PickNotifier m_PickNotifier;

        // Getters 
        public string Option { get => m_Option; }

        public List<MenuItem> ChildItems { get => m_ChildItems; }

        public bool IsActionItem { get => m_IsActionItem; }        

        // Methods
        public MenuItem(string i_Option, MenuItem i_ParentItem, bool i_IsActionItem)
        {
            m_Option = i_Option;
            m_ParentItem = i_ParentItem;
            m_IsActionItem = i_IsActionItem;
            m_ChildItems = new List<MenuItem>();
        }

        public void show()
        {
            ushort choice = v_Quit;
            do
            {
                Console.Clear();
                Console.WriteLine("========== {0} ==========", m_Option);
                displayOptions();
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
                        chosenMenuItem.OnPick();
                        Console.ReadLine();
                    }
                    else
                    {
                        chosenMenuItem.show();
                    }
                }
            }
            while (choice != v_Quit);
        }

        public void displayOptions()
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
            ushort usersChoice = ushort.Parse(input); // TODO: handle empty strings and other things
            while (isValidInput(usersChoice) == false)
            {
                Console.WriteLine("Invalid Input, Please re-enter your choice:");
                usersChoice = ushort.Parse(input);
            }

            return usersChoice;
        }

        private bool isValidInput(ushort i_UsersChoice)
        {
            return i_UsersChoice >= 0 && i_UsersChoice <= m_ChildItems.Count;
        }

        protected virtual void OnPick()
        {
            if (m_PickNotifier != null)
            {
                m_PickNotifier.Invoke();
            }
        }
    }
}
