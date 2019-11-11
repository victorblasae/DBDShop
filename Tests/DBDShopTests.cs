using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBDShopLib;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class DBDShopTests
    {
        [TestMethod]
        public void AddAndTestData()
        {
           //Connect to the test database
            Client client= new Client("JpczLFGlL0", "JpczLFGlL0", "TjTWsNqWOZ");
            //Get all the existing products
            List<Product> products = client.GetProducts();

            //Delete all the products
            client.DeleteProducts(products);
            //Check we deleted all the products
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 0);

            //Insert test data
            client.InsertTestData();
            //Check they were correctly inserted
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 2);

            //addNewProduct
            client.addNewProduct( "Pera", 0, 1);
            //Check correctly created
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 3);
            products = client.getSoldOut();
            Assert.IsTrue(products.Count == 1);
           // List<Order> orders = new List<Order>();
            //client.addOrder("123456789S", 1, 3);
            //orders = client.GetOrders();
            //Assert.IsTrue(orders.Count == 1);

        }
        [TestMethod]
        public void MyOhterTest()
        {
            //Connect to the test database
            //Client client = new Client("NLphb4HrH0", "NLphb4HrH0", "VM8GYV3qZ7");
            
            //Any testing you need to do
            //....
        }
    }
}
