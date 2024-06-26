using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrameWork.Core;

namespace Consumer
{
    public partial class GameLevel1 : Form
    {
        private  Game g;
      //  private  Label s;
        private string heliCopterStatus = "dead";
        private Point boundary;
        private int helCount = 0 , rocketCount = 0 , rCount = 0 , enemyCount = 0  , hurdelCount = 0 , pilesCount = 0;
        Random rand = new Random();
        public GameLevel1()
        {
            InitializeComponent();
        }

        private void GameLevel1_Load(object sender, EventArgs e)
        {
            g = new Game(this.Height);
            g.onAddingGameObject += new EventHandler(addIntoControls);
            g.onAddingGameObjectHealth += new EventHandler(healthAddIntoControls);
            g.onObjectRemove += new EventHandler(removeOutObjects);
            g.playerDieEvent += new EventHandler(removePlayer);
            g.onRemoveEnemy += new EventHandler(removeEnemy);
            boundary = new Point(this.Width,this.Height);
            g.addGameObject(Consumer.Properties.Resources.player,GameObjectName.player, 95, 370,83 , 100 ,  new KeyBoard(15, boundary));
            g.addGameObject(20,10 ,125 , 470  , GameObjectName.playerHealth , new KeyBoard(15 , boundary));
            ColisionClass  a = new ColisionClass(GameObjectName.player , GameObjectName.otherType,new PlayerCollision());
            g.addCollisions(a);
            ColisionClass  b = new ColisionClass(GameObjectName.player, GameObjectName.piles, new PlayerCollision());
          //  g.addCollisions(b);
        }         //   s = new Label(111, 52, 10, 1);


        private void addIntoControls(object sender , EventArgs e)
        {
            this.Controls.Add((PictureBox)sender);
        }
        private void removeEnemy(object sender, EventArgs e)
        {
            this.Controls.Remove((PictureBox)sender);
        }

        private void healthAddIntoControls(object sender, EventArgs e)
        {
            this.Controls.Add((ProgressBar)sender);
        }
        private void removeOutObjects(object sender, EventArgs e)
        {
            this.Controls.Add((PictureBox)sender);
        }
        private void removePlayer(object sender, EventArgs e)
        {
            this.Controls.Remove((PictureBox)sender);
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            helCount++;
            if (helCount >= 100)
            {
                if (heliCopterStatus == "dead")
                {
                    g.addGameObject(Consumer.Properties.Resources.helicopter, GameObjectName.otherType, 20, 60,70 , 70, new Horizontal(2, boundary, "left"));
                    heliCopterStatus = "alive";
                }
                helCount = 0;
            }
            rocketCount++;
            if (rocketCount >= 250)
            {
                rCount++;
                if (rCount == 0 || rCount == 1)
                {
                    g.addGameObject(Consumer.Properties.Resources.rocket1, GameObjectName.otherType, 38 , -5 , 40 , 70 , new Vertical(15 , boundary  ) );
                }
                else if (rCount == 2 || rCount == 3)
                {
                    g.addGameObject(Consumer.Properties.Resources.rocket2, GameObjectName.otherType, 134, -5,40 , 70, new Vertical(15, boundary));
                }
                else if (rCount == 4 || rCount == 5)
                {
                    g.addGameObject(Consumer.Properties.Resources.rocket3, GameObjectName.otherType, 205, -5, 40 , 70, new Vertical(15, boundary));
                    rCount = 0;
                }
                rocketCount = 0;
            }
            enemyCount++;
            if (enemyCount >= 300 )
            {
                int x = rand.Next(1, 3);
                if (x == 1)
                {
                    g.addGameObject(Consumer.Properties.Resources.enemy1, GameObjectName.otherType, 20, 0, 60, 90, new Vertical(2, boundary));
                }
                else if (x == 2)
                {
                    g.addGameObject(Consumer.Properties.Resources.enemy2, GameObjectName.otherType, 107 , 0, 60, 90, new Vertical(2, boundary));
                }
                else if (x == 3)
                {
                    g.addGameObject(Consumer.Properties.Resources.enemy3, GameObjectName.otherType, 187 , 0, 60, 90, new Vertical(2, boundary));
                }
                enemyCount = 0;
            }

            hurdelCount++; 
            if (hurdelCount >=150  )
            {
                int x = rand.Next(1, 6);
                if (x == 1)
                {
                    g.addGameObject(Consumer.Properties.Resources.hurdel1, GameObjectName.otherType, 20, 0, 50, 70, new Vertical(10, boundary));
                }
                else if (x == 2)
                {
                    g.addGameObject(Consumer.Properties.Resources.hurdel2, GameObjectName.otherType, 107, 0, 50, 70, new Vertical(10, boundary));
                }
                else if (x == 3)
                {
                    g.addGameObject(Consumer.Properties.Resources.hurdel3, GameObjectName.otherType, 187, 0, 50, 70, new Vertical(10, boundary));
                }
                else if (x == 4)
                {
                    g.addGameObject(Consumer.Properties.Resources.hurdel4, GameObjectName.otherType, 20, 0, 50, 70, new Vertical(10, boundary));
                }
                else if (x == 5)
                {
                    g.addGameObject(Consumer.Properties.Resources.hurdel5, GameObjectName.otherType, 107, 0, 50, 70, new Vertical(10, boundary));
                }
                else if (x == 6)
                {
                    g.addGameObject(Consumer.Properties.Resources.hurdel6, GameObjectName.otherType, 187, 0, 50, 70, new Vertical(10, boundary));
                }
                hurdelCount = 0;
            }
            pilesCount++;
            if (pilesCount >= 400 )
            {
                int x = rand.Next(1, 3);
                if (x == 1)
                {
                    g.addGameObject(Consumer.Properties.Resources.piles, GameObjectName.piles, 20, 0, 20, 50, new Vertical(10, boundary));
                }
                else if (x == 2)
                {
                    g.addGameObject(Consumer.Properties.Resources.piles, GameObjectName.piles, 107, 0, 20, 50, new Vertical(10, boundary));
                }
                else if (x == 3)
                {
                    g.addGameObject(Consumer.Properties.Resources.piles, GameObjectName.piles, 187, 0, 20, 50, new Vertical(10, boundary));
                }
                pilesCount = 0;
            }
            g.Update();
            g.removeObjects();
        }

        private void GameLevel1_KeyDown(object sender, KeyEventArgs e)
        {
            g.KeyPressed(e.KeyCode); 
        }
    }
}
