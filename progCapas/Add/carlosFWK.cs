using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace progCapas.Add
{
    public class carlosFWK
    {
        public void cerrar()
        {
            Application.Exit();
        }
        public void minimizar(Form Form1)
        {
            Form1.WindowState = FormWindowState.Minimized;
        }

        public void limpiarDG(DataGridView dt)
        {
            dt.Rows.Clear();
        }

        public void limpiarTextBox(TextBox textBox)
        {
            textBox.Clear();
        }

        public void openForm(Form form1, Form form2)
        {
            form2.Show();
            form1.Hide();
        }

        public void habilitarButton(Button btn)
        {
            btn.Enabled = true;
        }

        public void inhabilitarButton(Button btn)
        {
            btn.Enabled = false;
        }
    }
}
