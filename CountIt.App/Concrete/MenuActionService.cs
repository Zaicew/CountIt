using CountIt.App.Common;
using CountIt.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountIt.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        //private List<MenuAction> menuActions;

        public MenuActionService()
        {
            Initialize();
        }

        //public void AddNewAction(int id, string name, string menuName)
        //{
        //   // MenuAction menuAction = new MenuAction() { Id = id, Name = name, MenuName = menuName };
        //    MenuAction menuAction = new MenuAction(id, name, menuName);
        //    menuActions.Add(menuAction);
        //}
        public List<MenuAction> GetMenuActrionsByMenuName(string menuName)
        {
            List<MenuAction> result = new List<MenuAction>();
            foreach (var menuAction in Items)
            {
                if (menuAction.MenuName == menuName)
                {
                    result.Add(menuAction);
                }
            }
            return result;
        }

        public void Initialize()
        {
            AddItem(new MenuAction(1, "Management of products", "MainMenu"));
            AddItem(new MenuAction(2, "Your calculator", "MainMenu"));
            AddItem(new MenuAction(3, "Close app", "MainMenu"));
            AddItem(new MenuAction(4, "Set some data", "MainMenu"));

            AddItem(new MenuAction(1, "Add category", "ManagementApplication"));
            AddItem(new MenuAction(2, "Add product", "ManagementApplication"));
            AddItem(new MenuAction(3, "Sign product to category", "ManagementApplication"));
            AddItem(new MenuAction(4, "Delete category", "ManagementApplication"));
            AddItem(new MenuAction(5, "Delete product", "ManagementApplication"));
            AddItem(new MenuAction(6, "Show all categories", "ManagementApplication"));
            AddItem(new MenuAction(7, "Show all products from all categories", "ManagementApplication"));
            AddItem(new MenuAction(8, "Show all products from choosen category", "ManagementApplication"));
            AddItem(new MenuAction(9, "Back", "ManagementApplication"));

            AddItem(new MenuAction(1, "Add meal day", "UserCalculator"));
            AddItem(new MenuAction(2, "Add meal", "UserCalculator"));
            AddItem(new MenuAction(3, "Add product to meal", "UserCalculator"));
            AddItem(new MenuAction(4, "Delete product from meal", "UserCalculator"));
            AddItem(new MenuAction(5, "Delete day", "UserCalculator"));
            AddItem(new MenuAction(6, "Delete meal", "UserCalculator"));
            AddItem(new MenuAction(7, "Show day calories", "UserCalculator"));
            AddItem(new MenuAction(8, "Show meal calories", "UserCalculator"));
            AddItem(new MenuAction(9, "Show all day macros", "UserCalculator"));
            AddItem(new MenuAction(0, "Back", "UserCalculator"));
        }
    }
}
