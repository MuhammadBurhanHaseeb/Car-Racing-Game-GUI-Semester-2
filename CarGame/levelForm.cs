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
    public partial class levelForm : Form
    {
        public levelForm()
        {
            InitializeComponent();
        }

        private void btnLevel1_Click(object sender, EventArgs e)
        {
            Game frm = new Game();
            frm.ShowDialog();
        }

        private void btnLevel2_Click(object sender, EventArgs e)
        {
            gameLevel2 frm = new gameLevel2();
            frm.ShowDialog();
        }

        private void levelForm_Load(object sender, EventArgs e)
        {

        }
    }
}
