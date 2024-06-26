using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace FrameWork.Core
{
   public  class GameObject
    {
        private GameObjectName otherType;
        private PictureBox pb;
        private IMovemnt movement;
        private ProgressBar health;
        public GameObject(Image img ,GameObjectName otherType, int X , int Y , int width , int height  , IMovemnt movement )
        {
            System.Drawing.Point location = new System.Drawing.Point();
            Pb = new PictureBox();
            Pb.Image = img;
            pb.Size = new Size(width, height);
            Pb.SizeMode = PictureBoxSizeMode.Zoom;
            Pb.BackColor = Color.Transparent;
            location.X = X;
            location.Y = Y;
            Pb.Location = location;
            this.Movement = movement;
            this.OtherType = otherType;
        }
        public GameObject(int width, int height, int x, int y, GameObjectName otherType , IMovemnt movement)
        {
            System.Drawing.Point location = new System.Drawing.Point();
            Health = new ProgressBar();
            Health.Size = new Size(width, height);
            Health.Value = 100;
            location.X = x;
            location.Y = y;
            Health.Location = location;
            this.Movement = movement;
            this.OtherType = otherType;
        }
        public PictureBox Pb { get => pb; set => pb = value; }
        public IMovemnt Movement { get => movement; set => movement = value; }
        public GameObjectName OtherType { get => otherType; set => otherType = value; }
        public ProgressBar Health { get => health; set => health = value; }

        public void update()
        {
            if (OtherType == GameObjectName.playerHealth)
            {
                health.Location = Movement.move(health.Location);
            }
            if( OtherType != GameObjectName.playerHealth)
            {
                pb.Location = Movement.move(pb.Location);
            }
           
        }

        public Point getObjectLocation()
        {
            return pb.Location;
        }
    }
}
