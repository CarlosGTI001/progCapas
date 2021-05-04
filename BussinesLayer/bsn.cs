using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataLayer;

namespace BussinesLayer
{
    public class bsn
    {
        lista Lst = new lista();
        Random number = new Random();
        public DataTable mostrarPersona()
        {
            DataTable tabla = new DataTable();
            tabla = Lst.ShowList();
            return tabla;
        }

        public DataTable mostrarPersonaWhereM(string matricula)
        {
            DataTable tabla = new DataTable();
            tabla = Lst.ShowListWhere(matricula);
            return tabla;
        }
        public DataTable mostrarPersonaWhereS(string Seccion)
        {
            DataTable tabla = new DataTable();
            tabla = Lst.ShowListWhereSeccion(Seccion);
            return tabla;
        }
        public DataTable mostrarPersonaWhereS2(string Seccion)
        {
            DataTable tabla = new DataTable();
            tabla = Lst.ShowListWhereSeccion2(Seccion);
            return tabla;
        }

        public bool insertarPersona(string matricula, string nombre, string apellido, int edad, string numeroTelefono, DateTime fechaNacimiento, string cursoid, string cursonombre, string seccionid, string seccionnombre)
        {
            bool _2;
            _2 = false;
            string[] datos = Lst.ShowMatricula();
            int longitud = datos.Length;
            if (longitud > 0)
            {
                if (_2 == false)
                {
                    for (int i = 0; i < longitud; i++)
                    {
                        if (datos[i] != matricula)
                        {
                            _2 = true;
                        }
                        else
                        {
                            _2 = false;
                        }
                    }
                }
            }
            else
            {
                _2 = true;
            }



            if (_2 == true)
            {
                Lst.Insert(matricula, nombre, apellido, edad, numeroTelefono, fechaNacimiento, cursoid, cursonombre, seccionid, seccionnombre);
                return _2;
            }
            else
            {
                return _2;
            }


        }

        public void editarPersona(string matricula, string nombre, string apellido, int edad, string numeroTelefono, DateTime fechaNacimiento, string cursoid, string cursonombre, string seccionid, string seccionnombre)
        {
            Lst.Edit(matricula, nombre, apellido, edad, numeroTelefono, fechaNacimiento, cursoid, cursonombre, seccionid, seccionnombre);
        }

        public void eliminarPersona(string id)
        {
            Lst.Del(id);
        }
    }
}
