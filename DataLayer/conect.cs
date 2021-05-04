using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class conect
    {
        private SqlConnection Con = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;DataBase=prueba1;Integrated Security=true");

        public SqlConnection OpenCon()
        {
            if (Con.State == ConnectionState.Closed)
                Con.Open();
            return Con;
        }

        public SqlConnection CerrarConexion()
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
            return Con;
        }
    }
}
