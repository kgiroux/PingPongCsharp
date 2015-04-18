﻿using System;
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

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        public Partie(int joueur, Form1 form)
        {
            this.joueur = joueur;
            this.form = form;
            InitializeComponent();
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
            if(e.KeyCode == Keys.Space && b.Vitesse == 0 && ball.Visible == true)
                b.Lance();
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
            
            if(ball.Visible == true)  
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
                            if(b.Angle < 300 && b.Angle > 180)
                                b.Angle = 300;

                            b.Vitesse++;
                        }

                        if (b.X > this.ClientSize.Width)
                        {
                            ball.Visible = false;
                        
                            //Envoi des données
                            ServerClass.prepareSendData(b);

                            b.Vitesse = 0;
                        }
                        else if (b.X < 0)
                        {
                            b.EnDehors = true;
                            b.Vitesse = 0;
                            ball.Visible = false;
                            ServerClass.prepareSendData(b);
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
                            if (b.Angle < 120 && b.Angle > 0)
                                b.Angle = 120;
                        }

                        if (b.X < 0)
                        {
                            ball.Visible = false;

                            //Envoi des données
                            ClientClass.prepareSendData(b);

                            b.Vitesse = 0;
                        }
                        else if (b.X > this.ClientSize.Width)
                        {
                            b.EnDehors = true;
                            b.Vitesse = 0;
                            ball.Visible = false;
                            ClientClass.prepareSendData(b);
                            b.EnDehors = false;
                        }
                    }

                    b.Delta();

                    try
                    {
                        ball.Location = new Point(b.X, b.Y);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                
                }
                else
                {
                    if (b.EnDehors == true)
                    {
                        b = new Balle(this.ClientSize.Width / 2 - ball.Width / 2, this.ClientSize.Height / 2 - ball.Height / 2);
                    }
                }
            }
            else
            {
                if(joueur == 0)
                {
                    if(ServerClass.b != null)
                    {
                        b.X = this.ClientSize.Width;
                        b.Y = ServerClass.b.Y;
                        b.Vitesse = ServerClass.b.Vitesse;
                        b.Angle = ServerClass.b.Angle;
                        b.Delta();
                        ball.Visible = true;
                        ServerClass.b = null;
                    }
                }
                else
                    if (ClientClass.b != null)
                    {
                        b.X = 0;
                        b.Y = ClientClass.b.Y;
                        b.Vitesse = ClientClass.b.Vitesse;
                        b.Angle = ClientClass.b.Angle;
                        b.Delta();
                        ball.Visible = true;
                        ClientClass.b = null;
                    }
            }
        }
        public void updateOutputLog(String text, int type)
        {
            this.form.updateConsoleLog(text, type);
        }

        private void Form1_Resize(Object sender, EventArgs e)
        {
            if (joueur == 1)
                raquette.Location = new Point(this.ClientSize.Width - 46, this.ClientSize.Height / 2 - raquette.Height / 2);
        }

        private void Partie_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.updateOutputLog("Fermeture de la partie en cours", 0);
            if (joueur == 1)
            {
                this.form.changeScanButtonActivate(true);
                this.form.changeClientButtonActivate(false);
                ClientClass.CloseConnection();
            }
            else
            {
                this.form.changeServerButtonActivate(true);
                ServerClass.CloseConnection();
            }
        }
    }
}
