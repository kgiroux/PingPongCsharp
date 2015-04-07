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
    public partial class Partie : Form
    {
        public Partie()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) 
            {
                raquette.Location = new Point(raquette.Left, raquette.Top --);
            } 
        }

        private void Partie_Load(object sender, EventArgs e)
        {

        }
    }
}
