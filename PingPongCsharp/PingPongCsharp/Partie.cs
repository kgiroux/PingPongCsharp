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

            int i = 0;
            
            while (i < 15)
            {
                if (e.KeyCode == Keys.Z && y > 0) y -= 1;
                else if (e.KeyCode == Keys.S && y + raquette.Height < this.ClientSize.Height) y += 1;
                i++;
            }

            raquette.Location = new Point(x, y);
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
                int bx = ball.Location.X;
                int by = ball.Location.Y;
                int bh = ball.Height;
                int bw = ball.Width;
                int rx = raquette.Location.X;
                int ry = raquette.Location.Y;
                int rh = raquette.Height;
                int rw = raquette.Width;

                int[] tab = new int[2];

                if (by < 0 || by + bh > this.ClientSize.Height)
                    b.Angle = 360 - b.Angle;
                if (bx > this.ClientSize.Width)
                    if (b.Angle > 180)
                        b.Angle = 540 - b.Angle;
                    else
                        b.Angle = 180 - b.Angle;

                if(bx < rx + rw && bx > rx && by < ry + rh && by + bh > ry)
                    if(b.Angle > 180)
                        b.Angle = 540 - b.Angle;
                    else
                        b.Angle = 180 - b.Angle;
                
                tab = b.Delta();

                Console.WriteLine(b.Angle);
                Console.WriteLine(tab[0] + "    " + tab[1]);

                bx += tab[0];
                by += tab[1];

                ball.Location = new Point(bx, by);

                t = 0;
            }

            this.Invalidate();
        }
    }
}
