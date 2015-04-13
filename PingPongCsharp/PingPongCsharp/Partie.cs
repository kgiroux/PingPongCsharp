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
        private static Balle b;
        private static int t = 0;

        public Partie()
        {
            InitializeComponent();

            KeyDown += new KeyEventHandler(Form1_KeyDown);

            b = new Balle();

            b.Lance();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int x = raquette.Location.X;
            int y = raquette.Location.Y;

            int x2 = raquette2.Location.X;
            int y2 = raquette2.Location.Y;

            int i =0;

            Console.WriteLine(this.Height);
            Console.WriteLine(raquette2.Location.Y + raquette2.Height);
            
            while (i < 15)
            {
                if (e.KeyCode == Keys.Up && raquette2.Location.Y > 0) y2 -= 1;
                else if (e.KeyCode == Keys.Down && raquette2.Location.Y + raquette2.Height < this.Height) y2 += 1;

                if (e.KeyCode == Keys.Z && raquette.Location.Y > 0) y -= 1;
                else if (e.KeyCode == Keys.S && raquette.Location.Y + raquette.Height < this.Height) y += 1;
                i++;

                raquette.Location = new Point(x, y);
                raquette2.Location = new Point(x2, y2);
            }
        }

        private void Partie_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (t < b.Vitesse)
            {
                t++;
            }
            else
            {
                int x = ball.Location.X;
                int y = ball.Location.Y;

                if (y < 0)
                    b.Angle += 2 * b.Angle;
                if (y > this.Height)
                    b.Angle -= 2 * b.Angle;

                int[] tab = new int[2];
                tab = b.Delta();

                x += tab[0];
                y += tab[1];

                ball.Location = new Point(x, y);

                t = 0;
            }
        }
    }
}
