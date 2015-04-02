using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class UserRepository : ConnectionClass
    {
        public UserRepository()
            : base()
        {
        }


        /// <summary>
        /// This method is used to register a user
        /// </summary>
        /// <param name="entry">user details</param>
        public void AddUser(User entry)
        {

            entities.AddToUsers(entry);
            entities.SaveChanges();

        }

        /// <summary>
        /// This method will check if email exists
        /// </summary>
        /// <param name="email">email address</param>
        /// <returns>a user if exist, otherwise null</returns>
        public User CheckIfEmailExists(string email)
        {

            return entities.Users.SingleOrDefault(u => u.Email == email);

        }

        /// <summary>
        /// this method will check if user exists in order to login
        /// </summary>
        /// <param name="username">email</param>
        /// <param name="password">password</param>
        /// <returns>null or a user</returns>
        public User AuthenticateUserByUsernameAndPassword(string email, string password)
        {
            return entities.Users.SingleOrDefault(x => x.Email == email && x.Password == password);

        }

        //method to get an entry by id
        public User GetUserByEmail(string email)
        {
            return entities.Users.SingleOrDefault(x => x.Email == email);
        }

       
    }
}
