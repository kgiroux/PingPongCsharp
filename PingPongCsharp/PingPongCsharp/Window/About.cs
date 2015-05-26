using System;
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
    public partial class About : Form
    {
        private Welcome wlcm; 
        /// <summary>
        /// Constructeur de la fenetre pour les explications
        /// </summary>
        /// <param name="wlcm"></param>
        public About(Welcome wlcm)
        {
            this.wlcm = wlcm;
            
            InitializeComponent();
            Image img = ClientBoxPicture.Image;
            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            ClientBoxPicture.Image = img;
        }
        /// <summary>
        /// Retour au menu;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackMenu_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.wlcm.Show();
            
        }
        /// <summary>
        /// Permet de fermer la fenêtre ainsi que tout les processus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.wlcm.Dispose();
            this.Dispose();
        }
    }
}
