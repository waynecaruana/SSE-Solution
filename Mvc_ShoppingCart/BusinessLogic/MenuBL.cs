using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class MenuBL
    {
        public IEnumerable<Menu> GetMenus(int roleId)
        {
            return new MenuRepository().GetMenus(roleId);
        }
    }
}
