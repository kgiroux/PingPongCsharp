using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPongCsharp
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /* Initialisation du programme */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /* Lancement de la première fenetre */
            Application.Run(new Welcome());
        }
    }
}
