using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Common;
using System.Security.Cryptography;

namespace BusinessLogic
{
    public class UserBL
    {
        /// <summary>
        /// This gives you all users
        /// </summary>
        /// <returns>a list of all users</returns>
        public IEnumerable<User> GetUsers()
        {
            return new UserRepository().GetUsers();
        }

        public void Register(string email, string password, string name, string surname)
        {

            if (new UserRepository().CheckIfEmailExists(email) == null)
            {
                int passwordComplexity = EncryptionClass.CheckPasswordStrength(password);

                if (passwordComplexity > 1)
                {
                    UserRepository ur = new UserRepository();
                    RoleRepository rr = new RoleRepository();

                    ur.entities = rr.entities;

                    //get all user details
                    User u = new User();
                    u.Email = email;
                    u.Password = EncryptionClass.Sha512encrypt(password);
                    u.Salt = EncryptionClass.GenerateSalt(34); //64bit
                    
                    //generate public and private key 
                    string privateK;
                    string publicK;
                    EncryptionClass.GeneratePublicPrivateKey(out privateK, out publicK);
                    u.PrivateKey = privateK;
                    u.PublicKey = publicK;
                    u.Firstname = name;
                    u.Lastname = surname;

                 

                    Role r = rr.GetRoleByID(2);//get role by id

                    ur.AddUser(u);
                    rr.AllocateRole(u, r, true);
                }
                else throw new MissingFieldException("Week Password");
            }
            else
            {
                throw new MissingFieldException("Email already Registered");
            }

        }

        /// <summary>
        /// This email us used for login
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>a user or null if does not exist</returns>
        public User AuthenticateUserByUsernameAndPassword(string email, string password)
        {
            return new UserRepository().AuthenticateUserByUsernameAndPassword(email, EncryptionClass.Sha512encrypt(password));
        }

        public User GetUserByEmail(string email)
        {

            return new UserRepository().GetUserByEmail(email);

        }
    }
}
