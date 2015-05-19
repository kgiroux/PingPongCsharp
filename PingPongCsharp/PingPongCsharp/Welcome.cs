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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            ConfigurationPanel cp = new ConfigurationPanel();
            cp.Show();
            this.Hide();
        }

        private void Scores_Click(object sender, EventArgs e)
        {
            ScoreResult rs = new ScoreResult();
            rs.ShowDialog();
        }
    }
}
