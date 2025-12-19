using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task06_12
{
    public class ProductService
    {
        
        private const string productPath = "products.json";

        public List<Products> GetAll()
        {
            return JsonHelper.Read<Products>(productPath);
        }


        public void Add(Products product)
        {
            var products = JsonHelper.Read<Products>(productPath);
            product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
            products.Add(product);
            JsonHelper.Write(productPath, products);
        }
        public void Edit(int id, Products updatedProduct)
        {
            var products = JsonHelper.Read<Products>(productPath);
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                product.Ingredients = updatedProduct.Ingredients;
                JsonHelper.Write(productPath, products);
            }
        }
        public void Delete(int id)
        {
            var products = JsonHelper.Read<Products>(productPath);
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
                JsonHelper.Write(productPath, products);
            }
        }
    }
}
