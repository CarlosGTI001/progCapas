using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;

namespace progCapas
{
    public partial class Login : Form
    {
        private Point pos = Point.Empty;
        private bool move = false;
        public Login()
        {
            InitializeComponent();
        }
        Add.carlosFWK winMgr = new Add.carlosFWK();
        usrMgrBsn login = new usrMgrBsn();
        
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void minimizar_Click(object sender, EventArgs e)
        {
            winMgr.minimizar(this);
        }

        private void cerrar_Click(object sender, EventArgs e)
        {
            winMgr.cerrar();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(login.login(txtUsr.Text, txtPsw.Text))
            {
                Dashboard frm = new Dashboard();
                if(login.verificarRoll(txtUsr.Text))
                {
                    frm.test = true;
                }
                else
                {
                    frm.test = false;
                }
                frm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Datos ingresados de manera incorrecta o aun no estas registrado: \n\n Contacta al administrador del sistema.", "Alerta");
            }
        }

        

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            pos = new Point(e.X, e.Y);
            move = true;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
                this.Location = new Point((this.Left + e.X - pos.X),
                    (this.Top + e.Y - pos.Y));
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }
    }
}
