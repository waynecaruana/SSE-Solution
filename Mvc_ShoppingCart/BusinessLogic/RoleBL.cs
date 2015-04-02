using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class RoleBL
    {
        public IEnumerable<Role> GetUserRoles(string email)
        {

            return new RoleRepository().GetUserRoles(email);
        }
    }
}
