using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CLSuperPixel
{
    public partial class frmTestParameters : Form
    {
        public frmTestParameters()
        {
            InitializeComponent();
        }

        Bitmap bmpOrig;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images|*.jpg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bmpOrig = new Bitmap(ofd.FileName);
                picImgOrig.Image = bmpOrig;

            }
        }
    }
}
