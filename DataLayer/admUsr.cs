using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class admUsr
    {
        SqlDataReader read;
        SqlDataReader reada;
        SqlCommand com = new SqlCommand();
        SqlCommand com2 = new SqlCommand();
        SqlDataReader Query;
        DataTable datos = new DataTable();

        private conect con = new conect();
        public string[] ShowUsr()
        {
            com2.Connection = con.OpenCon();
            com2.CommandText = "VerCorreo";
            com2.CommandType = CommandType.StoredProcedure;
            reada = com2.ExecuteReader();

            List<string> resultadoUsr = new List<string>();

            while (reada.Read())
            {
                resultadoUsr.Add((string)reada["correoElectronico"]);
            }
            string[] arrays = resultadoUsr.ToArray();

            con.CerrarConexion();
            return arrays;
        }

        public void eliminarUser(string id)
        {
            com2.Connection = con.OpenCon();
            com2.CommandText = "eliminarUsuario";
            com2.CommandType = CommandType.StoredProcedure;
            com2.Parameters.AddWithValue("@usuario", id);
            com2.ExecuteNonQuery();
            com2.Parameters.Clear();
            con.CerrarConexion();
        }

        public void actualizarEstatus (string user)
        {
            com2.Connection = con.OpenCon();
            com2.CommandText = "actualizarEstatus";
            com2.CommandType = CommandType.StoredProcedure;
            com2.Parameters.AddWithValue("@usuario", user);
            com2.ExecuteNonQuery();
            com2.Parameters.Clear();
            con.CerrarConexion();
        }

        public DataTable filtrarUsuarioEstatus(string estatus)
        {
            reada.Close();
            datos.Clear();
            com2.Connection = con.OpenCon();
            com2.CommandText = "filtrarUsuarioEstatus";
            com2.Parameters.AddWithValue("@estatus", estatus);
            com2.CommandType = CommandType.StoredProcedure;
            reada = com2.ExecuteReader();
            datos.Load(reada);
            com2.Parameters.Clear();
            con.CerrarConexion();
            return datos;
        }

        public string[] ShowUsrIF()
        {
            com2.Connection = con.OpenCon();
            com2.CommandText = "leerUsuarios";
            com2.Parameters.Clear();
            com2.CommandType = CommandType.StoredProcedure;
            reada = com2.ExecuteReader();

            List<string> resultadoUsr = new List<string>();

            while (reada.Read())
            {
                resultadoUsr.Add((string)reada["usuario"]);
            }
            string[] arrays = resultadoUsr.ToArray();

            com2.Parameters.Clear();
            con.CerrarConexion();
            return arrays;
        }

        /*public string ShowRoll(string id)
        {
            com2.Connection = con.OpenCon();
            com2.CommandText = "MostrarRoll";
            com2.Parameters.AddWithValue("@id", id);
            com2.CommandType = CommandType.StoredProcedure;
            reada = com2.ExecuteReader();

            List<string> resultadoUsr = new List<string>();

           
            string String = resultadoUsr.ToString();

            con.CerrarConexion();
            return String;
        }*/

        public string[] ShowPsw()
        {
            com2.Connection = con.OpenCon();
            com2.CommandText = "VerContrasenia";
            com2.CommandType = CommandType.StoredProcedure;
            read = com2.ExecuteReader();

            List<string> resultadoUsr = new List<string>();

            while (read.Read())
            {
                resultadoUsr.Add((string)read["contrasenia"]);
            }
            string[] arrays = resultadoUsr.ToArray();

            con.CerrarConexion();
            read.Close();
            return arrays;
        }


        public string roll(string usuario)
        {

            com2.Connection = con.OpenCon();
            com2.CommandText = "leerUsuarioPorId";
            com2.CommandType = CommandType.StoredProcedure;
            com2.Parameters.AddWithValue("@correoElectronico", usuario);
            read = com2.ExecuteReader();
            string roll = "";
            while (read.Read())
            {
                roll = read["roll"].ToString();
            }
            con.CerrarConexion();
            read.Close();
            return roll;
        }

        public void insertarestudiante(string nombre, string apellido, string numeroTelefono, string CorreoElectronico, string usuario, string contrasena, string privilegio, string estatus)
        {
            com2.Connection = con.OpenCon();
            com2.CommandText = "insertarUsuario";
            com2.CommandType = CommandType.StoredProcedure;
            com2.Parameters.AddWithValue("@nombre", nombre);
            com2.Parameters.AddWithValue("@apellido", apellido);
            com2.Parameters.AddWithValue("@numeroTelefono", numeroTelefono);
            com2.Parameters.AddWithValue("@usuario", usuario);
            com2.Parameters.AddWithValue("@contrasenia", contrasena);
            com2.Parameters.AddWithValue("@correoElectronico", CorreoElectronico);
            com2.Parameters.AddWithValue("@roll", privilegio);
            com2.Parameters.AddWithValue("@estatus", estatus);
            
            com2.ExecuteNonQuery();
            com2.Parameters.Clear();
            con.CerrarConexion();
        }

        public DataTable leerUsuarios()
        {
            datos.Clear();
            com2.Connection = con.OpenCon();
            com2.CommandText = "leerUsuarios";
            com2.CommandType = CommandType.StoredProcedure;
            Query = com2.ExecuteReader();
            datos.Load(Query);
            return datos;
        }

        public void actualizarUsuario(string nombre, string apellido, string numeroTelefono, string CorreoElectronico, string usuario, string contrasena, string privilegio, string estatus)
        {
            com2.Connection = con.OpenCon();
            com2.CommandText = "actualizarUsuario";
            com2.CommandType = CommandType.StoredProcedure;
            com2.Parameters.AddWithValue("@nombre", nombre);
            com2.Parameters.AddWithValue("@apellido", apellido);
            com2.Parameters.AddWithValue("@numeroTelefono", numeroTelefono);
            com2.Parameters.AddWithValue("@usuario", usuario);
            com2.Parameters.AddWithValue("@contrasenia", contrasena);
            com2.Parameters.AddWithValue("@correoElectronico", CorreoElectronico);
            com2.Parameters.AddWithValue("@roll", privilegio);
            com2.Parameters.AddWithValue("@estatus", estatus);
            com2.ExecuteNonQuery();
            com2.Parameters.Clear();
            com2.Dispose();
            con.CerrarConexion();
        }
        /*public string ShowId(string correo)
        {
            com2.Connection = con.OpenCon();
            com2.CommandText = "MostrarIdUs";
            com2.Parameters.AddWithValue("@user", correo);
            com2.CommandType = CommandType.StoredProcedure;
            reada = com2.ExecuteReader();

            List<string> resultadoUsr = new List<string>();

            string String = resultadoUsr.ToString();

            con.CerrarConexion();
            return String;
        }*/
    }
}