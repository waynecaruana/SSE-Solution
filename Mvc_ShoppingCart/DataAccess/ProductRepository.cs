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

         /// <summary>
         /// This method is used to add a product
         /// </summary>
         /// <param name="entry">a product enrty</param>
         public void AddProduct(Product entry)
         {

             entities.AddToProducts(entry);
             entities.SaveChanges();

         }

         /// <summary>
         /// This method allow us to update a product
         /// </summary>
         /// <param name="p">product</param>
         public void UpdateProduct(Product p)
         {
             entities.Products.Attach(GetProductsByID(p.ID));
             entities.Products.ApplyCurrentValues(p);

             entities.SaveChanges();
         }

         /// <summary>
         /// this method is used to delete a product
         /// </summary>
         /// <param name="entry">product</param>
         public void DeleteProduct(Product entry)
         {

             entities.Products.DeleteObject(entry);
             entities.SaveChanges();

         }
    }
}
