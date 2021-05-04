using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;
using IniParser;
using IniParser.Model;

namespace progCapas
{
    public partial class Dashboard : Form
    {
        public bool test {get; set;}
        Login login = new Login();
        private Point pos = Point.Empty;
        private bool move = false;
        //variables para almacenar puntos cardinales c = centro a = arriba 
        Point centro = new Point(349, 206);
        Point arriba = new Point(506, 14);
        Point oculto = new Point(9, 499);
        Point visible = new Point(6, 40);
        //instanciar las clases
        bsn cdb = new bsn();
        IniData data;
        bsnCursos cursBsn = new bsnCursos();
        bsnSeccion seccBsn = new bsnSeccion();
        Add.carlosFWK winMgr = new Add.carlosFWK();
        //variables
        //estudiante
        string ida, id;
        bool actualizar = false, busqueda = false, visto = false, actualizarE = false;
        //seccion
        //curso
        public Dashboard()
        {
            InitializeComponent();
            
            var parser = new FileIniDataParser();
            data = parser.ReadFile("tutorial.ini");
            button1.BackColor = Color.Navy;
            button1.ForeColor = Color.White;
            button1.BackgroundImage = Properties.Resources.gg__1_;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Text = "";
            button1.FlatAppearance.BorderSize = 0;
            button2.BackColor = Color.Navy;
            button2.ForeColor = Color.White;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Text = "";
            button2.BackgroundImage = Properties.Resources.New_Project__4_;
            button4.BackColor = Color.Navy;
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button4.ForeColor = Color.White;
            button4.FlatStyle = FlatStyle.Flat;
            button4.BackgroundImage = Properties.Resources.gg__1_;
            button4.Text = "";
            button4.FlatAppearance.BorderSize = 0;
            button5.BackColor = Color.Navy;
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.Text = "";
            button5.FlatStyle = FlatStyle.Flat;
            button5.ForeColor = Color.White;
            button5.FlatAppearance.BorderSize = 0;

            button5.BackgroundImage = Properties.Resources.New_Project__5_;
            this.Icon = login.Icon;
            
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Sign.Location = centro;
            this.BackgroundImage = Properties.Resources.sinLog;
            Font myFont = new Font(Sign.Font.FontFamily, 20);
            Sign.Font = myFont;
            string tutorial = data["tutorial"]["visto"];
            visto = bool.Parse(tutorial);
            button5.Enabled = false;
            if (test == true)
            {
                BtnUsrMgr.Visible = true;
            }
            else
            {
                BtnUsrMgr.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sign.Location = arriba;
            this.BackgroundImage = Properties.Resources.logExtended;
            Font myFont = new Font(Sign.Font.FontFamily, 12);
            Sign.Font = myFont;
            estudiante.Show();
            cursoP.Hide();
            seccionP.Hide();
            Sign.Text = "Registro Estudiantes";
            bool done = false;
            estudiante.Location = visible;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new carga())
                {
                    splashForm.Show();
                    while (!done)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
            Thread.Sleep(5000);
            done = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sign.Location = arriba;
            this.BackgroundImage = Properties.Resources.logExtended;
            Font myFont = new Font(Sign.Font.FontFamily, 12);
            Sign.Font = myFont;
            cursoP.Show();
            estudiante.Hide();
            seccionP.Hide();
            Sign.Text = "Registro Cursos";
            bool done = false;
            cursoP.Location = visible;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new carga())
                {
                    splashForm.Show();
                    while (!done)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
            Thread.Sleep(5000);
            done = true;
        }




        //estudiante Container

        private void estudiante_VisibleChanged(object sender, EventArgs e)
        {
            cbbxCurso.DataSource = cursBsn.MostrarIDCurso();
            readDgEstudiante.ClearSelection();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMatricula.Text) || !string.IsNullOrEmpty(txtName.Text) || !string.IsNullOrEmpty(txtApellido.Text) || !string.IsNullOrEmpty(txtEdad.Text))
                {
                    string seccion = cbbxSeccion.Text;
                    if (cdb.insertarPersona(txtMatricula.Text, txtName.Text, txtApellido.Text, int.Parse(txtEdad.Text), txtTelefono.Text, dtPFecha.Value, cbbxCurso.Text, cursBsn.leerCursoNombre(cbbxCurso.Text), seccion, seccBsn.obtenerSeccionNombreWhereId(seccion).ToString()))
                    {
                        readDgEstudiante.DataSource = cdb.mostrarPersonaWhereS2(seccion);
                        limpiarTb();
                        tuto.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un estudiante con esa matricula");
                    }


                }
                else
                {
                    MessageBox.Show("No deje ninguno de los campos obligatorios vacio", "Alerta");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar el registro por:\n " + ex.ToString() + "\n Contacte al administrador del sistema.", "Error");
            }
        }

        private void limpiarTb()
        {
            winMgr.limpiarTextBox(txtName);
            winMgr.limpiarTextBox(txtApellido);
            winMgr.limpiarTextBox(txtTelefono);
            winMgr.limpiarTextBox(txtEdad);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (actualizar == true)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtMatricula.Text) || !string.IsNullOrEmpty(txtName.Text) || !string.IsNullOrEmpty(txtApellido.Text) || !string.IsNullOrEmpty(txtEdad.Text))
                    {
                        cdb.editarPersona(ida, txtName.Text, txtApellido.Text, int.Parse(txtEdad.Text), txtTelefono.Text, dtPFecha.Value, cbbxCurso.Text, cursBsn.leerCursoNombre(cbbxCurso.Text), cbbxSeccion.Text, seccBsn.obtenerSeccionNombreWhereId(cbbxSeccion.Text).ToString());
                        cdb.mostrarPersonaWhereS(cbbxSeccion.Text);
                        limpiarTb();
                        winMgr.inhabilitarButton(btnActualizar);
                        winMgr.habilitarButton(btnInsertar);
                        winMgr.inhabilitarButton(btnEliminar);
                        txtMatricula.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No dejes ningun campo vacio", "Alerta");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo actualizar el registro por:\n" + ex.Message.ToString() + "\n Contacte al administrador del sistema.", "Error");
                }

            }
            else
            {
                MessageBox.Show("Selecciona una celda para modificar su registro", "Alerta");
            }
        }

        private void readDgEstudiante_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            winMgr.habilitarButton(btnActualizar);
            winMgr.habilitarButton(btnEliminar);
            ida = readDgEstudiante.CurrentRow.Cells["matricula"].Value.ToString();
            txtMatricula.Text = ida;
            txtName.Text = readDgEstudiante.CurrentRow.Cells["nombre"].Value.ToString();
            txtApellido.Text = readDgEstudiante.CurrentRow.Cells["apellido"].Value.ToString();
            txtEdad.Text = readDgEstudiante.CurrentRow.Cells["edad"].Value.ToString();
            txtTelefono.Text = readDgEstudiante.CurrentRow.Cells["numeroTelefono"].Value.ToString();
            dtPFecha.Value = DateTime.Parse(readDgEstudiante.CurrentRow.Cells["fechadenacimiento"].Value.ToString());
            cbbxSeccion.SelectedItem = readDgEstudiante.CurrentRow.Cells["seccionid"].Value.ToString();
            cbbxCurso.SelectedItem = readDgEstudiante.CurrentRow.Cells["cursoId"].Value.ToString();
            actualizar = true;
            txtMatricula.Enabled = false;
            winMgr.inhabilitarButton(btnInsertar);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(readDgEstudiante.SelectedCells.ToString()))
            {
                try
                {
                    if (readDgEstudiante.SelectedCells.Count > 0)
                    {
                        DialogResult resultado = MessageBox.Show("En realidad desea eliminar a la persona", "Alerta", MessageBoxButtons.YesNoCancel);
                        if (resultado == DialogResult.Yes)
                        {
                            id = readDgEstudiante.CurrentRow.Cells["matricula"].Value.ToString();
                            cdb.eliminarPersona(id);
                            cdb.mostrarPersonaWhereS(cbbxSeccion.Text);
                            limpiarTb();
                            winMgr.inhabilitarButton(btnActualizar);
                            winMgr.habilitarButton(btnInsertar);
                            winMgr.inhabilitarButton(btnEliminar);

                            txtMatricula.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("No has seleccionado una fila", "Alerta");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha podido elimiar el registro por:\n" + ex.Message.ToString() + "\n Contacte al administrador del sistema.", "Error");
                }
            }
            else
            {
                MessageBox.Show("No has seleccionado una fila", "Alerta");
            }
        }

        private void btnogout_Click(object sender, EventArgs e)
        {
            
            winMgr.openForm(this, login);
        }

        private void closeLbl_Click(object sender, EventArgs e)
        {
            
            winMgr.openForm(this, login);
        }

        private void miniLbl_Click(object sender, EventArgs e)
        {
            winMgr.minimizar(this);
        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            int contar = 0;
            if (busqueda)
            {
                tuto.Hide();
                readDgEstudiante.DataSource = cdb.mostrarPersonaWhereM(txtMatricula.Text);
                foreach (DataGridViewRow item in readDgEstudiante.Rows)
                {
                    contar = contar + 1;
                }
                if (contar < 1)
                {
                    if (tuto.Visible == false)
                    {
                        tuto.Show();
                    }
                    tuto.Text = "No hay estudiantes con esa matricula";
                }
                else
                {
                    tuto.Hide();
                }

            }
        }

        private void estudiante_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbbxSeccion_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbbxCurso.Text == "Seleccione Curso")
            {
                tuto.Text = "Selecciona un curso\n para desplegar los estudiantes";
            }
            else
            {
                if (cbbxSeccion.Text == "Seleccione Seccion")
                {
                    tuto.Show();
                    readDgEstudiante.DataSource = "";
                    tuto.Text = "Este curso no tiene secciones o no has seleccionado una, \ndebes crear una";
                }
                else
                {
                    verificarCantidad();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button5.Enabled = false;
            busqueda = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            estudiante.Location = oculto;
            this.BackgroundImage = Properties.Resources.sinLog;
            Font myFont = new Font(Sign.Font.FontFamily, 20);
            Sign.Font = myFont;
            Sign.Text = "Seleccione una opcion del menu";
            Sign.Location = centro;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtMatricula.Enabled = true;
            busqueda = true;
            button2.Enabled = false;
            button5.Enabled = true;
            txtMatricula.Text = "SD-";
        }

        private void cbbxCurso_SelectedValueChanged(object sender, EventArgs e)
        {
            readDgEstudiante.DataSource = "";
            if (cbbxCurso.Text == "Seleccione Curso")
            {

                tuto.Text = "Selecciona un curso\n para desplegar los estudiantes";
                tuto.Show();
                cbbxSeccion.Text = "";
            }
            else
            {
                cbbxSeccion.Text = "";
                cbbxSeccion.DataSource = seccBsn.obtenerSeccionID(cbbxCurso.Text);
                cbbxSeccion.Text = "Seleccione Seccion";
                verificarCantidad();
            }
            if (string.IsNullOrEmpty(cbbxSeccion.Text) && cbbxCurso.Text != "Seleccione Curso")
            {
                tuto.Text = "Este curso no tiene seccion, \ndebes crear una";
            }
        }

        public void verificarCantidad()
        {
            int contar = 0;
            readDgEstudiante.DataSource = cdb.mostrarPersonaWhereS(cbbxSeccion.Text);
            foreach (DataGridViewRow item in readDgEstudiante.Rows)
            {
                contar = contar + 1;
            }
            if (contar < 1)
            {
                if (tuto.Visible == false)
                {
                    tuto.Show();
                }
                tuto.Text = "Este curso no tiene secciones \no no has seleccionado una, \ndebes crear una";
            }
            else
            {
                tuto.Hide();
            }
        }



        //curso

        private void insertarSeccion_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescripcion.Text) && !string.IsNullOrEmpty(txtIDCurso.Text) && !string.IsNullOrEmpty(txtNombreCurso.Text) && numCupo.Value != 0)
            {
                try
                {
                    cursBsn.insertarCurso(txtIDCurso.Text, txtNombreCurso.Text, txtDescripcion.Text, int.Parse(numCupo.Value.ToString()));
                    actualizarCurso();
                    limpiarTxt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El registro no se pudo ingresar\n" + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("No deje los campos vacios.", "Alerta");
            }
        }

        private void eliminarSeccion_Click(object sender, EventArgs e)
        {
            if (actualizarE == true)
            {
                try
                {
                    cursBsn.eliminarCurso(txtIDCurso.Text);
                    actualizarCurso();
                    actualizarE = false;
                    EnabledBTN();
                    limpiarTxt();
                    txtIDCurso.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha eliminado correctamente el registro, contacte a el Desarrollador del sistema\n" + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("No has seleccionado una celda para la operacion\nSeleccione una celda para continuar", "Alerta");
            }
        }

        private void readDg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIDCurso.Enabled = false;
            winMgr.habilitarButton(actualizarCursobtn);
            winMgr.habilitarButton(eliminarCursoBtn);
            winMgr.inhabilitarButton(insertarCursoBtn);

            txtIDCurso.Text = readDg.CurrentRow.Cells["CursoID"].Value.ToString();
            txtNombreCurso.Text = readDg.CurrentRow.Cells["CursoNombre"].Value.ToString();
            txtDescripcion.Text = readDg.CurrentRow.Cells["CursoDescripcion"].Value.ToString();
            actualizarE = true;
        }

        private void cursoP_VisibleChanged(object sender, EventArgs e)
        {
            actualizarCurso();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cursoP.Hide();
            cursoP.Location = oculto;
            Sign.Location = centro;
            Font myFont = new Font(Sign.Font.FontFamily, 20);
            Sign.Font = myFont;
            this.BackgroundImage = Properties.Resources.sinLog;
            Sign.Text = "Seleccione una opcion del menu";
        }

        private void actualizarSeccion_Click(object sender, EventArgs e)
        {
            if (actualizarE == true)
            {
                try
                {
                    cursBsn.actualizarCurso(txtIDCurso.Text, txtNombreCurso.Text, txtDescripcion.Text, int.Parse(numCupo.Value.ToString()));
                    actualizarCurso();
                    EnabledBTN();
                    limpiarTxt();
                    txtIDCurso.Enabled = true;
                    actualizarE = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha actualizado correctamente el registro, contacte a el Desarrollador del sistema\n" + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("No has seleccionado una celda para la actualizacion\nSeleccione una celda para continuar", "Alerta");
            }
        }

        private void actualizarCurso()
        {
            readDg.DataSource = cursBsn.leerCurso();
        }

        private void limpiarTxt()
        {
            txtDescripcion.Text = "";
            txtIDCurso.Text = "";
            txtNombreCurso.Text = "";
            numCupo.Value = 0;
        }

        private void EnabledBTN()
        {
            winMgr.habilitarButton(insertarCursoBtn);
            winMgr.inhabilitarButton(eliminarCursoBtn);
            winMgr.inhabilitarButton(actualizarCursobtn);
        }

        //seccion

        private void insertarSeccion_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescripcion.Text) && !string.IsNullOrEmpty(txtMaestro.Text) && cbxCurso.Text != "" && cbxSeccion.Text != "" && numEdadMax.Value != 0)
            {
                try
                {
                    seccBsn.insertarSeccion(cbxCurso.Text + cbxSeccion.Text, txtDescripcion.Text + " " + cbxSeccion.Text, txtMaestro.Text, 0, int.Parse(numEdadMax.Value.ToString()), dtPFecha.Value);
                    verDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha podido agregar el registro por: " + ex.Message + ".\nLLame al administrador del sistema.", "Error");
                }
            }
            else
            {
                MessageBox.Show("No deje ningun campo obligatorio vacio", "Alerta - Campos Vacios");
            }
        }

        private void verDatos()
        {
            regDataSeccion.DataSource = seccBsn.leerSeccion();
        }

        private void actualizarSeccion_Click_1(object sender, EventArgs e)
        {
            if (actualizar)
            {
                try
                {
                    seccBsn.actualizarSeccion(cbxCurso.Text + cbxSeccion.Text, txtDescripcion.Text + " " + cbxSeccion.Text, txtMaestro.Text, int.Parse(numCantidad.Value.ToString()), int.Parse(numEdadMax.Value.ToString()), dtPFecha.Value);
                    activar();
                    verDatos();
                    actualizar = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha podido actualizar el registro por:" + ex.Message + ".\nContacte a el desarrollador del sistema", "Error");
                }
            }
            else
            {
                MessageBox.Show("No has seleccionado una celda para actualizar un registro.");
            }
        }

        private void seccionP_VisibleChanged(object sender, EventArgs e)
        {
            cbxCurso.DataSource = cursBsn.MostrarIDCurso();
            cbxCurso2.DataSource = cursBsn.MostrarIDCurso();
            verDatos();
        }

        private void seccionBtn_Click(object sender, EventArgs e)
        {
            Sign.Location = arriba;
            this.BackgroundImage = Properties.Resources.logExtended;
            Font myFont = new Font(Sign.Font.FontFamily, 12);
            Sign.Font = myFont;
            seccionP.Show();
            estudiante.Hide();
            cursoP.Hide();
            Sign.Text = "Registro Seccion";
            bool done = false;
            seccionP.Location = visible;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new carga())
                {
                    splashForm.Show();
                    while (!done)
                        Application.DoEvents();
                    splashForm.Close();
                }
            });
            Thread.Sleep(5000);
            done = true;
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            seccionP.Hide();
            seccionP.Location = oculto;
            Sign.Location = centro;
            Font myFont = new Font(Sign.Font.FontFamily, 20);
            Sign.Font = myFont;
            this.BackgroundImage = Properties.Resources.sinLog;
            Sign.Text = "Seleccione una opcion del menu";
        }

        private void Dashboard_MouseDown(object sender, MouseEventArgs e)
        {
            pos = new Point(e.X, e.Y);
            move = true;
        }

        private void Dashboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
                this.Location = new Point((this.Left + e.X - pos.X),
                    (this.Top + e.Y - pos.Y));
        }

        private void Dashboard_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           
        }

        private void seccionP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void activar()
        {
            cbxSeccion.Enabled = true;
            cbxSeccion.Enabled = true;
            winMgr.habilitarButton(insertarSeccion);
            winMgr.inhabilitarButton(actualizarSeccion);
            winMgr.inhabilitarButton(eliminarSeccion);
        }


        private void eliminarSeccion_Click_1(object sender, EventArgs e)
        {
            if (actualizar)
            {
                try
                {
                    seccBsn.eliminarSeccion(cbxCurso.Text + cbxSeccion.Text);
                    verDatos();
                    actualizar = false;
                    activar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El registro no se ha podido eliminar por:" + ex.Message + ".\nContacte al desarrollador del sistema", "Error");
                }

            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            readDg.DataSource = seccBsn.filtrarSeccion(cbxCurso2.Text);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            verDatos();
        }


    }
}
