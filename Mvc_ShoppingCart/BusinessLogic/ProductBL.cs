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

        /// <summary>
        /// This method will add a product to DB
        /// </summary>
        /// <param name="name">product name</param>
        /// <param name="desc">product description</param>
        /// <param name="image">product image</param>
        /// <param name="stock">product quantity in stock</param>
        /// <param name="price">product price</param>
        public void AddProduct(string name, string desc, string image, int stock, decimal price, string email, string genre)
        {


            User u = new UserRepository().GetUserByEmail(email);

            Product p = new Product();
            p.Name = name;
            p.Description = desc;
            p.ImagePath = image;
            p.Stock = stock;
            p.Price = price;
            p.DateListed = DateTime.Now.Date;
            p.SellerEmail = u.Email;
            p.GenreID = Convert.ToInt32(genre);

            new ProductRepository().AddProduct(p);

        }

        /// <summary>
        /// A method to update a product
        /// </summary>
        /// <param name="name">product name</param>
        /// <param name="desc">product description</param>
        /// <param name="image">product image</param>
        /// <param name="stock">product stock</param>
        /// <param name="price">product price</param>
        /// <param name="email">user email</param>
        public void UpdateProduct(int id, string name, string desc, string image, int stock, decimal price, string email, string genre)
        {
            User u = new UserRepository().GetUserByEmail(email);

            Product p = new Product();
            p.ID = id;
            p.Name = name;
            p.Description = desc;
            p.ImagePath = image;
            p.Stock = stock;
            p.Price = price;
            p.DateListed = DateTime.Now.Date;
            p.SellerEmail = email;
            p.GenreID = Convert.ToInt32(genre);

            new ProductRepository().UpdateProduct(p);
        }

        /// <summary>
        /// This method is used to delete a product
        /// </summary>
        /// <param name="id">product id</param>
        public void DeleteProduct(int id)
        {
            ProductRepository pr = new ProductRepository();
            Product product = pr.GetProductsByID(id);//get the user by username
            pr.DeleteProduct(product);
        }
    }
}
