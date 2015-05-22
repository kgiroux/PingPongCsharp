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

namespace PingPongCsharp
{
    public partial class ScoreResult : Form
    {


        public ScoreResult(ScoreResultEntities pongResult)
        {
            InitializeComponent();

            var query = from score in pongResult.Scores select new { score.NomServeur, score.ScoreServeur, score.ScoreClient, score.NomClient };
            ResultDataGrid.DataSource = query.ToList();
            ResultDataGrid.Columns[0].HeaderText = "Nom Joueur 1";
            ResultDataGrid.Columns[1].HeaderText = "Score du Joueur 1";
            ResultDataGrid.Columns[2].HeaderText = "Score du Joueur 2";
            ResultDataGrid.Columns[3].HeaderText = "Nom du Joueur 2";
            
            ResultDataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           
                
        }
        public ScoreResult(ScoreResultEntities pongResult, String nameJoueurServeur, String nameJoueurClient, int ScoreJoueurServer, int ScoreJoueurClient)
        {
            Score score = new Score();
            score.NomClient = nameJoueurClient;
            score.NomServeur = nameJoueurServeur;
            score.ScoreClient = ScoreJoueurClient;
            score.ScoreServeur = ScoreJoueurServer;
            pongResult.Scores.Add(score);
            pongResult.SaveChanges();
        }

 
    }
}
