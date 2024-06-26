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
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            LevelsForm frm = new LevelsForm();
            frm.ShowDialog();
        }

        private void btnExists_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
