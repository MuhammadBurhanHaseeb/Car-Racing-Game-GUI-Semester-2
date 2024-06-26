
namespace CarGame
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameLoop = new System.Windows.Forms.Timer(this.components);
            this.gameLoopEnemey = new System.Windows.Forms.Timer(this.components);
            this.enemyCarSpeedLoop = new System.Windows.Forms.Timer(this.components);
            this.hurdelsLoop = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // gameLoop
            // 
            this.gameLoop.Enabled = true;
            this.gameLoop.Interval = 30;
            this.gameLoop.Tick += new System.EventHandler(this.gameLoop_Tick);
            // 
            // gameLoopEnemey
            // 
            this.gameLoopEnemey.Enabled = true;
            this.gameLoopEnemey.Interval = 9000;
            this.gameLoopEnemey.Tick += new System.EventHandler(this.gameLoopEnemey_Tick);
            // 
            // enemyCarSpeedLoop
            // 
            this.enemyCarSpeedLoop.Enabled = true;
            this.enemyCarSpeedLoop.Interval = 800;
            this.enemyCarSpeedLoop.Tick += new System.EventHandler(this.enemyCarSpeedLoop_Tick);
            // 
            // hurdelsLoop
            // 
            this.hurdelsLoop.Enabled = true;
            this.hurdelsLoop.Interval = 1500;
            this.hurdelsLoop.Tick += new System.EventHandler(this.hurdelsLoop_Tick);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Coral;
            this.BackgroundImage = global::CarGame.Properties.Resources.WhatsApp_Image_2022_06_09_at_1_04_14_PM;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(421, 911);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Game";
            this.Text = "``````";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer gameLoop;
        private System.Windows.Forms.Timer gameLoopEnemey;
        private System.Windows.Forms.Timer enemyCarSpeedLoop;
        private System.Windows.Forms.Timer hurdelsLoop;
    }
}