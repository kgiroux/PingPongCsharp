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

            new SingletonDb();
            InitializeComponent();
            var query = from score in SingletonDb.Instance.Scores where score.Id == 1 select new { score.NomServeur, score.ScoreServeur, score.ScoreClient, score.NomClient };
            Console.WriteLine(query.ToList());
        }

        private void Start_Click(object sender, EventArgs e)
        {
            ConfigurationPanel cp = new ConfigurationPanel(this);
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
