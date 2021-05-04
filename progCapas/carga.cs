using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace progCapas
{
    public partial class carga : Form
    {
        public carga()
        {
            InitializeComponent();
        }

        private void carga_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("progress.gif");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }
    }
}
