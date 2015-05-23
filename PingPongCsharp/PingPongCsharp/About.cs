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
        Welcome wlcm; 
        /// <summary>
        /// Constructeur de la fenetre pour les explications
        /// </summary>
        /// <param name="wlcm"></param>
        public About(Welcome wlcm)
        {
            this.wlcm = wlcm;
            this.wlcm.Hide();
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
            wlcm.Show();
            this.Dispose();
        }
    }
}
