using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using EZInput;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarGame
{
    public partial class gameLevel2 : Form
    {
        List<PictureBox> playerBulletList = new List<PictureBox>();
        List<PictureBox> enemyList = new List<PictureBox>();
        List<PictureBox> enemyBulletList = new List<PictureBox>();
        List<PictureBox> pilesList = new List<PictureBox>();
        List<PictureBox> BigEnemyList = new List<PictureBox>();
        List<PictureBox> BigEnemyBulletList = new List<PictureBox>();
        List<ProgressBar> enemyHealthList = new List<ProgressBar>();
        List<PictureBox> rocketList = new List<PictureBox>();
        List<PictureBox> helicopterBombList = new List<PictureBox>();
        string heliCopterStatus = "dead", heliMovement = "left" , bhbEnemyStatus = "dead" , bhbBullet1Status = "dead", bhbBullet2Status = "dead", bhbBullet3Status = "dead";
        PictureBox piles;
        PictureBox player , Rocket , helicopter;
        PictureBox enemy1, enemy2, enemy3, enemy , bhbEnemy;
        PictureBox bullets , helicopterBomb , bhbBullet1 , bhbBullet2, bhbBullet3;
        ProgressBar playerHealth, enemyHealth , helicopterHealth , bhbEnemyHealth;
        PictureBox enemyBullets;
        private static int rCount = 0, rocketCount = 0, bhbCount = 0,  helCount = 0 , Bomb , moveHel = 0, count = 0, countt = 0, scor=0, Loop=0, enemyBulletLoop=0, bigEnemyBulletLoop=0 , big = 0, playerBulletCount=0;
        private void Restart()
        {
            scor = 0;
            playerHealth.Value = 100;
            this.Controls.Clear();
             createPlayer();
             playerBulletList = new List<PictureBox>();
             enemyList = new List<PictureBox>();
             enemyBulletList = new List<PictureBox>();
             pilesList = new List<PictureBox>();
            BigEnemyList = new List<PictureBox>();
            BigEnemyBulletList = new List<PictureBox>();
             enemyHealthList = new List<ProgressBar>();
             rocketList = new List<PictureBox>();
            helicopterBombList = new List<PictureBox>();
            heliCopterStatus = "dead";
            heliMovement = "left";
            bhbEnemyStatus = "dead";
            bhbBullet1Status = "dead";
            bhbBullet2Status = "dead";
            bhbBullet3Status = "dead";
            rocketCount = 0;
            bhbCount = 0;
            helCount = 0;
            Bomb = 0;
            moveHel = 0;
            count = 0;
            countt = 0;
            rCount = 0;
            Loop = 0;
            enemyBulletLoop = 0;
            bigEnemyBulletLoop = 0;
            big = 0;
            playerBulletCount = 0;
            rand = new Random();
            loopStart();

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
                if (player.Location.X < 190)
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
                if (player.Bottom < 400 + player.Width)
                {
                    player.Top = player.Top + 12;
                    playerHealth.Top = playerHealth.Top + 12;
                }
            }
            else if (Keyboard.IsKeyPressed(Key.Space))
            {
                if(playerBulletCount >= 10)
                {
                    createBullets();
                    playerBulletCount = 0;
                }
                   
            }

           if (scor >= 20 && scor <= 40)
            {
                enemyGameLoop.Interval =6000;
                enemyCarSpeedLoop.Interval = 500;
            }
            if (scor >= 41 && scor <= 60)
            {
                enemyGameLoop.Interval = 5000;
                enemyCarSpeedLoop.Interval = 400;
            }
            if (scor >= 61 && scor <= 80)
            {
                enemyGameLoop.Interval = 4500;
                enemyCarSpeedLoop.Interval = 300;
            }
            
            if (scor >= 101 && playerHealth.Value > 0)
            {
                loopEnd();
                Image img2 = CarGame.Properties.Resources.back1;
                ResultForm frm = new ResultForm(img2);
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
            if (scor <= 100 && playerHealth.Value <=0)
            {
                loopEnd();
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

            if (enemyList.Count != 0)
            {
                enemyDetection();
            }
            if(BigEnemyList.Count!=0)
            {
                  BigenemyDetection();
                  checkBigEnemy();
            }
            helCount++;
            if (helCount >= 500)
            {
                if (heliCopterStatus == "dead")
                {
                    createHelicopter();
                }
                helCount = 0;
            }
            bhbCount++;
            if (bhbCount >= 400)
            {
                if (bhbEnemyStatus == "dead")
                {
                    creteThrrefireEnemy();
                }
                bhbCount = 0;
            }
            moveHel++;
            if (moveHel >= 10)
            {
               moveHelicopter();
                moveHel = 0;
            }
            rocketCount++;
            if (rocketCount >= 600)
            {
                createRocket();
                rocketCount = 0;
            }
            if (bhbCount >=200)
            {
                if (bhbEnemyStatus == "alive")
                {
                    if(bhbBullet1Status == "dead" && bhbBullet2Status == "dead" && bhbBullet3Status == "dead")
                    {
                        createBhbEnemyBulllets();
                        bhbCount = 0;
                    }
                    
                }
            }
            checkBhbEnemy();
            moveBhbEnemyBullets();
            detectBhbEnemyWithPlayer();
            playerBulletCollisionWithBhbEnemy();
            removebhbBullets();
            checkHelicopter();
            createBomb();
            createBigEnemy();
            createEnemyBullets();
            createBigEnemyBullets();
            moveBigEnemyBullet();
            createPile();
            movePiles();
            moveBomb();
            moveRocket();
            movePlayerBullet();
            moveEnemyBullet();
            removePiles();
            removeRocket();
            removeBomb(); 
            removeBHBenemy();
            removeEnemys();
            removeEnemyBullets();
            playerCollisionWithEnemy();
            playerCollisionWithHelicopter();
            playerBulletDetectWithHelicopter();
            playerCollisionWithBhbBullets();
            playerCollisionWithPiles();
            enemyBulletDetectionWithPlayer();
            displayScore();
            removeBigEnemyBullets();
            playerCollisionWithBigEnemy();
            BigEnemyBulletDetectionWithPlayer();
            removePlayerBullets();
            helicopterBombDetectionWithPlayer();
            rocketDetecttWithPlayer(); 

        }

        Label score;
        Random rand = new Random();
        public gameLevel2()
        {
            InitializeComponent();
        }
        private void checkHelicopter()
        {
            if (heliCopterStatus == "alive")
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
        private void helicopterBombDetectionWithPlayer()
        {
            for (int y = 0; y < helicopterBombList.Count; y++)
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
        private void playerBulletDetectWithHelicopter()
        {
            if (heliCopterStatus == "alive")
            {
                for (int y = 0; y < playerBulletList.Count; y++)
                {
                    if (playerBulletList[y].Bounds.IntersectsWith(helicopter.Bounds))
                    {
                        if (helicopterHealth.Value - 10 <= 0)
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
        private void playerCollisionWithHelicopter()
        {
            if (heliCopterStatus == "alive")
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
        private void removeBomb()
        {
            for (int x = 0; x < helicopterBombList.Count; x++)
            {
                if (helicopterBombList[x].Location.Y > this.Height)
                {
                    this.Controls.Remove(helicopterBombList[x]);
                    helicopterBombList.Remove(helicopterBombList[x]);
                }
            }


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
            if (rocketList.Count > 0)
            {
                foreach (PictureBox s in rocketList)
                {
                    s.Top = s.Top + 15;
                }

            }
        }
       
        private void moveBomb()
        {
            if (helicopterBombList.Count > 0)
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
            if (Bomb >= 200)
            {
                if (heliCopterStatus == "alive")
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
                    if (helicopter.Location.X > 0)
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
        private void createRocket()
        {
            Rocket = new PictureBox();
            System.Drawing.Point location = new System.Drawing.Point();
            Image img = CarGame.Properties.Resources.rocket3; ;
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
        private void loopStart()
        {
            gameLoop.Enabled = true;
            enemyCarSpeedLoop.Enabled = true;
            enemyGameLoop.Enabled = true;
        }
        private void loopEnd()
        {
            gameLoop.Enabled = false;
            enemyCarSpeedLoop.Enabled = false;
            enemyGameLoop.Enabled = false;
        }
        private void gameLevel2_Load(object sender, EventArgs e)
        {
            createPlayer();
        }

        private void BigEnemyBulletDetectionWithPlayer()
         {
            for (int y = 0; y < BigEnemyBulletList.Count; y++)
            {
                if (player.Bounds.IntersectsWith(BigEnemyBulletList[y].Bounds))
                {
                        if (playerHealth.Value - 20  <=0 )
                        {
                            playerHealth.Value = 0;
                            this.Controls.Remove(BigEnemyBulletList[y]);
                            BigEnemyBulletList.Remove(BigEnemyBulletList[y]);
                        }
                        else
                        {
                            playerHealth.Value = playerHealth.Value - 20;
                            this.Controls.Remove(BigEnemyBulletList[y]);
                            BigEnemyBulletList.Remove(BigEnemyBulletList[y]);
                        }
                       
                      
                }
            }
        }
        private void createBigEnemyBullets()
        {
            bigEnemyBulletLoop++;
            if (bigEnemyBulletLoop >= 200)
            {
                if (BigEnemyList.Count >= 0)
                {
                    foreach (PictureBox enemy in BigEnemyList)
                    {
                        createBigEnemyBullet(enemy);
                    }

                }
                bigEnemyBulletLoop = 0;
            }
        }
        private void moveBigEnemyBullet()
        {
            foreach (PictureBox enemybullet in BigEnemyBulletList)
            {
                enemybullet.Top = enemybullet.Top + 10;
            }
        }
        private void removeBigEnemyBullets()
        {

            for (int x = 0; x < BigEnemyBulletList.Count; x++)
            {
                if (BigEnemyBulletList[x].Top > this.Height)
                {
                    this.Controls.Remove(BigEnemyBulletList[x]);
                    BigEnemyBulletList.Remove(BigEnemyBulletList[x]);
                }
            }
        }

        private void createPlayer()
        {
            System.Drawing.Point location = new System.Drawing.Point();
            score = new Label();
            score.Size = new Size(111, 52);
            score.BackColor = Color.Transparent;
            Font MediumFont = new Font("ArialBold", 14);
            score.Font = MediumFont;
            location.X = 10;
            location.Y = 1;
            score.Location = location;

            player = new PictureBox();
            Image img = CarGame.Properties.Resources.myCar;
            player.Image = img;
            player.Size = new Size(83, 100);
            player.SizeMode = PictureBoxSizeMode.Zoom;

            location.X = 95;
            location.Y = 370;
            player.Location = location;
            player.BackColor = Color.Transparent;

            playerHealth = new ProgressBar();
            playerHealth.Size = new Size(20,10);
            playerHealth.Top = player.Bottom + 2;
            playerHealth.Value = 100;
            playerHealth.Step = 10;
            location.X = 125;
            location.Y = 470;
            playerHealth.Location = location;

            this.Controls.Add(playerHealth);
            this.Controls.Add(player);
            this.Controls.Add(score);


        }
        private void createBullets()
        {
            bullets = new PictureBox();
            Image img = CarGame.Properties.Resources.myBullet;
            bullets.Image = img;
            bullets.Size = new Size(50, 50);
            bullets.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = player.Left + (player.Width / 2) - 25;
            location.Y = player.Top - 20;
            bullets.Location = location;
            bullets.BackColor = Color.Transparent;
            playerBulletList.Add(bullets);
            this.Controls.Add(bullets);
        }
        private void createEnemyBullets()
        {
            enemyBulletLoop++;
            if (enemyBulletLoop >= 150)
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
        private void movePlayerBullet()
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

                enemy.Top = enemy.Top + 15;
            }

        }
        private void movePiles()
        {
            foreach (PictureBox pile in pilesList)
            {
                pile.Top = pile.Top + 10;
            }


        }
        private void createBigEnemy()
        {
            big++;
            if (big >= 350)
            {
                createEnemy();
                big = 0;
            }
        }
        private void createPile()
        {
            Loop++;
            if (Loop == 700)
            {
                createPiles();
                Loop = 0;
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
      private void playerCollisionWithBigEnemy()
        {
            if (BigEnemyList.Count >= 0)
            {
                for (int y = 0; y < BigEnemyList.Count; y++)
                {
                    if (BigEnemyList[y].Bounds.IntersectsWith(player.Bounds))
                    {
                        if (playerHealth.Value - 40  <=0)
                        {
                            playerHealth.Value = 0;

                        }
                        else
                        {
                            playerHealth.Value = playerHealth.Value - 40;

                        }
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
        private void BigenemyDetection()
        {
            for (int x = 0; x < BigEnemyList.Count; x++)
            {
                for (int y = 0; y < playerBulletList.Count; y++)
                {
                    if (BigEnemyList[x].Bounds.IntersectsWith(playerBulletList[y].Bounds))
                    {
                        if (enemyHealthList[x].Value - 25   <= 0)
                        {
                            enemyHealthList[x].Value = 0;
                            this.Controls.Remove(playerBulletList[y]);
                            playerBulletList.Remove(playerBulletList[y]);
                        }
                        else
                        {
                            enemyHealthList[x].Value = enemyHealthList[x].Value  - 25;
                            this.Controls.Remove(playerBulletList[y]);
                            playerBulletList.Remove(playerBulletList[y]);
                        }
                       
                    }
                }
            }
        }
        private void checkBigEnemy()
        {
            if(enemyHealthList.Count >0)
            {
                for (int x = 0; x < BigEnemyList.Count; x++)
                {
                    if (enemyHealthList[x].Value <= 0)
                    {
                        this.Controls.Remove(enemyHealthList[x]);
                        this.Controls.Remove(BigEnemyList[x]);
                        enemyHealthList.Remove(enemyHealthList[x]);
                        BigEnemyList.Remove(BigEnemyList[x]);
                        scor = scor + 10;

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
                        if (playerHealth.Value - 10 <0)
                        {
                            playerHealth.Value =0;
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
        private void playerCollisionWithEnemy()
        {
            for (int y = 0; y < enemyList.Count; y++)
            {
                if (enemyList[y].Bounds.IntersectsWith(player.Bounds))
                {
                    if (playerHealth.Value >= 10)
                    {
                        playerHealth.Value = playerHealth.Value - 10;
                        this.Controls.Remove(enemyList[y]);
                        enemyList.Remove(enemyList[y]);
                    }
                }
            }
        }
        private void enemyGameLoop_Tick(object sender, EventArgs e)
        {
            int x = rand.Next(1, 3);
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

        private void scoreChange(ref int score)
        {
            score = score + 1;
        }
        private void createEnemy()
        {

            enemy = new PictureBox();
            enemyHealth = new ProgressBar();
           
            enemyHealth.Size = new Size(20,10);
            enemy.Size = new Size(60, 60);
            enemy.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            countt++;

            if (countt == 0 || countt ==1)
            {
                Image img = CarGame.Properties.Resources.Hull_01;
                enemy.Image = img;
                location.X = 15;
                location.Y = 70;
                enemy.Location = location;
                location.X = 36;
                location.Y = 76;
                enemyHealth.Location = location;
            }
            else if (countt == 2 || countt == 3)
            {
                Image img = CarGame.Properties.Resources.Hull_02;
                enemy.Image = img;
                location.X = 100;
                location.Y = 70;
                enemy.Location = location;
                location.X = 124;
                location.Y = 76;
                enemyHealth.Location = location;
            }
            else if (countt == 4 || countt == 5)
            {
                Image img = CarGame.Properties.Resources.Hull_03;
                enemy.Image = img;
                location.X = 187;
                location.Y = 70;
                enemy.Location = location;
                location.X = 200;
                location.Y = 76;
                enemyHealth.Location = location;
                countt = 0;
            }
            enemy.BackColor = Color.Transparent;
            enemyHealth.Top = enemy.Top -2 ;
            enemyHealth.Value = 100;
            enemyHealth.Step = 10;
            this.Controls.Add(enemyHealth);
            BigEnemyList.Add(enemy);
            this.Controls.Add(enemy);
            enemyHealthList.Add(enemyHealth);

        }

        private void enemyCarSpeedLoop_Tick(object sender, EventArgs e)
        {
            moveEnemys();
            if (bhbEnemyStatus == "alive")
            {
                moveBhbEnemy();
            }
        }

        private void createEnemy1()
        {
            enemy1 = new PictureBox();
            Image img = CarGame.Properties.Resources.myEnemy1;
            enemy1.Image = img;
            enemy1.Size = new Size(50, 60);
            enemy1.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 20;
            location.Y = -5;
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
            enemy2.Size = new Size(50, 60);
            enemy2.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 107;
            location.Y = -5;
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
            enemy3.Size = new Size(50, 60);
            enemy3.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = 187;
            location.Y = -5;
            enemy3.Location = location;
            enemy3.BackColor = Color.Transparent;

            enemyList.Add(enemy3);
            this.Controls.Add(enemy3);



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
            else if (count == 2 || count == 3)
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
        private void createEnemyBullet(PictureBox enemy)
        {
            enemyBullets = new PictureBox();
            Image img = CarGame.Properties.Resources.enemyFire;
            enemyBullets.Image = img;
            enemyBullets.Size = new Size(50, 50);
            enemyBullets.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = enemy.Left + (enemy.Width / 2) - 20;
            location.Y = enemy.Top;
            enemyBullets.Location = location;
            enemyBullets.BackColor = Color.Transparent;
            enemyBulletList.Add(enemyBullets);
            this.Controls.Add(enemyBullets);
        }
        private void createBigEnemyBullet(PictureBox enemy)
        {
            enemyBullets = new PictureBox();
            Image img = CarGame.Properties.Resources.enemyFire;
            enemyBullets.Image = img;
            enemyBullets.Size = new Size(50, 50);
            enemyBullets.SizeMode = PictureBoxSizeMode.Zoom;
            System.Drawing.Point location = new System.Drawing.Point();
            location.X = enemy.Left + (enemy.Width / 2) - 20;
            location.Y = enemy.Top;
            enemyBullets.Location = location;
            enemyBullets.BackColor = Color.Transparent;
            BigEnemyBulletList.Add(enemyBullets);
            this.Controls.Add(enemyBullets);
        }

        private void createBhbEnemyBulllets()
        {
            System.Drawing.Point location = new System.Drawing.Point();
            Image img = CarGame.Properties.Resources.bhbBullet;

            bhbBullet1 = new PictureBox();
            bhbBullet1Status = "alive";
            bhbBullet1.Image = img;
            bhbBullet1.Size = new Size(20, 20);
            bhbBullet1.BackColor = Color.Transparent;
            bhbBullet1.SizeMode = PictureBoxSizeMode.Zoom;
            location.X = bhbEnemy.Left + (bhbEnemy.Width / 2) - 20;
            location.Y = bhbEnemy.Top;
            bhbBullet1.Location = location;
            this.Controls.Add(bhbBullet1);

            bhbBullet2 = new PictureBox();
            bhbBullet2Status = "alive"; 
            bhbBullet2.Image = img;
            bhbBullet2.Size = new Size(20, 20);
            bhbBullet2.BackColor = Color.Transparent;
            bhbBullet2.SizeMode = PictureBoxSizeMode.Zoom;
            location.X = bhbEnemy.Left + (bhbEnemy.Width / 2) - 20;
            location.Y = bhbEnemy.Top;
            bhbBullet2.Location = location;
            this.Controls.Add(bhbBullet2);

            bhbBullet3 = new PictureBox();
            bhbBullet3Status = "alive";
            bhbBullet3.Image = img;
            bhbBullet3.Size = new Size(20, 20);
            bhbBullet3.BackColor = Color.Transparent;
            bhbBullet3.SizeMode = PictureBoxSizeMode.Zoom;
            location.X = bhbEnemy.Left + (bhbEnemy.Width / 2) - 20;
            location.Y = bhbEnemy.Top;
            bhbBullet3.Location = location;
            this.Controls.Add(bhbBullet3);
        }
        private void removeBHBenemy()
        {
            if (bhbEnemyStatus == "alive")
            {
                    if (bhbEnemy.Top > this.Height)
                    {
                        this.Controls.Remove(bhbEnemy);
                         this.Controls.Remove(bhbEnemyHealth);
                        bhbEnemyStatus = "dead"; 
                    }
            }
        }
        private void moveBhbEnemy()
        {
            bhbEnemy.Top = bhbEnemy.Top + 5;
            bhbEnemyHealth.Top = bhbEnemyHealth.Top + 5;
        }
        private void creteThrrefireEnemy()
        {
            System.Drawing.Point location = new System.Drawing.Point();
            bhbEnemy = new PictureBox();
            bhbEnemyHealth = new ProgressBar();
            bhbEnemyHealth.Value = 100;
            Image img = CarGame.Properties.Resources.hell;
            bhbEnemy.Image = img;
            bhbEnemy.Size = new Size(90, 90);
            bhbEnemy.SizeMode = PictureBoxSizeMode.Zoom;
            bhbEnemyStatus = "alive";
            location.X = 100;
            location.Y = 1;
            bhbEnemy.Location = location;
            bhbEnemy.BackColor = Color.Transparent;
            bhbEnemyHealth.Size = new Size(20, 10);
            location.X = 115;
            location.Y = -2;
            bhbEnemyHealth.Location = location;
            this.Controls.Add(bhbEnemy);
            this.Controls.Add(bhbEnemyHealth);
        }
        private void removebhbBullets()
        {
            if (bhbBullet2Status == "alive")
            {
                if (bhbBullet2.Top > this.Height)
                {
                    this.Controls.Remove(bhbBullet2);
                    bhbBullet2Status = "dead";
                }
            }
            if (bhbBullet1Status == "alive")
            {
                if (bhbBullet1.Left < 0  || bhbBullet1.Top > this.Height )
                {
                    this.Controls.Remove(bhbBullet1);
                    bhbBullet1Status = "dead";
                }
            }
            if (bhbBullet3Status == "alive")
            {
                if(bhbBullet3.Top   > this.Height || bhbBullet3.Left > this.Width + bhbBullet1.Width)
                {
                    this.Controls.Remove(bhbBullet3);
                    bhbBullet3Status = "dead";
                }
            }
        }
        private void moveBhbEnemyBullets()
        {
            if (bhbBullet1Status =="alive" || bhbBullet2Status == "alive" || bhbBullet3Status == "alive")
            {

                bhbBullet2.Top = bhbBullet2.Top + 5;
                bhbBullet1.Top = bhbBullet1.Top + 4;
                bhbBullet1.Left = bhbBullet1.Left - 2;
                bhbBullet3.Top = bhbBullet3.Top + 4;
                bhbBullet3.Left = bhbBullet3.Left + 2;
            }
        }
        private void detectBhbEnemyWithPlayer()
        {
            if (bhbEnemyStatus == "alive")
            {
                if (bhbEnemy.Bounds.IntersectsWith(player.Bounds))
                {
                    this.Controls.Remove(bhbEnemy);
                    this.Controls.Remove(bhbEnemyHealth);
                    bhbEnemyStatus = "dead";

                    if (playerHealth.Value - 20 < 0 )
                    {
                        playerHealth.Value = 0;
                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 20;
                    }
                   
                }
            }
        }

        private void playerBulletCollisionWithBhbEnemy()
        {
            if(bhbEnemyStatus == "alive")
            {
                for(int x  = 0; x < playerBulletList.Count; x++)
                {
                    if (bhbEnemy.Bounds.IntersectsWith(playerBulletList[x].Bounds))
                    {
                        this.Controls.Remove(playerBulletList[x]);
                        playerBulletList.Remove(playerBulletList[x]);
                        if (bhbEnemyHealth.Value - 10 <0)
                        {
                            bhbEnemyHealth.Value = 0;
                        }
                        else
                        {
                            bhbEnemyHealth.Value = bhbEnemyHealth.Value - 10;
                        }
                    }
                }
            }
        }
        private void checkBhbEnemy()
        {
            if (bhbEnemyStatus == "alive")
            {
                if (bhbEnemyHealth.Value <=0)
                {
                    this.Controls.Remove(bhbEnemy);
                    this.Controls.Remove(bhbEnemyHealth);
                    bhbEnemyStatus = "dead";
                }
            }
        }
        private void playerCollisionWithBhbBullets()
        {
            if (bhbBullet1Status == "alive")
            {
                if (player.Bounds.IntersectsWith(bhbBullet1.Bounds))
                {
                    this.Controls.Remove(bhbBullet1);
                    bhbBullet1Status = "dead";
                    if (playerHealth.Value - 10 < 0)
                    {
                          playerHealth.Value = 0;
                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 10;
                    }
                  

                }
            }
            if (bhbBullet2Status == "alive")
            { 
                if ( player.Bounds.IntersectsWith(bhbBullet2.Bounds))
                {
                    this.Controls.Remove(bhbBullet2);
                    bhbBullet2Status = "dead";
                    if (playerHealth.Value - 10 < 0)
                    {
                        playerHealth.Value = 0;

                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 10;

                    }
                  


                }
            }
            if (bhbBullet3Status == "alive")
            {

                if (player.Bounds.IntersectsWith(bhbBullet3.Bounds))
                {
                    this.Controls.Remove(bhbBullet3);
                    bhbBullet3Status = "dead";
                    if (playerHealth.Value - 10 < 0)
                    {
                         playerHealth.Value = 0;
                    }
                    else
                    {
                        playerHealth.Value = playerHealth.Value - 10;
                    }
                 

                }
            }
            
        }
    }
}
