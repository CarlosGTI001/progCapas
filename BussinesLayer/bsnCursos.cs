using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataLayer;

namespace BussinesLayer
{
    public class bsnCursos
    {
        curso course = new curso();
        public bool insertarCurso(string CursoID, string CursoNombre, string CursoDescripcion, int CursoLimite)
        {
            bool _2;
            _2 = false;
            string[] datos = course.ShowIDCurso().ToArray();
            int longitud = datos.Length;
            if (longitud > 0)
            {
                if (_2 == false)
                {
                    for (int i = 0; i < longitud; i++)
                    {
                        if (datos[i] != CursoID)
                        {
                            _2 = true;
                        }
                        else
                        {
                            _2 = false;
                            goto fa;
                        }
                    }
                }
            }
            else
            {
                _2 = true;
            }
        fa:
            if (_2)
            {
                course.CreateCurso(CursoID, CursoNombre, CursoDescripcion, CursoLimite);
                return _2;
            }
            else
            {
                return _2;
            }
        }

        public DataTable leerCurso()
        {
            return course.ReadCurso();
        }

        public void actualizarCurso(string CursoID, string CursoNombre, string CursoDescripcion, int CursoLimite)
        {
            course.UpdateCurso(CursoID, CursoNombre, CursoDescripcion, CursoLimite);
        }

        public void eliminarCurso(string CursoID)
        {
            course.DeleteCurso(CursoID);
        }

        public List<string> MostrarIDCurso()
        {
            return course.ShowIDCurso();
        }

        public string leerCursoNombre(string id)
        {
            return course.ShowNombreCurso(id);
        }
    }
}
