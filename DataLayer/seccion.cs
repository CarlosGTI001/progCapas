using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataLayer;

namespace DataLayer
{
    public class seccion
    {
        private conect Conect = new conect();
        SqlDataReader read;
        SqlDataReader reada;
        DataTable tab = new DataTable();
        SqlCommand com = new SqlCommand();
        DataTable tab2 = new DataTable();
        SqlCommand com2 = new SqlCommand();
        SqlCommand com3 = new SqlCommand();
        public void CreateSeccion(string SeccionID, string SeccionNombre, string SeccionMaestro, int SeccionCantidadEs, int EdadMaxima, DateTime SeccionAnioCurso) 
        {
            com.Connection = Conect.OpenCon();
            com.CommandText = "CreateSeccion";
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@SeccionId", SeccionID);
            com.Parameters.AddWithValue("@SeccionNombre", SeccionNombre);
            com.Parameters.AddWithValue("@SeccionMaestro", SeccionMaestro);
            com.Parameters.AddWithValue("@SeccionCantidadEs", SeccionCantidadEs);
            com.Parameters.AddWithValue("@EdadMaxima", EdadMaxima);
            com.Parameters.AddWithValue("@SeccionAñoCurso", SeccionAnioCurso);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            Conect.CerrarConexion();
        }

        public DataTable ReadSeccion()
        {
            tab.Clear();
            com.Connection = Conect.OpenCon();
            com.CommandText = "verSeccionesDesc";
            com.CommandType = CommandType.StoredProcedure;
            read = com.ExecuteReader();
            tab.Load(read);
            read.Close();
            Conect.CerrarConexion();
            return tab;
        }

        public DataTable filterSeccion(string CursoID)
        {
            tab.Clear();
            com.Connection = Conect.OpenCon();
            com.CommandText = "filterSeccion";
            com.Parameters.AddWithValue("@CursoID", CursoID);
            com.CommandType = CommandType.StoredProcedure;
            read = com.ExecuteReader();
            tab.Load(read);
            read.Close();
            Conect.CerrarConexion();
            com.Parameters.Clear();
            return tab;
        }

        public void UpdateSeccion(string SeccionID, string SeccionNombre, string SeccionMaestro, int SeccionCantidadEs, int EdadMaxima, DateTime SeccionAnioCurso)
        {
            com.Connection = Conect.OpenCon();
            com.CommandText = "UpdateSeccion";
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@SeccionId", SeccionID);
            com.Parameters.AddWithValue("@SeccionNombre", SeccionNombre);
            com.Parameters.AddWithValue("@SeccionMaestro", SeccionMaestro);
            com.Parameters.AddWithValue("@SeccionCantidadEs", SeccionCantidadEs);
            com.Parameters.AddWithValue("@EdadMaxima", EdadMaxima);
            com.Parameters.AddWithValue("@SeccionAñoCurso", SeccionAnioCurso);
            
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            Conect.CerrarConexion();
        }

        public void DeleteSeccion(string SeccionID)
        {
            com.Connection = Conect.OpenCon();
            com.CommandText = "DeleteSeccion";
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@SeccionId", SeccionID);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            Conect.CerrarConexion();
        }

        public string[] ShowIDSeccion()
        {
            com2.Connection = Conect.OpenCon();
            com2.CommandText = "ReadSeccion";
            com2.CommandType = CommandType.StoredProcedure;
            reada = com.ExecuteReader();

            List<string> resultado = new List<string>();
            while (reada.Read())
            {
                resultado.Add(Convert.ToString(reada["SeccionId"]));
            }
            string[] arrays = resultado.ToArray();
            reada.Close();
            Conect.CerrarConexion();
            return arrays;
        }

        public List <string> ShowIDSeccionWhere(string curso)
        {
            com3.Connection = Conect.OpenCon();
            com3.CommandText = "LeerSeccionLike";
            com3.CommandType = CommandType.StoredProcedure;
            com3.Parameters.AddWithValue("@seccionid", curso);
            reada = com3.ExecuteReader();
            List<string> resultado = new List<string>();
            while (reada.Read())
            {
                resultado.Add(Convert.ToString(reada["SeccionId"]));
            }
            com3.Parameters.Clear();
            reada.Close();
            return resultado;
        }

        public string ShowIDSeccionWhereId(string seccion)
        {
            com3.Connection = Conect.OpenCon();
            com3.CommandText = "LeerSeccion";
            com3.CommandType = CommandType.StoredProcedure;
            com3.Parameters.AddWithValue("@seccionId", seccion);
            reada = com3.ExecuteReader();
            string resultado = "";
            while (reada.Read())
            {
                resultado = Convert.ToString(reada["SeccionNombre"]);
            }
            com3.Parameters.Clear();
            reada.Close();
            return resultado;
        }
        public string ShowNombreSeccion(string SeccionID)
        {
            com2.Connection = Conect.OpenCon();
            com2.CommandText = "ReadSeccionWhere";
            com2.CommandType = CommandType.StoredProcedure;
            com2.Parameters.AddWithValue("@seccionid", SeccionID);
            reada = com.ExecuteReader();

            List<string> resultado = new List<string>();
            while (reada.Read())
            {
                resultado.Add(Convert.ToString(reada["SeccionNombre"]));
            }
            string arrays = resultado.ToString();
            com2.Parameters.Clear();
            Conect.CerrarConexion();
            return arrays;
        }
    }
}
