using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PingPongCsharp;
using System.Data.Entity.Core.Objects;
///<author>
///Cyril LEFEBVRE & Kévin Giroux
///</author>
namespace PingPongCsharp
{
    public partial class ScoreResult : Form
    {
        private Welcome wlcm; 
        /// <summary>
        /// Permet d'afficher tout les scores
        /// </summary>
        /// <param name="wlcm"></param>
        public ScoreResult(Welcome wlcm)
        {
            InitializeComponent();
            using (ScoreResultEntities db = new ScoreResultEntities())
            {
                var query = from score in db.Scores select new { score.NomServeur, score.ScoreServeur, score.ScoreClient, score.NomClient };
                ResultDataGrid.DataSource = query.ToList();
                ResultDataGrid.Columns[0].HeaderText = "Nom Joueur 1";
                ResultDataGrid.Columns[1].HeaderText = "Score du Joueur 1";
                ResultDataGrid.Columns[2].HeaderText = "Score du Joueur 2";
                ResultDataGrid.Columns[3].HeaderText = "Nom du Joueur 2";
            }

            this.wlcm = wlcm;
            ResultDataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           
                
        }
        /// <summary>
        /// Permet de fermer tout les processus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScoreResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.wlcm.Dispose();
            this.Dispose();
        }
        /// <summary>
        /// Permet de revenir au menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackMenu_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.wlcm.Show();
        }
    }
}
