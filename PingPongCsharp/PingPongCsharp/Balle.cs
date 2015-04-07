using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCsharp
{
    class Balle
    {
        private double angle;
        private int vitesse;

        public Balle()
        {
            vitesse = 0;
            angle = 0;
        }
        public void Lance()
        {
            vitesse = 20;

        }
    }
}
