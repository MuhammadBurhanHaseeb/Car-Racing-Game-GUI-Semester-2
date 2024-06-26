using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace FrameWork.Core
{
   public  class KeyBoard :IMovemnt
    {
        private string arrowKeyAction ; 
        private int speed;
        private Point boundary;
        private int set;
        public KeyBoard(int speed , Point boundary)
        {
            this.speed = speed;
            this.boundary = boundary;
            arrowKeyAction = null;
             set = 100;
        }

       public  Point move(Point location )
        {
            if (arrowKeyAction != null)
            {
                if (arrowKeyAction == "up")
                {
                    if (location.Y > 0)
                    {
                        location.Y -= speed;
                    }
                }
                else if (arrowKeyAction == "down")
                {
                    if (location.Y  + set+50 < boundary.Y)
                    {
                        location.Y += speed;
                    }
                }
                else if (arrowKeyAction == "right")
                {
                    if (location.X + set < boundary.X)
                    {
                        location.X += speed;
                    }
                }
                else if (arrowKeyAction == "left")
                {
                    if (location.X > 0)
                    {
                        location.X -= speed;
                    }
                }
                arrowKeyAction = null;
            }
            return location;
        }

        public void keyPressedByUser(Keys keyCode)
        {
            if (keyCode == Keys.Up)
            {
                arrowKeyAction = "up";
            }
            else if (keyCode == Keys.Down)
            {
                arrowKeyAction = "down";
            }
            else if (keyCode == Keys.Right)
            {
                arrowKeyAction = "right";
            }
            else if (keyCode == Keys.Left)
            {
                arrowKeyAction = "left";
            }
        }
    }
}
