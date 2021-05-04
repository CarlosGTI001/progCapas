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
    public class usrMgrBsn
    {
        admUsr adm = new admUsr();

        public bool login(string email, string password)
        {
            bool _u, _p, _r;
            string[] pwd = adm.ShowPsw();
            string[] eml = adm.ShowUsr();
            int longitudU = eml.Length, longitudC = pwd.Length;
            
            _u = false;
            _p = false;
        
            if (longitudU > 0)
            {  
                for (int i = 0; i < longitudU; i++)
                {
                    if (eml[i] == email)
                    {
                        _u = true;
                        goto exit1;
                    }
                    else
                    {
                        _u = false;
                    }
                }

            exit1:
                for (int i = 0; i < longitudC; i++)
                {
                    if (pwd[i] == password)
                    {
                        _p = true;
                        goto exit2;
                    }
                    else
                    {
                        _p = false;
                    }
                }
            exit2:; 
            }
            else
            {
                _p = false;
            }



            if (_u == true && _p == true)
            {
                _r = true;
            }
            else
            {
                _r = false;
            }

            return _r;
        }


        public bool verificarRoll(string usuario)
        {
            string result = adm.roll(usuario);
            if(result == "administrador")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable leerUser()
        {
            return adm.leerUsuarios();
        }

        public void insertarUser(string nombre, string apellido, string numeroTelefono, string CorreoElectronico, string usuario, string contrasena, string privilegio, string estatus)
        {
            adm.insertarestudiante(nombre, apellido, numeroTelefono, CorreoElectronico, usuario, contrasena, privilegio, estatus);
        }

        public void actualizarUser(string nombre, string apellido, string numeroTelefono, string CorreoElectronico, string usuario, string contrasena, string privilegio, string estatus)
        {
            adm.actualizarUsuario(nombre, apellido, numeroTelefono, CorreoElectronico, usuario, contrasena, privilegio, estatus);
        }

        public bool confirmarRepetido(string usuario)
        {
            bool _u, _p, _r;
            string[] usr = adm.ShowUsrIF();
            int longitudU = usr.Length;

            _u = false;
            _p = false;

            if (longitudU > 0)
            {
                for (int i = 0; i < longitudU; i++)
                {
                    if (usr[i] == usuario)
                    {
                        _u = true;
                        goto exit1;
                    }
                    else
                    {
                        _u = false;
                    }
                }

            }
            else
            {
                _u = false;
            }
            exit1:;
            return _u;
        }

        public DataTable filtrarUsuarios(string estatus)
        {
            return adm.filtrarUsuarioEstatus(estatus);
        }

        public void eliminarUsuer(string id)
        {
            adm.eliminarUser(id);
        }

        public void actualizarEstatus(string usuario)
        {
            adm.actualizarEstatus(usuario);
        }
    }

}
