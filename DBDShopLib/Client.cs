using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DBDShopLib
{
    public class Client
    {
        MySqlConnection m_connection = null;

        public Client(string databasename , string username , string password , string server= "remotemysql.com")
        {
            m_connection = new MySqlConnection();
            m_connection.ConnectionString =
            "Server=" + server + ";" +
            "database=" + databasename + ";" +
            "UID=" + username + ";" +
            "password=" + password + ";";
            m_connection.Open();
        }

        public void InsertTestData()
        {
            string query = "CREATE TABLE IF NOT EXISTS producto (Id int,descripcion TEXT, stock int, precio float)";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
            query = "INSERT INTO producto(descripcion, stock, precio) VALUES('Nocilla', 1, 5);";
            cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
            query = "INSERT INTO producto(descripcion, stock, precio) VALUES('Patata', 1, 6);";
            cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
            
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            string query = "SELECT Id, descripcion, stock, precio FROM producto";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                int id= int.Parse(reader.GetValue(0).ToString());
                string descripcion = reader.GetValue(1).ToString();
                int stock = int.Parse(reader.GetValue(2).ToString());
                float precio = float.Parse(reader.GetValue(3).ToString());
                Product product = new Product();
                product.Id = id;
                product.descripcion= descripcion; 
                product.stock= stock;
                product.precio = precio;
                products.Add(product);
            }
            reader.Close();
            return products;
        }

        public List<Customer> GetCustomers()
        {
        List<Customer> customers = new List<Customer>();

            String query = "SELECT DNI, nombre, apellidos FROM Cliente";
            
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string DNI = reader.GetValue(0).ToString();
                string nombre = reader.GetValue(1).ToString();
                string apellidos = reader.GetValue(2).ToString();
                
              Customer customer = new Customer();
                customer.DNI = DNI;
                customer.nombre = nombre;
                customer.apellidos = apellidos;
                customers.Add(customer);
            }   
            reader.Close();
            return customers;
        }

        public List<Distributor> GetDistributors()
        {
            List<Distributor> distributors = new List<Distributor>();

            
            String query = "SELECT CIF, nombre, direccion, tlf, email FROM Distribuidor";

            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string CIF = reader.GetValue(0).ToString();
                string nombre = reader.GetValue(1).ToString();
                string direccion = reader.GetValue(2).ToString();
                int tlf = int.Parse(reader.GetValue(3).ToString());
                string email = reader.GetValue(4).ToString();

                Distributor distributor = new Distributor();

                distributor.CIF = CIF;
                distributor.nombre = nombre;
                distributor.direccion = direccion;
                distributor.tlf = tlf;
                distributor.email = email;

            }

            reader.Close();
            return distributors;
        }

        public List<Order> GetOrders()
        {
        List<Order> orders = new List<Order>();

            String query = "SELECT id, DNI, fecha FROM Pedido";
            
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader.GetValue(0).ToString());
                string DNI = reader.GetValue(1).ToString();
                DateTime fecha = DateTime.Parse(reader.GetValue(2).ToString());
                
              Order order = new Order();
                order.id = id;
                order.DNI = DNI;
                order.fecha = fecha;
                orders.Add(order);
            }   
            reader.Close();
            return orders;
        }

        public List<ProductOrder> GetProductOrders()
        {
            List<ProductOrder> productOrders = new List<ProductOrder>();

            String query = "SELECT idProducto, idPedido, articulos FROM producto_pedido";
            
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int idProducto = int.Parse(reader.GetValue(0).ToString());
                int idPedido = int.Parse(reader.GetValue(1).ToString());
                int articulos = int.Parse(reader.GetValue(2).ToString());
              
                ProductOrder productOrder = new ProductOrder();
                productOrder.idProducto = idProducto;
                productOrder.idPedido = idPedido;
                productOrder.articulos = articulos;
                productOrders.Add(productOrder);
            }   
            reader.Close();
            return productOrders;
        }

        public List<ProductDistributor> GetProductDistributors()
        {
         
            List<ProductDistributor> productDistributors = new List<ProductDistributor>();

            String query = "SELECT CIF, idProducto, precio FROM Pedido";
            
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string CIF = (reader.GetValue(0).ToString());
                int idProducto = int.Parse(reader.GetValue(1).ToString());
                float precio = float.Parse(reader.GetValue(2).ToString());
                
             ProductDistributor productDistributor = new ProductDistributor();
                productDistributor.CIF = CIF;
                productDistributor.idProducto = idProducto;
                productDistributor.precio = precio;
                productDistributors.Add(productDistributor);
            }   
            reader.Close();
            return productDistributors;
        }


        public void DeleteProducts(List<Product> products)
        {
            foreach(Product product in products)
            {
                string query = "DELETE FROM producto WHERE Id =" + product.Id + ";";
                MySqlCommand cmd = new MySqlCommand(query, m_connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void addNewProduct (string descripcion, int stock, float precio)
        {
            string query = "INSERT INTO producto (descripcion, stock, precio) VALUES('"+ descripcion + "'," + stock + "," + precio + ");";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
        }

        public List<Product> getSoldOut()
        {
            List<Product> products = new List<Product>();

            for(int i = 0; i<GetProducts().Count; i++)
            {
                if (GetProducts().ElementAt<Product>(i).stock == 0)
                {
                    products.Add(GetProducts().ElementAt<Product>(i));
                }
               
            }

            return products;

        }
       /* public void addOrder(String DNI, int idProducto, int articulos)
        {
            Order order = new Order();
            order.DNI = DNI;
            ProductOrder productOrder = new ProductOrder();
            productOrder.idPedido = order.id;
            productOrder.idProducto = idProducto;
            productOrder.articulos = articulos;
            String query = "INSERT INTO Pedido(DNI, fecha) VALUES('"+ DNI + "'," + 2019/11/11 11:57:00 + ");";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
            query = "INSERT INTO Producto_Pedido(idProducto, idPedido, articulos) VALUES(" + idProducto + "," + order.id + ", " + articulos + ");";
            cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
        }*/
    }
}
