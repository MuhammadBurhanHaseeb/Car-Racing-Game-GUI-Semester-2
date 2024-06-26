using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZInput;
using System.Windows.Forms;

namespace CarGame
{
    public partial class Game : Form
    {

      
        List<PictureBox> playerBulletList = new List<PictureBox>();
        List<PictureBox> enemyList = new List<PictureBox>();
        List<PictureBox> enemyBulletList = new List<PictureBox>();
        List<PictureBox> pilesList = new List<PictureBox>();
        List<PictureBox> hurdelsList = new List<PictureBox>();
        List<PictureBox> helicopterBombList = new List<PictureBox>();
        List<PictureBox> rocketList = new List<PictureBox>();
        string heliCopterStatus = "dead" , heliMovement = "left";
        PictureBox piles;
        PictureBox player;
        PictureBox enemy1, enemy2, enemy3 , helicopter;
        PictureBox bullets , helicopterBomb , Rocket;
        ProgressBar playerHealth , helicopterHealth;
        PictureBox enemyBullets;
        PictureBox hurdel1, hurdel2, hurdel3, hurdel4, hurdel5, hurdel6;
        private static int helCount  = 0 ,rCount = 0,moveHel = 0 ,   count=0 ,Bomb = 0 , scor , Loop , enemyBulletLoop , playerBulletCount, rocketCount = 0 , rocketMove = 0;
        Label score;
        Random rand = new Random();
        private void Restart()
        {
            scor = 0;
            this.Controls.Clear();
            createPlayer();
            playerBulletList = new List<PictureBox>();
            enemyList = new List<PictureBox>();
            enemyBulletList = new List<PictureBox>();
            pilesList = new List<PictureBox>();
            hurdelsList = new List<PictureBox>();
            helicopterBombList = new List<PictureBox>();
            rocketList = new List<PictureBox>();
            heliCopterStatus = "dead";
            heliMovement = "left";
            rocketCount = 0;
            rocketMove = 0;
            helCount = 0;
            Bomb = 0;
            moveHel = 0;
            count = 0;
            Loop = 0;
            enemyBulletLoop = 0;
            playerBulletCount = 0;
            rand = new Random();
            loopStart();

        }
        public Game()
        {
            InitializeComponent();
        }


      
        private void gameLoop_Tick(object sender, EventArgs e)
        {
            playerBulletCount++;
            if (Keyboard.IsKeyPressed(Key.LeftArrow))
            {
                if (player.Location.X > 0)
                {
                    player.Left = player.Left - 12;
                    playerHealth.Left = playerHealth.Left - 12;
                }
            }
            else if (Keyboard.IsKeyPressed(Key.RightArrow))
            {
                if (player.Location.X < 190 )
                {
                    player.Left = player.Left + 12;
                    playerHealth.Left = playerHealth.Left + 12;
                }
            }
            else if (Keyboard.IsKeyPressed(Key.UpArrow))
            {
                if (player.Top > 0)
                {
                    player.Top = player.Top - 12;
                    playerHealth.Top = playerHealth.Top - 12;
                }
            }
            else if (Keyboard.IsKeyPressed(Key.DownArrow))
            {
                if (player.Bottom < 400 + player.Width )
                {
                    player.Top = player.Top + 12;
                    playerHealth.Top = playerHealth.Top + 12;
                }
            }
            else if (Keyboard.IsKeyPressed(Key.Space))
            {
                if (playerBulletCount >=10)
                {
                    createBullets();
                    playerBulletCount = 0;
                }
            }
           if(scor >= 20 && scor <=39)
            {
                gameLoopEnemey.Interval = 10000;
                enemyCarSpeedLoop.Interval = 500;
                hurdelsLoop.Interval = 1000;
            }
            if (scor >= 40 && scor <=60)
            {
                gameLoopEnemey.Interval = 6000;
                enemyCarSpeedLoop.Interval = 300;
                hurdelsLoop.Interval = 700;
            }
           
            if (scor >=61 && playerHealth.Value >0)
            {
                loopStop();
                Image img2 = CarGame.Properties.Resources.missionWin;
                ResultForm frm = new ResultForm(img2);
                 DialogResult  result =   frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
                else if (result == DialogResult.Yes)
                {
                    Restart();
                }
            }
            if (scor <=60 && playerHealth.Value <=0)
            {
                loopStop();
                Image img = CarGame.Properties.Resources.missionFail;
                ResultForm frm = new ResultForm(img);
                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
                else if (result == DialogResult.Yes)
                {
                    Restart();
                }

            }

            if (hurdelsList.Count != 0)
            {
                hurdelDetection();
            }
            if (enemyList.Count != 0)
            {
                enemyDetection();
            }
            helCount++;
            if (helCount >= 100)
            {
                if (heliCopterStatus == "dead")
                {
                    createHelicopter();
                }
                helCount = 0;
            }
            moveHel++;
            if (moveHel >= 10)
            {
                moveHelicopter();
                moveHel = 0;
            }
            rocketCount++;
            if(rocketCount >=250)
            {
                createRocket();
                rocketCount = 0;
            }

            checkHelicopter();

            createBomb();
            createEnemyBullets();
            createPile();
            movePiles();
            moveBomb();
            moveRocket();
            movePlayerBullet();
            moveEnemyBullet();
            moveHurdels();
            removePiles();
            removeRocket();
            removeBomb();
            removePlayerBullets();
            removeHurdels();
            removeEnemys();
            removeEnemyBullets();
            playerCollisionWithEnemy();
            playerCollisionWithHelicopter();
            playerBulletDetectWithHelicopter();
            playerCollisionWithHurdels();
            playerCollisionWithPiles();
            enemyBulletDetectionWithPlayer();
            helicopterBombDetectionWithPlayer();
            rocketDetecttWithPlayer();
            displayScore();
        }

        private void rocketDetecttWithPlayer()
        {
            for (int y = 0; y < rocketList.Count; y++)
            {
                if (rocketList[y].Bounds.IntersectsWith(player.Bounds))
                {
                    if (playerHealth.Value - 60 <= 0)
                    {
                        playerHealth.Value = 0;
                        this.Controls.Remove(rocketList[y]);
                        rocketList.Remove(rocketList[y]);
                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 60;
                        this.Controls.Remove(rocketList[y]);
                        rocketList.Remove(rocketList[y]);
                    }
                }
            }
        }
        private void removeRocket()
        {

            for (int x = 0; x < rocketList.Count; x++)
            {
                if (rocketList[x].Location.Y > this.Height)
                {
                    this.Controls.Remove(rocketList[x]);
                    rocketList.Remove(rocketList[x]);
                }
            }
        }
        private void moveRocket()
        {
            if(rocketList.Count>0)
            {
                foreach(PictureBox s in rocketList)
                {
                    s.Top = s.Top + 20;
                }

            }
        }
        private void createRocket()
        {
            Rocket = new PictureBox();
            System.Drawing.Point location = new System.Drawing.Point();
            Image img  = CarGame.Properties.Resources.rocket3; ;
            rCount++;
            if (rCount == 0 || rCount == 1)
            {
                 img = CarGame.Properties.Resources.rocket1;
                location.X = 38;
                location.Y = -5;
            }
            else if (rCount == 2 || rCount == 3)
            {
                 img = CarGame.Properties.Resources.rocket2;
                location.X = 134;
                location.Y = -5;
            }
            else if (rCount == 4 || rCount == 5)
            {
                 img = CarGame.Properties.Resources.rocket3;
                location.X = 205;
                location.Y = -5;
                rCount = 0;
            }
            Rocket.Image = img;
            Rocket.Size = new Size(40, 70);
            Rocket.SizeMode = PictureBoxSizeMode.Zoom;
            Rocket.Location = location;
            Rocket.BackColor = Color.Transparent;
            rocketList.Add(Rocket);
            this.Controls.Add(Rocket);
        }
        private void checkHelicopter()
        {
            if (heliCopterStatus =="alive")
            {
                if (helicopterHealth.Value <= 0)
                {
                    this.Controls.Remove(helicopter);
                    this.Controls.Remove(helicopterHealth);
                    heliCopterStatus = "dead";
                    scor = scor + 10;
                }
            }
            
        }
         private void moveHelicopter()
        {
            if (heliCopterStatus == "alive")
            {
                if (heliMovement == "left")
                {
                    if (helicopter.Location.X < 190)
                    {
                        helicopterHealth.Left = helicopterHealth.Left + 10;
                        helicopter.Left = helicopter.Left + 10;
                    }
                    else
                    {
                        heliMovement = "right";
                    }
                }
                else if (heliMovement == "right")
                {
                    if (helicopter.Location.X  >0)
                    {
                        helicopterHealth.Left = helicopterHealth.Left - 10;
                        helicopter.Left = helicopter.Left - 10;
                    }
                    else
                    {
                        heliMovement = "left";
                    }
                }
                 
               
            }
        }
        private void loopStart()
        {
            gameLoop.Enabled = true;
            gameLoopEnemey.Enabled = true;
            enemyCarSpeedLoop.Enabled = true;
            hurdelsLoop.Enabled = true;
        }
        private void loopStop()
        {
            enemyList.Clear();
            gameLoop.Enabled = false;
            gameLoopEnemey.Enabled = false;
            enemyCarSpeedLoop.Enabled = false;
            hurdelsLoop.Enabled = false;
        }
        private void moveBomb()
        {
            if (helicopterBombList.Count >0)
            {
                foreach (PictureBox s in helicopterBombList)
                {
                    helicopterBomb.Top = helicopterBomb.Top + 10;
                }
            }
        }
        private void createBomb()
        {
            Bomb++;
            if (Bomb>=200)
            {
                if(heliCopterStatus == "alive") 
                {
                    helicopterBomb = new PictureBox();
                    Image img = CarGame.Properties.Resources.bomb;
                    helicopterBomb.Image = img;
                    helicopterBomb.Size = new Size(30, 30);
                    helicopterBomb.SizeMode = PictureBoxSizeMode.Zoom;
                    System.Drawing.Point location = new System.Drawing.Point();
                    location.X = helicopter.Left + (helicopter.Width / 2) - 20;
                    location.Y = helicopter.Top;
                    helicopterBomb.Location = location;
                    helicopterBomb.BackColor = Color.Transparent;
                    helicopterBombList.Add(helicopterBomb);
                    this.Controls.Add(helicopterBomb);
                }
                Bomb = 0;
            }
        }
        private void createEnemyBullets()
        {
            enemyBulletLoop++;
            if (enemyBulletLoop >= 200)
            {
                if (enemyList.Count != 0)
                {
                    foreach (PictureBox enemy in enemyList)
                    {
                        createEnemyBullet(enemy);
                    }

                }
                enemyBulletLoop = 0;
            }
        }
        private void  movePlayerBullet()
        {
            foreach (PictureBox bullet in playerBulletList)
            {
                bullet.Top = bullet.Top - 10;
            }
        }
        private void moveEnemyBullet()
        {
            foreach (PictureBox enemybullet in enemyBulletList)
            {
                enemybullet.Top = enemybullet.Top + 10;
            }
        }
        
        private void moveEnemys()
        {
            foreach (PictureBox enemy in enemyList)
            {

                enemy.Top = enemy.Top + 10;
            }
          
        }
        private void movePiles()
        {
            foreach (PictureBox pile in pilesList)
            {
                pile.Top = pile.Top + 10;
            }

           
        }
        private void createPile()
        {
            Loop++;
            if (Loop >= 700)
            {
                createPiles();
                Loop = 0;
            }
        }
        private void moveHurdels()
        {
            foreach (PictureBox hurdels in hurdelsList)
            {
                hurdels.Top = hurdels.Top + 10;
            }
        }
        private void removeEnemys()
        {
            for (int x = 0; x < enemyList.Count; x++)
            {
                if (enemyList[x].Location.Y > this.Height)
                {
                    this.Controls.Remove(enemyList[x]);
                    enemyList.Remove(enemyList[x]);
                }
            }
        }
        private void removeBomb()
        {
            for (int x = 0; x <helicopterBombList.Count; x++)
            {
                if (helicopterBombList[x].Location.Y > this.Height)
                {
                    this.Controls.Remove(helicopterBombList[x]);
                    helicopterBombList.Remove(helicopterBombList[x]);
                }
            }


        }



        private void removeHurdels()
        {
            for (int idx = 0; idx < hurdelsList.Count; idx++)
            {
                if (hurdelsList[idx].Location.Y > 500)
                {
                    this.Controls.Remove(hurdelsList[idx]);
                    hurdelsList.Remove(hurdelsList[idx]);
                }
            }
        }
        private void removePlayerBullets()
        {
            for (int x = 0; x < playerBulletList.Count; x++)
            {
                if (playerBulletList[x].Bottom < 0)
                {
                    this.Controls.Remove(playerBulletList[x]);
                    playerBulletList.Remove(playerBulletList[x]);
                }
            }
        }
        private void removeEnemyBullets()
        {
            for (int x = 0; x < enemyBulletList.Count; x++)
            {
                if (enemyBulletList[x].Top > this.Height)
                {
                    this.Controls.Remove(enemyBulletList[x]);
                    enemyBulletList.Remove(enemyBulletList[x]);
                }
            }
        }
        private void removePiles()
        {
            for (int x = 0; x < pilesList.Count; x++)
            {
                if (pilesList[x].Location.Y > this.Height)
                {
                    this.Controls.Remove(pilesList[x]);
                    pilesList.Remove(pilesList[x]);
                }
            }
        }
        private void playerCollisionWithPiles()
        {
            for (int y = 0; y < pilesList.Count; y++)
            {
                if (pilesList[y].Bounds.IntersectsWith(player.Bounds))
                {
                    if (playerHealth.Value <= 90)
                    {
                        playerHealth.Value = playerHealth.Value + 10;
                        this.Controls.Remove(pilesList[y]);
                        pilesList.Remove(pilesList[y]);
                    }
                }
            }
        }
        private void displayScore()
        {
            score.Text = "Score:" + scor.ToString();
        }

        private void playerCollisionWithHelicopter()
        {
            if(heliCopterStatus == "alive")
            {
                if (helicopter.Bounds.IntersectsWith(player.Bounds))
                {
                    helicopterHealth.Value = 0;
                    if (playerHealth.Value - 30 < 0)
                    {
                        playerHealth.Value = 0;
                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 30;
                    }
                }
            }
        }
        private void playerCollisionWithEnemy()
        {
            for (int y = 0; y < enemyList.Count; y++)
            {
                if (enemyList[y].Bounds.IntersectsWith(player.Bounds))
                {
                    if (playerHealth.Value >=10 )
                    {
                        playerHealth.Value = playerHealth.Value - 10;
                        this.Controls.Remove(enemyList[y]);
                        enemyList.Remove(enemyList[y]);
                    }
                }
            }
        }
        private void playerCollisionWithHurdels()
        {
            for (int y = 0; y < hurdelsList.Count; y++)
            {
                if (hurdelsList[y].Bounds.IntersectsWith(player.Bounds))
                {
                    if (playerHealth.Value >= 10)
                    {
                        playerHealth.Value = playerHealth.Value - 10;
                        this.Controls.Remove(hurdelsList[y]);
                        hurdelsList.Remove(hurdelsList[y]);
                    }
                }
            }
        }
        private void playerBulletDetectWithHelicopter()
        {
            if (heliCopterStatus == "alive")
            {
                for (int y = 0; y < playerBulletList.Count; y++)
                {
                    if (playerBulletList[y].Bounds.IntersectsWith(helicopter.Bounds))
                    {
                        if(helicopterHealth.Value - 10 <= 0)
                        {
                            helicopterHealth.Value = 0;
                        }
                        else
                        {
                            helicopterHealth.Value = helicopterHealth.Value - 10;
                        }
                        this.Controls.Remove(playerBulletList[y]);
                        playerBulletList.Remove(playerBulletList[y]);
                    }
                }
            }
        }
        private void enemyDetection()
        {
            for (int y = 0; y < playerBulletList.Count; y++)
            {
                for (int x = 0; x < enemyList.Count; x++)
                 {
               
                     if (playerBulletList[y].Bounds.IntersectsWith(enemyList[x].Bounds))
                    {
                           
                            this.Controls.Remove(playerBulletList[y]);
                            this.Controls.Remove(enemyList[x]);
                             playerBulletList.Remove(playerBulletList[y]);
                             enemyList.Remove(enemyList[x]);
                              scoreChange(ref scor);
                    }
                }
            }
        }
        private void hurdelDetection()
        {
            for (int x = 0; x < playerBulletList.Count; x++)
            {
                for (int y = 0; y < hurdelsList.Count; y++)
                {

                    if (bullets.Bounds.IntersectsWith(hurdelsList[y].Bounds))
                        {
                            this.Controls.Remove(playerBulletList[x]);
                            this.Controls.Remove(hurdelsList[y]);
                            playerBulletList.Remove(playerBulletList[x]);
                            hurdelsList.Remove(hurdelsList[y]);
                            scoreChange(ref scor);
                        }
                }
            }
        }

        private void enemyBulletDetectionWithPlayer()
        {
            for (int y = 0; y < enemyBulletList.Count; y++)
            {
                if (enemyBulletList[y].Bounds.IntersectsWith(player.Bounds))
                {
                    if (playerHealth.Value -10 <= 0)
                    {
                        playerHealth.Value = 0;
                        this.Controls.Remove(enemyBulletList[y]);
                        enemyBulletList.Remove(enemyBulletList[y]);
                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 10;
                        this.Controls.Remove(enemyBulletList[y]);
                        enemyBulletList.Remove(enemyBulletList[y]);
                    }
                }
            }
        }

        private void helicopterBombDetectionWithPlayer()
        {
            for (int y = 0; y <helicopterBombList.Count; y++)
            {
                if (helicopterBombList[y].Bounds.IntersectsWith(player.Bounds))
                {
                    if (playerHealth.Value - 40 <= 0)
                    {
                        playerHealth.Value = 0;
                        this.Controls.Remove(helicopterBombList[y]);
                        helicopterBombList.Remove(helicopterBombList[y]);
                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 40;
                        this.Controls.Remove(helicopterBombList[y]);
                        helicopterBombList.Remove(helicopterBombList[y]);
                    }
                }
            }
        }
        private void scoreChange(ref int score)
        {
            score = score + 1;
        }
        private void Game_Load(object sender, EventArgs e)
        {
            createPlayer();
          
        }
        private void createPlayer()
        {
            System.Drawing.Point location = new System.Drawing.Point();
            score = new Label();
            score.Size = new Size(111,52);
            score.BackColor = Color.Transparent;
            Font MediumFont = new Font("ArialBold",14);
            score.Font = MediumFont;
            location.X =10 ;
            location.Y = 1;
            score.Location = location;

            player = new PictureBox();
            Image img = CarGame.Properties.Resources.myCar;
            player.Image = img;
            player.Size = new Size(83, 100);
            player.SizeMode = PictureBoxSizeMode.Zoom;
           
            location.X = 95;
            location.Y =370;
            player.Location = location; 
            player.BackColor = Color.Transparent;

            playerHealth = new ProgressBar();
            playerHealth.Size = new Size(20, 10);
            playerHealth.Top = player.Bottom + 2;
            playerHealth.Value = 100;
            playerHealth.Step = 10;
            location.X = 125;
            location.Y = 470 ;
            playerHealth.Location = location;

            this.Controls.Add(playerHealth);
            this.Controls.Add(player);
            this.Controls.Add(score);


        }
        private void createEnemy1()
        {
            enemy1 = new PictureBox();
            Image img = CarGame.Properties.Resources.myEnemy1;
            enemy1.Image = img;
            enemy1.Size = new Size(60, 90);
            enemy1.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 20;
            location.Y = 0;
            enemy1.Location = location;
            enemy1.BackColor = Color.Transparent;
            enemyList.Add(enemy1);
            this.Controls.Add(enemy1);
        }
        private void createEnemy2()
        {
            enemy2 = new PictureBox();
            Image img = CarGame.Properties.Resources.myEnemy2;
            enemy2.Image = img;
            enemy2.Size = new Size(60, 90);
            enemy2.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 107;
            location.Y = 0;
            enemy2.Location = location;
            enemy2.BackColor = Color.Transparent;

            enemyList.Add(enemy2);
            this.Controls.Add(enemy2);



        }
        private void createEnemy3()
        {
            enemy3 = new PictureBox();
            Image img = CarGame.Properties.Resources.myEnymy3;
            enemy3.Image = img;
            enemy3.Size = new Size(60, 90);
            enemy3.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 187;
            location.Y = 0;
            enemy3.Location = location;
            enemy3.BackColor = Color.Transparent;

            enemyList.Add(enemy3);
            this.Controls.Add(enemy3);



        }
        private void createBullets()
        {
            bullets = new PictureBox();
            Image img = CarGame.Properties.Resources.myBullet;
            bullets.Image = img;
            bullets.Size = new Size(50, 50);
            bullets.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X =player.Left +( player.Width/2) -25;
            location.Y = player.Top - 20;
            bullets.Location = location;
            bullets.BackColor = Color.Transparent;
            playerBulletList.Add(bullets);
            // player.Top = this.Height;
            //player.Size = new Size(60 , 100);
            this.Controls.Add(bullets);
        }

        private void gameLoopEnemey_Tick(object sender, EventArgs e)
        {
            int x = rand.Next(1,3);
            if (x == 1)
            {
                createEnemy1();
            }
            else if (x == 2)
            {
                createEnemy2();
            }
            else if (x == 3)
            {
                createEnemy3();
            }
          
        }

        private void createPiles()
        {
            piles = new PictureBox();
            Image img = CarGame.Properties.Resources.power;
            piles.Image = img;
            piles.Size = new Size(20, 50);
            piles.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            count++;
            if (count == 0 || count == 1)
            {
                location.X = 38;
                location.Y = -5;
            }
            else if (count == 2 || count ==3)
            {
                location.X = 134;
                location.Y = -5;
            }
            else if (count == 4 || count == 5)
            {
                location.X = 205;
                location.Y = -5;
                count = 0;
            }
            piles.Location = location;
            piles.BackColor = Color.Transparent;

            pilesList.Add(piles);
            this.Controls.Add(piles);
        }
        private void enemyCarSpeedLoop_Tick(object sender, EventArgs e)
        {
            moveEnemys();
        }


          private void createEnemyBullet(PictureBox enemy)
          {
              enemyBullets = new PictureBox();
              Image img = CarGame.Properties.Resources.enemyFire;
              enemyBullets.Image = img;
              enemyBullets.Size = new Size(50, 50);
              enemyBullets.SizeMode = PictureBoxSizeMode.Zoom;
              System.Drawing.Point location = new System.Drawing.Point();
              location.X = enemy.Left+( enemy.Width/2) - 20 ;
              location.Y = enemy.Top;
              enemyBullets.Location = location;
              enemyBullets.BackColor = Color.Transparent;
              enemyBulletList.Add(enemyBullets);
              this.Controls.Add(enemyBullets);
          }

        private void createHurdel1()
        {
            hurdel1 = new PictureBox();
            Image img = CarGame.Properties.Resources.hurdel1;
            hurdel1.Image = img;
            hurdel1.Size = new Size(60, 90);
            hurdel1.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 187;
            location.Y = -5;
            hurdel1.Location = location;
            hurdel1.BackColor = Color.Transparent;

            hurdelsList.Add(hurdel1);
            this.Controls.Add(hurdel1);



        }

        private void hurdelsLoop_Tick(object sender, EventArgs e)
        {
            int y = rand.Next(1, 6);
            if (y == 1)
            {
                createHurdel1();
            }
            else if(y==2)
            {
                createHurdel2();
            }
            else if (y == 3)
            {
                createHurdel3();
            }
            else if (y == 4)
            {
                createHurdel4();
            }
            else if (y == 5)
            {
                createHurdel5();
            }
            else if (y == 6)
            {
                createHurdel6();
            }
        }

        private void createHurdel2()
        {
            hurdel2 = new PictureBox();
            Image img = CarGame.Properties.Resources.hurdel2;
            hurdel2.Image = img;
            hurdel2.Size = new Size(60, 90);
            hurdel2.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 187;
            location.Y = -5;
            hurdel2.Location = location;
            hurdel2.BackColor = Color.Transparent;

            hurdelsList.Add(hurdel2);
            this.Controls.Add(hurdel2);
        }
        private void createHurdel3()
        {
            hurdel3 = new PictureBox();
            Image img = CarGame.Properties.Resources.hurdel3;
            hurdel3.Image = img;
            hurdel3.Size = new Size(60, 90);
            hurdel3.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 107;
            location.Y = -5;
            hurdel3.Location = location;
            hurdel3.BackColor = Color.Transparent;

            hurdelsList.Add(hurdel3);
            this.Controls.Add(hurdel3);



        }
        private void createHurdel4()
        {
            hurdel4 = new PictureBox();
            Image img = CarGame.Properties.Resources.hurdel4;
            hurdel4.Image = img;
            hurdel4.Size = new Size(60, 90);
            hurdel4.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 107;
            location.Y = -5;
            hurdel4.Location = location;
            hurdel4.BackColor = Color.Transparent;

            hurdelsList.Add(hurdel4);
            this.Controls.Add(hurdel4);



        }
        private void createHurdel5()
        {
            hurdel5 = new PictureBox();
            Image img = CarGame.Properties.Resources.hurdel5;
            hurdel5.Image = img;
            hurdel5.Size = new Size(60, 90);
            hurdel5.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 20;
            location.Y = -5;
            hurdel5.Location = location;
            hurdel5.BackColor = Color.Transparent;

            hurdelsList.Add(hurdel5);
            this.Controls.Add(hurdel5);


        }
        private void createHurdel6()
        {
            hurdel6 = new PictureBox();
            Image img = CarGame.Properties.Resources.hurdel6;
            hurdel6.Image = img;
            hurdel6.Size = new Size(60, 90);
            hurdel6.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 20;
            location.Y = -5;
            hurdel6.Location = location;
            hurdel6.BackColor = Color.Transparent;

            hurdelsList.Add(hurdel6);
            this.Controls.Add(hurdel6);



        }

        private void createHelicopter()
        {
            System.Drawing.Point location = new System.Drawing.Point();


            helicopter = new PictureBox();
            helicopterHealth = new ProgressBar();
            helicopterHealth.Value = 100;
            Image img = CarGame.Properties.Resources.hell;
            helicopter.Image = img;
            helicopter.Size = new Size(70, 70);
            helicopter.SizeMode = PictureBoxSizeMode.Zoom;
            heliCopterStatus = "alive";
            location.X = 20;
            location.Y = 60;
            helicopter.Location = location;
            helicopter.BackColor = Color.Transparent;
            helicopterHealth.Size = new Size(20, 10);
            location.X = 30;
            location.Y = 56;
            helicopterHealth.Location = location;
            this.Controls.Add(helicopter);
            this.Controls.Add(helicopterHealth);
        }
    }
}
