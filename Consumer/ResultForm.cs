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
    public partial class ResultForm : Form
    {
        private Image img;
        public ResultForm(Image img)
        {
            InitializeComponent();
            this.img = img;
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = img;
                         
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
    }
}
