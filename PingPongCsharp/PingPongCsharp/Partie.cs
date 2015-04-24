using System;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPongCsharp
{
    public partial class Partie : Form
    {
        private int joueur;
        private Form1 form;
        private Balle b;
        private DataTransit dt;
        private int scoreServer;
        private int scoreClient;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        public Partie(int joueur, Form1 form)
        {
            scoreServer = 0;
            scoreClient = 0;
            this.joueur = joueur;
            this.form = form;
            InitializeComponent();
            temps.Location = new Point(this.ClientSize.Width / 2 - temps.Width / 2, temps.Location.Y);
            score.Location = new Point(this.ClientSize.Width / 2 - score.Width / 2, score.Location.Y);
            raquette.Location = new Point(raquette.Location.X, this.ClientSize.Height / 2 - raquette.Height / 2);
            ball.Location = new Point(this.ClientSize.Width / 2 - ball.Width / 2, this.ClientSize.Height / 2 - ball.Height / 2);
            b = new Balle(ball.Location.X, ball.Location.Y);
            this.Resize += new EventHandler(Form1_Resize);

            if (joueur == 1)
            {
                ball.Visible = false;
                raquette.Location = new Point(this.ClientSize.Width - 46, this.ClientSize.Height / 2 - raquette.Height / 2);
                Image img = raquette.Image;
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                raquette.Image = img;
                Image img_terrain = this.BackgroundImage;
                img_terrain.RotateFlip(RotateFlipType.Rotate180FlipNone);
                this.BackgroundImage = img_terrain;
            }

            KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && b.Vitesse == 0 && ball.Visible == true)
            {
                b.Lance();
                timer2.Enabled = true;
                dt = new DataTransit();
                dt.Timer = true;

                if (joueur == 0)
                    ServerClass.prepareSendData(dt);
                else
                    ClientClass.prepareSendData(dt);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int bh = ball.Height;
            int bw = ball.Width;
            int rx = raquette.Location.X;
            int ry = raquette.Location.Y;
            int rh = raquette.Height;
            int rw = raquette.Width;

            int[] tab = new int[2];

            if (GetAsyncKeyState(Keys.Up) != 0 && ry > 0)
            {
                ry -= 10;
            }
            if (GetAsyncKeyState(Keys.Down) != 0 && ry + rh < this.ClientSize.Height)
            {
                ry += 10;
            }

            try
            {
                raquette.Location = new Point(rx, ry);
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if(ServerClass.dt.Alive == false || ClientClass.dt.Alive == false)
            {
                this.Partie_FormClosing(null, null);
            }
            
            if(ServerClass.dt.BallePro == null && ClientClass.dt.BallePro == null)  
            {
                if (b.Vitesse != 0) 
                {
                    if (b.Y < 0 || b.Y + bh > this.ClientSize.Height)
                        b.Angle = 360 - b.Angle;

                    if (joueur == 0)
                    {
                        if(b.X < rx + rw && b.X > rx && b.Y < ry + rh && b.Y + bh > ry)
                        {
                            if(b.Angle > 180)
                                b.Angle = 540 - b.Angle;
                            else
                                b.Angle = 180 - b.Angle;

                            if(b.Y + bh > ry && b.Y + bh/2 < ry + rh/2)
                                b.Angle -= (int)((double)((double)rh / 2 - ((b.Y + bh) - ry)) / ((double)rh / 2) * 60);
                            else
                                b.Angle += (int) ((double)(b.Y - (ry + rh / 2)) / ((double)rh / 2) * 60);

                            if(b.Angle > 60 && b.Angle < 180)
                                b.Angle = 60;
                            else if(b.Angle < 300 && b.Angle > 180)
                                b.Angle = 300;

                            b.Vitesse++;
                        }

                        if (b.X > this.ClientSize.Width)
                        {
                            ball.Visible = false;
                            b.Y = (double)ball.Location.Y / (double)this.ClientSize.Height;
                        
                            //Envoi des données
                            DataTransit dt = new DataTransit();
                            dt.BallePro = b;
                            dt.Alive = true;
                            ServerClass.prepareSendData(dt);

                            b.Vitesse = 0;
                        }
                        else if (b.X < 0)
                        {
                            ball.Visible = false;

                            scoreClient++;
                            score.Text = scoreServer + "-" + scoreClient;

                            b.EnDehors = true;
                            b.Vitesse = 0;
                            DataTransit dt = new DataTransit();
                            dt.BallePro = b;
                            dt.Alive = true;
                            ServerClass.prepareSendData(dt);
                            timer2.Enabled = false;
                            b.EnDehors = false;
                        }
                    }
                    else
                    {
                        if (b.X + bw < rx + rw && b.X + bw > rx && b.Y < ry + rh && b.Y + bh > ry)
                        {
                            if (b.Angle > 180)
                                b.Angle = 540 - b.Angle;
                            else
                                b.Angle = 180 - b.Angle;

                            if(b.Y + bh > ry && b.Y + bh/2 < ry + rh/2)
                                b.Angle += (int)((double)((double)rh / 2 - ((b.Y + bh) - ry)) / ((double)rh / 2) * 60);
                            else
                                b.Angle -= (int) ((double)(b.Y - (ry + rh / 2)) / ((double)rh / 2) * 60);

                            if (b.Angle > 240 && b.Angle < 360)
                                b.Angle = 240;
                            else if (b.Angle < 120 && b.Angle > 0)
                                b.Angle = 120;
                        }

                        if (b.X < 0)
                        {
                            ball.Visible = false;
                            b.Y = (double)ball.Location.Y / (double)this.ClientSize.Height;

                            //Envoi des données
                            dt = new DataTransit();
                            dt.BallePro = b;
                            dt.Alive = true;
                            ClientClass.prepareSendData(dt);

                            b.Vitesse = 0;
                        }
                        else if (b.X > this.ClientSize.Width)
                        {
                            ball.Visible = false;

                            scoreServer++;
                            score.Text = scoreServer + "-" + scoreClient;

                            b.EnDehors = true;
                            b.Vitesse = 0;
                            dt = new DataTransit();
                            dt.BallePro = b;
                            dt.Alive = true;
                            ClientClass.prepareSendData(dt);
                            timer2.Enabled = false;
                            b.EnDehors = false;
                        }
                    }

                    b.Delta();

                    try
                    {
                        ball.Location = new Point(b.X, (int)b.Y);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            { 
                ball.Visible = true;

                if (joueur == 0)
                {
                    b = ServerClass.dt.BallePro;
                    b.X = this.ClientSize.Width;
                    b.Y = b.Y * this.ClientSize.Height;
                    if (b.EnDehors == true)
                    {
                        timer2.Enabled = false;
                        scoreServer++;
                        score.Text = scoreServer + "-" + scoreClient;
                        ball.Location = new Point(this.ClientSize.Width / 2 - ball.Width / 2, this.ClientSize.Height / 2 - ball.Height / 2);
                        b = new Balle(ball.Location.X, ball.Location.Y);
                        b.EnDehors = false;
                    }
                    ServerClass.dt.BallePro = null;
                }
                else
                {
                    b = ClientClass.dt.BallePro;
                    b.X = 0;
                    b.Y = b.Y * this.ClientSize.Height;
                    if (b.EnDehors == true)
                    {
                        timer2.Enabled = false;
                        scoreClient++;
                        score.Text = scoreServer + "-" + scoreClient;
                        ball.Location = new Point(this.ClientSize.Width / 2 - ball.Width / 2, this.ClientSize.Height / 2 - ball.Height / 2);
                        b = new Balle(ball.Location.X, ball.Location.Y);
                        b.EnDehors = false;
                    }
                    ClientClass.dt.BallePro = null;
                }
            }

            if (ServerClass.dt.Timer == true || ClientClass.dt.Timer == true)
                timer2.Enabled = true;

        }
        public void updateOutputLog(String text, int type)
        {
            this.form.updateConsoleLog(text, type);
        }

        private void Form1_Resize(Object sender, EventArgs e)
        {
            if (joueur == 1)
                raquette.Location = new Point(this.ClientSize.Width - 46, this.ClientSize.Height / 2 - raquette.Height / 2);

            temps.Location = new Point(this.ClientSize.Width / 2 - temps.Width / 2, temps.Location.Y);
            score.Location = new Point(this.ClientSize.Width / 2 - score.Width / 2, score.Location.Y);
        }

        private void Partie_FormClosing(object sender, FormClosingEventArgs e)
        {
            dt = new DataTransit();
            dt.Alive = false;
            this.updateOutputLog("Fermeture de la partie en cours", 0);
            if (joueur == 1)
            {
                ClientClass.prepareSendData(dt);
                this.form.changeScanButtonActivate(true);
                ClientClass.CloseConnection();
                this.Dispose();
                
            }
            else
            {
                ServerClass.prepareSendData(dt);
                this.form.changeServerButtonActivate(true);
                ServerClass.CloseConnection();
                this.Dispose();
                this.form.InvokeClickServer();
            }
            this.form.ChangeVisibily(true);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (int.Parse(temps.Text) > 0)
            {
                temps.Text = (int.Parse(temps.Text) - 1).ToString();
                temps.Location = new Point(this.ClientSize.Width / 2 - temps.Width / 2, temps.Location.Y);
            }
            else
            {
                timer1.Enabled = false;
            }
        }
    }
}
