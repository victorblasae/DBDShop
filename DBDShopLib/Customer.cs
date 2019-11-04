using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DBDShopLib
{
    public class Customer
    {
        MySqlConnection m_connection = null;

        public String DNI, nombre, apellidos;

        public Customer()
        { }

        public Customer(String DNI, String nombre, String apellidos)
        {
            this.DNI = DNI;
            this.nombre = nombre;
            this.apellidos = apellidos;

            String query = "INSERT INTO Cliente values(DNI, nombre, apellidos)";
        }
    }
}
