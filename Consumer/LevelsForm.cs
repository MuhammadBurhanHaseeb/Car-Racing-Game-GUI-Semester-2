using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Consumer
{
    public partial class LevelsForm : Form
    {
        public LevelsForm()
        {
            InitializeComponent();
        }

        private void btnLevel1_Click(object sender, EventArgs e)
        {
            GameLevel1 frm = new GameLevel1();
            frm.ShowDialog();
        }

        private void btnLevel2_Click(object sender, EventArgs e)
        {
            GameLevel2 frm = new GameLevel2();
            frm.ShowDialog();
        }
    }
}
