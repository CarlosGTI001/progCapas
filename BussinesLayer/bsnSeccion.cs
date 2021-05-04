using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataLayer;

namespace BussinesLayer
{
    public class bsnSeccion
    {
        seccion weccion = new seccion();

        public DataTable leerSeccion()
        {
            return weccion.ReadSeccion();
        }

        public bool insertarSeccion(string SeccionID, string SeccionNombre, string SeccionMaestro, int SeccionCantidadEs, int EdadMaxima, DateTime SeccionAnioCurso)
        {
            bool _2;
            _2 = false;
            string[] datos = weccion.ShowIDSeccion();
            int longitud = datos.Length;
            if (longitud > 0)
            {
                if (_2 == false)
                {
                    for (int i = 0; i < longitud; i++)
                    {
                        if (datos[i] != SeccionID)
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
                weccion.CreateSeccion(SeccionID, SeccionNombre, SeccionMaestro, SeccionCantidadEs, EdadMaxima, SeccionAnioCurso);
                return _2;
            }
            else
            {
                return _2;
            }
        }

        public void eliminarSeccion(string SeccionID)
        {
            weccion.DeleteSeccion(SeccionID);
        }

        public void actualizarSeccion(string SeccionID, string SeccionNombre, string SeccionMaestro, int SeccionCantidadEs, int EdadMaxima, DateTime SeccionAnioCurso)
        {
            weccion.UpdateSeccion(SeccionID, SeccionNombre, SeccionMaestro, SeccionCantidadEs, EdadMaxima, SeccionAnioCurso);
        }

        public List<string> obtenerSeccionID(string cursoId)
        {
            return weccion.ShowIDSeccionWhere(cursoId);
        }

        public string obtenerSeccionNombreWhereId(string cursoId)
        {
            return weccion.ShowIDSeccionWhereId(cursoId);
        }


        public string obtenerSeccionNombre(string id)
        {
            return weccion.ShowNombreSeccion(id);
        }

        public DataTable filtrarSeccion(string cursoId)
        {
            return weccion.filterSeccion(cursoId);
        }
    }
}
