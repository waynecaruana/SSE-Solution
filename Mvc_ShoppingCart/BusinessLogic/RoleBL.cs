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

        /// <summary>
        /// This is used to get a list of roles
        /// </summary>
        /// <returns>a list of roles</returns>
        public IEnumerable<Role> GetAllRoles()
        {
            return new RoleRepository().GetAllRoles();
        }


        public void AllocateRole(int roleID, string email, string option)
        {
            UserRepository ur = new UserRepository();
            RoleRepository rr = new RoleRepository();

            ur.entities = rr.entities;

            //get all user details
            User u = ur.GetUserByEmail(email);
            Role r = ur.GetRoleByID(roleID);//get role by id

            rr.AllocateRole(u, r, option);

        }

        /// <summary>
        /// This is used to get a role by id
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns>a single role by id</returns>
        public Role GetRoleByID(int id)
        {
            return new RoleRepository().GetRoleByID(id);
        }
    }
}
