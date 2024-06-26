using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace FrameWork.Core
{
   public  class progressBar
    {
       private ProgressBar health;
        public progressBar(int width , int height , int x , int y )
        {
            health = new ProgressBar();
            System.Drawing.Point location = new System.Drawing.Point();
            health.Size = new Size(width,height);
            health.Value = 100;
            location.X = x;
            location.Y = y;
            health.Location = location;
        }
    }
}
