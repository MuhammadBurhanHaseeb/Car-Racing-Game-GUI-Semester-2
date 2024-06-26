using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace FrameWork.Core
{
    public class Vertical : IMovemnt
    {
        private int speed;
        private Point boundary;
        private int set;


        public Vertical(int speed, Point boundary)
        {
            this.speed = speed;
            this.boundary = boundary;
            set = 100;
        }

        public Point move(Point location)
        {
            location.Y += speed;
            return location;
        }
    }
}
