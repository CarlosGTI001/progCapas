using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class curso
    {

        conect conection = new conect();
        SqlDataReader read;
        SqlDataReader reada;
        DataTable tab = new DataTable();
        SqlCommand com = new SqlCommand();
        DataTable tab2 = new DataTable();
        SqlCommand com2 = new SqlCommand();


        public void CreateCurso(string cursoId, string cursoNombre, string cursoDescripcion, int cursoCupoLimite)
        {
            com.Connection = conection.OpenCon();
            com.CommandText = "CreateCurso";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CursoId", cursoId);
            com.Parameters.AddWithValue("@CursoNombre", cursoNombre);
            com.Parameters.AddWithValue("@CursoDescripcion", cursoDescripcion);
            com.Parameters.AddWithValue("@CursoCupoLimite", cursoCupoLimite);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            conection.CerrarConexion();
        }

        public DataTable ReadCurso()
        {
            tab.Clear();
            com.Connection = conection.OpenCon();
            com.CommandText = "ReadCurso";
            com.CommandType = CommandType.StoredProcedure;
            read = com.ExecuteReader();
            tab.Load(read);
            read.Close();
            return tab;
        }

        public void UpdateCurso(string cursoId, string cursoNombre, string cursoDescripcion, int cursoCupoLimite)
        {
            com.Connection = conection.OpenCon();
            com.CommandText = "UpdateCurso";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CursoId", cursoId);
            com.Parameters.AddWithValue("@CursoNombre", cursoNombre);
            com.Parameters.AddWithValue("@CursoDescripcion", cursoDescripcion);
            com.Parameters.AddWithValue("@CursoCupoLimite", cursoCupoLimite);

            com.ExecuteNonQuery();
            com.Parameters.Clear();
            conection.CerrarConexion();
        }

        public void DeleteCurso(string cursoId)
        {
            com.Connection = conection.OpenCon();
            com.CommandText = "DeleteCurso";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@CursoId", cursoId);
            
            com.ExecuteNonQuery();
            com.Parameters.Clear();
            conection.CerrarConexion();
        }

        public List<string> ShowIDCurso()
        {
            com2.Connection = conection.OpenCon();
            com2.CommandText = "ReadCurso";
            com2.CommandType = CommandType.StoredProcedure;
            
            reada = com2.ExecuteReader();

            List<string> resultado = new List<string>();
            while (reada.Read())
            {
                resultado.Add(reada["CursoId"].ToString());
            }
            com2.Parameters.Clear();
            reada.Close();
            return resultado;
        }

        public string ShowNombreCurso(string id)
        {
            string resultado = "";
            com2.Connection = conection.OpenCon();
            com2.CommandText = "ReadCursoWhereId";
            com2.Parameters.AddWithValue("@cursoid", id);
            com2.CommandType = CommandType.StoredProcedure;
            reada = com2.ExecuteReader();

            
            while (reada.Read())
            {
                resultado = reada["CursoNombre"].ToString();
            }
            com2.Parameters.Clear();
            reada.Close();
            return resultado;
        }
    }
}
