﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class RoleRepository : ConnectionClass
    {
        public RoleRepository()
            : base()
        {
        }

        /// <summary>
        /// this allow us to get a role by id
        /// </summary>
        /// <param name="id">id of a role PK</param>
        /// <returns>a single role entity</returns>
        public Role GetRoleByID(int id)
        {
            
            return entities.Roles.SingleOrDefault(r => r.RoleID == id);

        }

        /// <summary>
        /// this method one can allocate a role
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="role">role of user</param>
        public void AllocateRole(User user, Role role, bool option)
        {
         

                if (option == true)//allocate
                {
                    user.Roles.Add(role);
                }
                else if (option == false) //Deallocate
                {
                    user.Roles.Remove(role);
                }
                entities.SaveChanges();
          
        }

        public IEnumerable<Role> GetUserRoles(string email)
        {

            User u = new UserRepository().GetUserByEmail(email);//get user
            return u.Roles.ToList();//get the list of users
        }
    }
}
