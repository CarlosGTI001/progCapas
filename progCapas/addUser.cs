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
    public partial class addUser : Form
    {
        Add.genpass gen = new Add.genpass();
        usrMgrBsn usrMgr = new usrMgrBsn();
        bool seleccionado;
        int id;
        string eatatus;
        public addUser()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void addUser_Load(object sender, EventArgs e)
        {
            actualizar();
            regDataUser.Columns["id"].Visible = false;
            regDataUser.Columns["contrasenia"].Visible = false;
        }

        private void btnGenerarPassw_Click(object sender, EventArgs e)
        {
            txtpasswd.Text = gen.GenerarPassword(8);
        }

        private void insertarSeccion_Click(object sender, EventArgs e)
        {

        }

        public void actualizar()
        {
            regDataUser.DataSource = usrMgr.leerUser();
        }

        private void insertarSeccion_Click_1(object sender, EventArgs e)
        {
            try{
                if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtNumero.Text) && !string.IsNullOrEmpty(txtCorreo.Text) && !string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtpasswd.Text) && !string.IsNullOrEmpty(cbxRoll.Text) && cbxRoll.Text != "Selecciona")
                {
                    if (!usrMgr.confirmarRepetido(txtUsuario.Text))
                    {
                        usrMgr.insertarUser(txtNombre.Text, txtApellido.Text, txtNumero.Text, txtCorreo.Text, txtUsuario.Text, txtpasswd.Text, cbxRoll.Text, "Activo");
                        actualizar(); 
                        foreach (Control c in this.Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Text = "";
                                txtNombre.Focus();
                            }
                        }
                        MessageBox.Show("Usuario Ingresado", "Suceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Disculpa, Ya existe ese usuario", "Alera");
                    }
                }
                else
                {
                    MessageBox.Show("No deje los campos vacio", "Alerta");
                }
            }catch(Exception es)
            {
                MessageBox.Show("No se puede ingresar el usario por: \n" + es.Message.ToLower() + "", "Error");
            }
            
        }

        private void actualizarSeccion_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtNumero.Text) && !string.IsNullOrEmpty(txtCorreo.Text) && !string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtpasswd.Text) && !string.IsNullOrEmpty(cbxRoll.Text) && cbxRoll.Text != "Selecciona")
                {
                    if (seleccionado)
                    {
                        usrMgr.actualizarUser(txtNombre.Text, txtApellido.Text, txtNumero.Text, txtCorreo.Text, txtUsuario.Text, txtpasswd.Text, cbxRoll.Text, "Activo");
                        actualizar();
                        seleccionado = false;
                        btnEliminar.Enabled = false;
                        btnHabilitar.Enabled = false;
                        btnActualizar.Enabled = false;
                        btnInsertar.Enabled = true;
                        MessageBox.Show("Usuario actualizado Correctamente", "Correcto");
                        foreach (Control c in this.Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Text = "";
                                txtNombre.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debes elegir un usuario para editar", "Alera");
                    }
                }
                else
                {
                    MessageBox.Show("No deje los campos vacio", "Alerta");
                }
            }
            catch (Exception es)
            {
                MessageBox.Show("No se puede actualizar el usario por: \n" + es.Message.ToLower() + "", "Error");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try 
            {
                if (seleccionado)
                {
                    if(MessageBox.Show("En realidad desea eliminar el usuario?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        usrMgr.eliminarUsuer(txtUsuario.Text);
                        actualizar();
                        seleccionado = false;
                        btnEliminar.Enabled = false;
                        btnHabilitar.Enabled = false;
                        btnActualizar.Enabled = false;
                        btnInsertar.Enabled = true;
                        foreach (Control c in this.Controls)
                        {
                            if (c is TextBox)
                            {
                                c.Text = "";
                                txtNombre.Focus();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debes elegir un usuario para elominar", "Alera");
                }
            }
            catch (Exception es)
            {
                MessageBox.Show("No se puede eliminar el usario por: \n" + es.Message.ToLower() + "", "Error");
            }
        }

        private void regDataUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(regDataUser.CurrentRow.Cells[0].Value.ToString());
            txtNombre.Text = regDataUser.CurrentRow.Cells[1].Value.ToString();
            txtApellido.Text = regDataUser.CurrentRow.Cells[2].Value.ToString();
            txtCorreo.Text = regDataUser.CurrentRow.Cells[3].Value.ToString();
            txtNumero.Text = regDataUser.CurrentRow.Cells[4].Value.ToString();
            txtUsuario.Text = regDataUser.CurrentRow.Cells[5].Value.ToString();
            txtpasswd.Text = regDataUser.CurrentRow.Cells[6].Value.ToString();
            cbxRoll.Text = regDataUser.CurrentRow.Cells[7].Value.ToString();
            eatatus = regDataUser.CurrentRow.Cells[8].Value.ToString();
            if(eatatus == "Activo")
            {
                btnHabilitar.Text = "Deshabilitar";
            }
            else
            {
                btnHabilitar.Text = "Habilitar";
            }
            seleccionado = true;
            btnEliminar.Enabled = true;
            btnHabilitar.Enabled = true;
            btnActualizar.Enabled = true;
            btnInsertar.Enabled = false;
        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if(btnHabilitar.Text == "Deshabilitar")
            {
                
            }
            else if(btnHabilitar.Text == "Habilitar")
            {

            }
        }
    }
}
