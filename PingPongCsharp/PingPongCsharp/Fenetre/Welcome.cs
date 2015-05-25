using PingPongCsharp.Bdd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPongCsharp
{
    public partial class Welcome : Form
    {
        /// <summary>
        /// Constructeur de la fenetre + Initialisation de Entities Framework
        /// </summary>
        public Welcome()
        {
           InitializeComponent();
           var query = from score in SingletonDb.Instance.Scores where score.Id == 1 select new { score.NomServeur, score.ScoreServeur, score.ScoreClient, score.NomClient };
           Console.WriteLine(query.ToList());   
        }
        /// <summary>
        /// Permet d'afficher le menu de configuration bluetooth;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Click(object sender, EventArgs e)
        {
            ConfigurationPanel cp = new ConfigurationPanel(this);
            cp.Show();
            this.Hide();
        }
        /// <summary>
        /// Bouton d'acces au résultat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Scores_Click(object sender, EventArgs e)
        {
            Score score = SingletonDb.Instance.Scores.Create();
            score.NomClient = "hjklmù";
            score.NomServeur = "jgklmùkjhklm";
            score.ScoreClient = 42;
            score.ScoreServeur = 114;

            SingletonDb.Instance.Scores.Add(score);
            SingletonDb.Instance.SaveChanges();
            ScoreResult rs = new ScoreResult();
            
            rs.ShowDialog();
        }
        /// <summary>
        /// Permet de revenir au Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Click(object sender, EventArgs e)
        {
            About abt = new About(this);
            abt.ShowDialog();
        }

    }
}
