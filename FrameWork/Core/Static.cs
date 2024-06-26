using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace FrameWork.Core
{
   public  class Static : IMovemnt
    {
    
        public Point move(Point location)
        {
            return location; 
        }
    }
}
