using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class ProductBL
    {
        public IEnumerable<Product> GetProductsByGenre(string genre)
        {
            return new ProductRepository().GetProductsByGenre(genre);
        }

        public Product GetProductsByID(int id)
        {
            return new ProductRepository().GetProductsByID(id);
        }

        public IEnumerable<Product> ProductManagment(string email)
        {
            User u = new UserBL().GetUserByEmail(email);
            List<Role> roles =  u.Roles.ToList();
            bool admin = false;

            foreach (Role r in roles)
            {
                if (r.Name == "Admin")
                {
                    admin = true;
                }
            }

            if (admin)//if admin return all products if seller return his products
            {
                return new ProductRepository().GetAllProducts();
            }
            else return new ProductRepository().GetProductsBySellerEmail(email);
        }
    }
}
