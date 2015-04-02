using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class MenuRepository : ConnectionClass
    {
         public MenuRepository()
            : base()
        {
        }

        public IEnumerable<Menu> GetMenus(int roleId)
        {
            //users, menus, roles
            var list =
                (
                    from r in entities.Roles
                    from m in r.Menus
                    where r.RoleID == roleId
                    select m
                );

            return list.Distinct().AsQueryable();
        }
    }
}
