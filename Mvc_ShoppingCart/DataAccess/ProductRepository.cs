using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class ProductRepository :  ConnectionClass
    {
         public ProductRepository()
            : base()
        {
        }

         public IEnumerable<Product> GetProductsByGenre(string genre)
         {
             Genre g = GetGenreByName(genre);//get user
             return g.Products.ToList();//get the list of users
         }

         public Product GetProductsByID(int id)
         {
             return entities.Products.SingleOrDefault(p => p.ID == id);
         }


         public Genre GetGenreByName(string genre)
         {

             return entities.Genres.SingleOrDefault(g => g.Name == genre);

         }

         public IEnumerable<Product> GetAllProducts()
         {
             return entities.Products.ToList();
         }

         public IEnumerable<Product> GetProductsBySellerEmail(string sellerEmail)
         {
             return entities.Products.Where(p => p.SellerEmail == sellerEmail);
         }
    }
}
