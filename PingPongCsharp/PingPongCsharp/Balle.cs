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

        public Balle()
        {
            vitesse = 0;
            angle = 0;
        }
        public void Lance()
        {
            Random r = new Random();
            vitesse = 20;
            angle = r.Next(0, 360);
        }

        public int[] Position()
        {
            int[] p = new int[2];
            p[0] = (int)Math.Cos(angle) * vitesse;

            return p;
        }
    }
}
