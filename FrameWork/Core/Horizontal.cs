using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FrameWork.Core
{
   public  class Horizontal:IMovemnt
    {
        private int speed;
        private Point boundary;
        private string direction;
        private int set;


        public Horizontal(int speed , Point boundary , string direction )
        {
            this.speed = speed;
            this.boundary = boundary;
            this.direction = direction;
            set = 100;
        }

       public  Point move(Point location)
       {
           if (location.X <=0)
            {
                direction = "right";
            }
           else if (location.X + set >= boundary.X)
            {
                direction = "left";
            }
           if (direction == "left")
            {
                location.X -= speed; 
            }
           else if (direction =="right")
            {
                location.X += speed;
            }
            return location;
       }
    }
}
