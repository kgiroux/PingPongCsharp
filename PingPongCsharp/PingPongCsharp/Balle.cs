using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCsharp
{
    [Serializable]
    public class Balle
    {
        private int x;
        private int y;
        private int angle;
        private int vitesse;
        private bool enDehors;
        public int Angle { get {return angle;} set{angle = value;} }
        public int Vitesse { get {return vitesse;} set{vitesse = value;} }
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public bool EnDehors { get { return enDehors; } set { enDehors = value; } }

        public Balle(int x, int y)
        {
            this.x = x;
            this.y = y;
            vitesse = 0;
            angle = 0;
            enDehors = false;
        }
        public void Lance()
        {
            Random r = new Random();
            vitesse = 7;
            do
                angle = r.Next(0, 360);
            while(angle > 45 && angle < 135 || angle > 225 && angle < 315);
        }

        public void Delta()
        {
            int[] p = new int[2];
            x += (int)(Math.Cos(angle * Math.PI / 180) * vitesse);
            y += (int)(Math.Sin(angle * Math.PI / 180) * vitesse);

            Console.WriteLine(Math.Cos(angle) + "     " + Math.Sin(angle));
        }
    }
}
