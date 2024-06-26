using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            levelForm frm = new levelForm();
            frm.ShowDialog();
         
        }

        private void btnExists_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLevels_Click(object sender, EventArgs e)
        {
            levelForm frm = new levelForm();
            frm.ShowDialog();

        }
    }
}
