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
        private double y;
        private int angle;
        private int vitesse;
        private bool enDehors;
        public int Angle { get {return angle;} set{angle = value;} }
        public int Vitesse { get {return vitesse;} set{vitesse = value;} }
        public int X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public bool EnDehors { get { return enDehors; } set { enDehors = value; } }

        /// <summary>
        /// Constructeur de la balle
        /// </summary>
        /// <param name="x">Position en abscisse</param>
        /// <param name="y">Position en ordonnée</param>
        public Balle(int x, int y)
        {
            this.x = x;
            this.y = y;
            vitesse = 0;
            angle = 0;
            enDehors = false;
        }

        /// <summary>
        /// Lancement de la balle (attribution d'une vitesse fixe et d'un angle aléatoire)
        /// </summary>
        public void Lance()
        {
            Random r = new Random();
            vitesse = 7;
            do
                angle = r.Next(0, 360);
            while(angle > 45 && angle < 135 || angle > 225 && angle < 315);
        }

        /// <summary>
        /// Modification de la position de la balle selon la vitesse et l'angle
        /// </summary>
        public void Delta()
        {
            int[] p = new int[2];
            x += (int)(Math.Cos(angle * Math.PI / 180) * vitesse);
            y += (int)(Math.Sin(angle * Math.PI / 180) * vitesse);
        }

        /// <summary>
        /// Modification de la vitesse de la balle limitée à la largeur de la raquette : 21px
        /// </summary>
        public void Accelere()
        {
            if (this.vitesse < 20)
            {
                this.vitesse++;
            }
        }
    }
}
