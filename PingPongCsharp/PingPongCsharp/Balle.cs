using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCsharp
{
    class Balle
    {
        private int angle;
        private int vitesse;
        public int Angle { get {return angle;} set{angle = value;} }
        public int Vitesse { get {return vitesse;} set{vitesse = value;} }

        public Balle()
        {
            vitesse = 0;
            angle = 0;
        }
        public void Lance()
        {
            Random r = new Random();
            vitesse = 10;
            do
                angle = r.Next(0, 360);
            while(angle > 45 && angle < 135 || angle > 225 && angle < 315);
        }

        public int[] Delta()
        {
            int[] p = new int[2];
            p[0] = (int)(Math.Cos(angle * Math.PI / 180) * vitesse);
            p[1] = (int)(Math.Sin(angle * Math.PI / 180) * vitesse);

            Console.WriteLine(Math.Cos(angle) + "     " + Math.Sin(angle));
            return p;
        }
    }
}
