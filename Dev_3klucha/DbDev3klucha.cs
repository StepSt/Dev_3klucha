using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dev_3klucha
{
    public class DbDev3klucha
    {
        //public List<Product> GetProductEf()
        //{
        //    var context = new Dev_3kluchaEntities();
        //    var customer = context.Product.ToList();
        //    return customer;
        //}
        public C3kuhca_product GetProductEf_id(int id)
        {
            var context = new DevEngInt_BaseEntities();
            C3kuhca_product castomer = context.C3kuhca_product.Single(c => c.ProductId == id);
            return castomer;
        }
        //public void SetProductEf_id(int id, double price)
        //{
        //    var context = new Dev_3kluchaEntities();
        //    var castomer = context.Product.Single(c => c.id == id);
        //    castomer.Price = price;
        //    context.SaveChanges();
        //}

        //public void SetCustomerUser_id(string name)
        //{
        //    var context = new Dev_3kluchaEntities();
        //    Customer user = new Customer();
        //    //user.id = 1;
        //    user.Name = name;
        //    var castomer = context.Customer.Add(user);
        //    context.SaveChanges();
        //}
    }
}