using System;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        // Members
        private MenuItem m_RootMenuItem;

        public MainMenu()
        {
            MenuItem orphan = null;
            m_RootMenuItem = new MenuItem("Main Menu", orphan);
        }

        // Methods
        public void show()
        {
            m_RootMenuItem.show();
        }

        // For Action Item:
        public void addMenuItem(string i_ParentNodeName, string i_OptionName, IPickObserver i_PickObserver)
        {
            MenuItem parent = find(i_ParentNodeName, m_RootMenuItem);
            MenuItem newNode = new MenuItem(i_OptionName, parent, i_PickObserver);
            parent.ChildItems.Add(newNode);
        }

        // For Sub-Menu Item:
        public void addMenuItem(string i_ParentNodeName, string i_OptionName)
        {
            MenuItem parent = find(i_ParentNodeName, m_RootMenuItem);
            MenuItem newNode = new MenuItem(i_OptionName, parent);
            parent.ChildItems.Add(newNode);
        }

        public void addMenuItems(string i_ParentNodeName, params string[] i_OptionNames)
        {
            MenuItem parent = find(i_ParentNodeName, m_RootMenuItem);
            foreach (string option in i_OptionNames)
            {
                addMenuItem(i_ParentNodeName, option);                
            }            
        }

        private MenuItem find(string i_ParentNodeName, MenuItem i_MenuItemNode)
        {
            MenuItem resItem = null;

            if (i_MenuItemNode.IsActionItem)
            {
                resItem = null;
            }
            else if (i_MenuItemNode.Option == i_ParentNodeName)
            {
                resItem = i_MenuItemNode;
            }
            else
            {
                foreach (MenuItem itemNode in i_MenuItemNode.ChildItems)
                {
                    resItem = find(i_ParentNodeName, itemNode);
                }       
            }

            return resItem;
        }                            
    }
}
