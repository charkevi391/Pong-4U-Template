/*
 * Description:     A basic PONG simulator
 * Author:           
 * Date:            
 */

#region libraries

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

#endregion

namespace Pong
{
    public partial class Form1 : Form
    {
        #region global values
        //Rectantgle hazards
        Class1 rectangle1;
        Class1 rectangle2;

        //graphics objects for drawing
        SolidBrush drawBrush = new SolidBrush(Color.White);
        Font drawFont = new Font("Courier New", 10);

        // Sounds for game
        SoundPlayer scoreSound = new SoundPlayer(Properties.Resources.score);
        SoundPlayer collisionSound = new SoundPlayer(Properties.Resources.collision);

        //determines whether a key is being pressed or not
        Boolean aKeyDown, zKeyDown;

        // check to see if a new game can be started
        Boolean newGameOk = true;

        //ball directions, speed, and rectangle
        Boolean ballMoveRight = false;
        Boolean ballMoveDown = true;
        int BALL_SPEED = 4;
        Rectangle ball;

        //Rectangle Hazard values
        int rec1X = 400;
        int rec1Y = 100;
        int rec1SizeX = 60;
        int rec1SizeY = 20;

        int rec2X = 400;
        int rec2Y = 500;
        int rec2SizeX = 60;
        int rec2SizeY = 20;

        //paddle speeds and rectangles
        const int PADDLE_SPEED = 4;
        const int PADDLE_EDGE = 20;
        Class1 p1 = new Class1();

        //player and game scores
        int player1Score = 0;
        int level2Score = 2;
        int level3Score = 5;
        int winScore = 9;

        List<Class1> hazardList = new List<Class1>();
        #endregion

        public Form1()
        {
            InitializeComponent();
            onSTart();
        }
        public void onSTart()
        {
            rectangle1 = new Class1(drawBrush, 450, 0, 30, 150);
            rectangle2 = new Class1(drawBrush, 450, 350, 30, 150);

            p1.sizeX = 10;    //height for both paddles set the same
            p1.sizeY = 60;  //width for both paddles set the same

            //p1 starting position
            p1.x = PADDLE_EDGE;
            p1.y = this.Height / 2 - p1.sizeY / 2;

        }


        // -- YOU DO NOT NEED TO MAKE CHANGES TO THIS METHOD
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //check to see if a key is pressed and set is KeyDown value to true if it has
            switch (e.KeyCode)
            {
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.Z:
                    zKeyDown = true;
                    break;
                case Keys.Space:
                    if (newGameOk)
                    {
                        SetParameters();
                    }
                    break;
                case Keys.N:
                    if (newGameOk)
                    {
                        Close();
                    }
                    break;
            }
        }

        // -- YOU DO NOT NEED TO MAKE CHANGES TO THIS METHOD
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //check to see if a key has been released and set its KeyDown value to false if it has
            switch (e.KeyCode)
            {
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.Z:
                    zKeyDown = false;
                    break;
            }
        }

        /// <summary>
        /// sets the ball and paddle positions for game start
        /// </summary>
        private void SetParameters()
        {
            if (newGameOk)
            {
                player1Score = 0;
                newGameOk = false;
                startLabel.Visible = false;
                gameUpdateLoop.Start();
            }

            //set starting position for paddles on new game and point scored 
              // buffer distance between screen edge and paddle            

            p1.sizeX = 10;    //height for both paddles set the same
            p1.sizeY = 60;  //width for both paddles set the same

            //p1 starting position
            p1.x = PADDLE_EDGE;
            p1.y = this.Height / 2 - p1.sizeY / 2;


            ball.Width = 12;
            ball.Height = 12;
            ball.X = this.Width / 2;
            ball.Y = this.Height / 2;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is the game engine loop that updates the position of all elements
        /// and checks for collisions.
        /// </summary>
        public void gameUpdateLoop_Tick(object sender, EventArgs e)
        {
            scoreLabel.Text = "";


            #region update ball position

            // TODO create code to move ball either left or right based on ballMoveRight and using BALL_SPEED

            if (ballMoveRight == true)
            {
                ball.X = ball.X + BALL_SPEED;
            }
            else if (ballMoveRight == false)
            {
                ball.X = ball.X - BALL_SPEED;
            }
            // TODO create code move ball either down or up based on ballMoveDown and using BALL_SPEED

            if (ballMoveDown == true)
            {
                ball.Y = ball.Y + BALL_SPEED;
            }
            else if (ballMoveDown == false)
            {
                ball.Y = ball.Y - BALL_SPEED;
            }
            #endregion

            #region update paddle positions

            if (aKeyDown == true && p1.y > 0)
            {
                p1.y = p1.y - PADDLE_SPEED;
            }

            if (zKeyDown == true && p1.y < this.Height - p1.sizeY)
            {
                p1.y = p1.y + PADDLE_SPEED;
            }

            #endregion

            #region ball collision with top and bottom lines

            if (ball.Y < 0) // if ball hits top line
            {
                ballMoveDown = true;
            }
            else if (ball.Y > this.Height - ball.Width)
            {
                ballMoveDown = false;
            }
            // TODO In an else if statement use ball.Y, this.Height, and ball.Width to check for collision with bottom line
            // If true use ballMoveDown down boolean to change direction

            #endregion

            #region ball collision with paddles

            if (p1.Collides(ball))
            {
                ballMoveRight = true;
                collisionSound.Play();
                BALL_SPEED++;
            }

            if (topleftLabel.Text == "2" || topleftLabel.Text == "3" || topleftLabel.Text == "4")
            {
                if (rectangle1.Collides(ball))
                {
                    ballMoveRight = !ballMoveRight;
                }
            }

            if (topleftLabel.Text == "5" || topleftLabel.Text == "6" || topleftLabel.Text == "7" || topleftLabel.Text == "8")
            {
                if (rectangle1.Collides(ball))
                {
                    ballMoveRight = !ballMoveRight;
                }

                if (rectangle2.Collides(ball))
                {
                    ballMoveRight = !ballMoveRight;
                }
            }




            /*  ENRICHMENT
             *  Instead of using two if statments as noted above see if you can create one
             *  if statement with multiple conditions to play a sound and change direction
             */

            #endregion

            #region ball collision with side walls (point scored)

            if (ball.X < 0)  // ball hits left wall logic
            {
                GameOver("1");
                this.Refresh();
                Thread.Sleep(2000);
                scoreLabel.Visible = false;
            }

            if (ball.X >= this.Width)
            {
                ballMoveRight = false;
                scoreSound.Play();
                player1Score++;
                topleftLabel.Text = Convert.ToString(player1Score);


                if (player1Score == level2Score)
                {
                    LevelUp("2");
                }

                else if (player1Score == level3Score)
                {
                    LevelUp("3");

                }
                else if (player1Score == winScore)
                {
                    LevelUp("4");
                }
            }
            #endregion

            //refresh the screen, which causes the Form1_Paint method to run
            this.Refresh();
        }

        private void GameOver(string lose)
        {
            gameUpdateLoop.Stop();

            if (lose == "1")
            {
                scoreLabel.Visible = true;
                scoreLabel.Text = "You Lose Stupid!";
            }

            this.Refresh();
            Thread.Sleep(2000);

            newGameOk = true;
            this.Refresh();

        }

        private void LevelUp(string level)
        {
            gameUpdateLoop.Stop();

            if (level == "2")
            {
                scoreLabel.Visible = true;
                scoreLabel.Text = "Level 2";
                Thread.Sleep(2000);
                hazardList.Add(rectangle1);


            }

            else if (level == "3")
            {
                scoreLabel.Visible = true;
                scoreLabel.Text = "Level 3";
                Thread.Sleep(2000);
                hazardList.Add(rectangle2);
            }
            else if (level == "4")
            {
                scoreLabel.Visible = true;
                scoreLabel.Text = "You Win Congrats!";
                Thread.Sleep(2000);
            }

            this.Refresh();
            Thread.Sleep(2000);
            gameUpdateLoop.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(drawBrush, p1.x, p1.y, p1.sizeX, p1.sizeY);
            e.Graphics.FillRectangle(drawBrush, ball);

            foreach (Class1 a in hazardList)
            {
                e.Graphics.FillRectangle(drawBrush, a.x, a.y, a.sizeX, a.sizeY);
            }

        }

    }
}
