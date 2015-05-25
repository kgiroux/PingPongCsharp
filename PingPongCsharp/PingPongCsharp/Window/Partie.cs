using System;
using System.Timers;
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
        private int joueur;
        private ConfigurationPanel form;
        private Balle b;
        private DataTransit dt;
        private int scoreServer;
        private int scoreClient;
        private String nameServer;
        private String nameClient;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        /// <summary>
        /// Constructeur de la fenêtre de la partie
        /// </summary>
        /// <param name="joueur">Identifiant permettant de différencier client et serveur</param>
        /// <param name="form">Fenêtre de configuration bluetooth active</param>
        /// <param name="nameJoueur">Nom du joueur</param>
        public Partie(int joueur, ConfigurationPanel form, String nameJoueur)
        {
            // Initialisation des variables
            scoreServer = 0;
            scoreClient = 0;
            this.joueur = joueur;
            this.form = form;
            
            // Initialisation des composant graphiques
            InitializeComponent();
            temps.Location = new Point(this.ClientSize.Width / 2 - temps.Width / 2, temps.Location.Y);
            score.Location = new Point(this.ClientSize.Width / 2 - score.Width / 2, score.Location.Y);
            ball.Location = new Point(this.ClientSize.Width / 2 - ball.Width / 2, this.ClientSize.Height / 2 - ball.Height / 2);
            
            // Création d'un objet Balle permettant de manipuler le composant graphique ball
            b = new Balle(ball.Location.X, ball.Location.Y);
            
            // Initialisation des évènements
            this.Resize += new EventHandler(Form1_Resize);
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            
            // Si le joueur est du côté client
            if (joueur == 1)
            {
                // On cache la balle et on change place la raquette
                ball.Visible = false;
                raquette.Location = new Point(this.ClientSize.Width - 46, this.ClientSize.Height / 2 - raquette.Height / 2);

                // On retourne les images affichées
                Image img = raquette.Image;
                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                raquette.Image = img;
                Image img_terrain = this.BackgroundImage;
                img_terrain.RotateFlip(RotateFlipType.Rotate180FlipNone);
                this.BackgroundImage = img_terrain;

                // On attribut le nom du joueur client
                nameClient = nameJoueur;
                nameServer = "";
            }
            else
            {
                // On place la raquette
                raquette.Location = new Point(raquette.Location.X, this.ClientSize.Height / 2 - raquette.Height / 2);

                // On attribut le nom du joueur serveur
                nameServer = nameJoueur;
                nameClient = "";
            }
        }
        
        /// <summary>
        /// Méthode déclenchée lorsqu'une touche du clavier est enfoncée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Si la partie est en cours
            if (timerMvmt.Enabled == true)
            {
                // Si la balle est de notre côté qu'elle est fixe et que l'on a appuyé sur espace
                if (e.KeyCode == Keys.Space && b.Vitesse == 0 && ball.Visible == true)
                {
                    // Lancement de la balle
                    b.Lance();

                    // Lancement du chronomètre
                    timerChrono.Enabled = true;

                    // Création d'un objet de transfert de données avec l'information que le chronomètre est relancé
                    dt = new DataTransit();
                    dt.Timer = true;

                    // Si nous sommes côté serveur
                    if (joueur == 0)
                    {
                        // Stockage du nom du joueur serveur dans l'objet de transfert de données et envois
                        dt.NameJoueur = nameServer;
                        ServerClass.prepareSendData(dt);
                    }
                    else
                    {
                        // Stockage du nom du joueur client dans l'objet de transfert de données et envois
                        dt.NameJoueur = nameClient;
                        ClientClass.prepareSendData(dt);
                    }
                }
            }
            else
            {
                // Si nous sommes côté serveur
                if (joueur == 0)
                {
                    // Selon la touche enfoncée
                    if (e.KeyCode == Keys.Space)
                    {
                        // Réinitialisation de la form et de ses variables
                        this.Reset();
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        // Fermeture de la partie
                        this.Partie_FormClosing(null, null);
                    }
                }
            }
        }

        /// <summary>
        /// Méthode s'exécutant à chaque tour du timer 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            int bh = ball.Height;
            int bw = ball.Width;
            int rx = raquette.Location.X;
            int ry = raquette.Location.Y;
            int rh = raquette.Height;
            int rw = raquette.Width;

            // Modification de la position de la raquette
            if (GetAsyncKeyState(Keys.Up) != 0 && ry > 0)
            {
                ry -= 10;
            }
            if (GetAsyncKeyState(Keys.Down) != 0 && ry + rh < this.ClientSize.Height)
            {
                ry += 10;
            }

            try
            {
                raquette.Location = new Point(rx, ry);
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Si la partie de l'adversaire est arrétée, on ferme la partie de ce côté
            if(ServerClass.dt.Alive == false || ClientClass.dt.Alive == false)
            {
                this.Partie_FormClosing(null, null);
            }
            
            // Si aucune information sur la balle n'est arrivée par bluetooth et que la balle est en mouvement
            if (ServerClass.dt.BallePro == null && ClientClass.dt.BallePro == null && b.Vitesse != 0)  
            {
                // Modification de l'angle de la balle celle-ci tape en haut ou en bas
                if (b.Y < 0 || b.Y + bh > this.ClientSize.Height)
                    b.Angle = 360 - b.Angle;

                // Si l'on est côté serveur
                if (joueur == 0)
                {
                    // Modification de l'angle de la balle si celle-ci tape la raquette
                    if(b.X < rx + rw && b.X > rx && b.Y < ry + rh && b.Y + bh > ry)
                    {
                        if(b.Angle > 180)
                            b.Angle = 540 - b.Angle;
                        else
                            b.Angle = 180 - b.Angle;

                        // Modification de l'angle selon l'endroit où la balle arrive sur la raquette
                        if(b.Y + bh > ry && b.Y + bh/2 < ry + rh/2)
                            b.Angle -= (int)((double)((double)rh / 2 - ((b.Y + bh) - ry)) / ((double)rh / 2) * 60);
                        else
                            b.Angle += (int) ((double)(b.Y - (ry + rh / 2)) / ((double)rh / 2) * 60);

                        if(b.Angle > 60 && b.Angle < 180)
                            b.Angle = 60;
                        else if(b.Angle < 300 && b.Angle > 180)
                            b.Angle = 300;

                        // Augmentation de la vitesse de la balle
                        b.Vitesse++;
                    }

                    // Si la balle sort du terrain vers le joueur adverse
                    if (b.X > this.ClientSize.Width)
                    {
                        // La balle devient invisible et sa position est modifée
                        ball.Visible = false;
                        b.Y = (double)ball.Location.Y / (double)this.ClientSize.Height;
                        
                        // Envoi des données vers le client
                        DataTransit dt = new DataTransit();
                        dt.BallePro = b;
                        dt.Alive = true;
                        ServerClass.prepareSendData(dt);

                        // La balle est stoppée
                        b.Vitesse = 0;
                    }
                    // Si le joueur ne parvient pas à toucher la balle
                    else if (b.X < 0)
                    {
                        // La balle est cachée
                        ball.Visible = false;

                        // Le score est modifié
                        scoreClient++;
                        score.Text = scoreServer + "-" + scoreClient;

                        // Les informations sont envoyées à l'adversaire
                        b.EnDehors = true;
                        b.Vitesse = 0;
                        DataTransit dt = new DataTransit();
                        dt.BallePro = b;
                        dt.Alive = true;
                        ServerClass.prepareSendData(dt);

                        // Le chrono s'arrête
                        timerChrono.Enabled = false;

                        b.EnDehors = false;
                    }
                }
                else
                {
                    // Modification de l'angle de la balle si celle-ci tape la raquette
                    if (b.X + bw < rx + rw && b.X + bw > rx && b.Y < ry + rh && b.Y + bh > ry)
                    {
                        if (b.Angle > 180)
                            b.Angle = 540 - b.Angle;
                        else
                            b.Angle = 180 - b.Angle;

                        // Modification de l'angle selon l'endroit où la balle arrive sur la raquette
                        if(b.Y + bh > ry && b.Y + bh/2 < ry + rh/2)
                            b.Angle += (int)((double)((double)rh / 2 - ((b.Y + bh) - ry)) / ((double)rh / 2) * 60);
                        else
                            b.Angle -= (int) ((double)(b.Y - (ry + rh / 2)) / ((double)rh / 2) * 60);

                        if (b.Angle > 240 && b.Angle < 360)
                            b.Angle = 240;
                        else if (b.Angle < 120 && b.Angle > 0)
                            b.Angle = 120;
                    }

                    // Si la balle sort du terrain vers le joueur adverse
                    if (b.X < 0)
                    {
                        // La balle devient invisible et sa position est modifée
                        ball.Visible = false;
                        b.Y = (double)ball.Location.Y / (double)this.ClientSize.Height;

                        // Envoi des données vers le serveur
                        dt = new DataTransit();
                        dt.BallePro = b;
                        dt.Alive = true;
                        ClientClass.prepareSendData(dt);

                        b.Vitesse = 0;
                    }
                    // Si le joueur ne parvient pas à toucher la balle
                    else if (b.X > this.ClientSize.Width)
                    {
                        // La balle est cachée
                        ball.Visible = false;

                        // Le score est modifié
                        scoreServer++;
                        score.Text = scoreServer + "-" + scoreClient;

                        // Les informations sont envoyées à l'adversaire
                        b.EnDehors = true;
                        b.Vitesse = 0;
                        dt = new DataTransit();
                        dt.BallePro = b;
                        dt.Alive = true;
                        ClientClass.prepareSendData(dt);

                        // Le chrono est arrété
                        timerChrono.Enabled = false;

                        b.EnDehors = false;
                    }
                }

                // Modification de la position de la balle
                b.Delta();

                try
                {
                    ball.Location = new Point(b.X, (int)b.Y);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                // La balle est visible
                ball.Visible = true;

                // SI l'on est côté serveur
                if (joueur == 0)
                {
                    // Mise à jour de la balle
                    b = ServerClass.dt.BallePro;
                    b.X = this.ClientSize.Width;
                    b.Y = b.Y * this.ClientSize.Height;

                    // Si l'adversaire n'avait pas pu renvoyer la balle
                    if (b.EnDehors == true)
                    {
                        // Le chrono est stoppé
                        timerChrono.Enabled = false;

                        // Le score est modifié
                        scoreServer++;
                        score.Text = scoreServer + "-" + scoreClient;

                        // La balle est réinitialisée pour un nouveau lancé
                        ball.Location = new Point(this.ClientSize.Width / 2 - ball.Width / 2, this.ClientSize.Height / 2 - ball.Height / 2);
                        b = new Balle(ball.Location.X, ball.Location.Y);
                        b.EnDehors = false;
                    }

                    // On vide la variable de transfert de données
                    ServerClass.dt.BallePro = null;
                }
                else
                {
                    // Mise à jour de la balle
                    b = ClientClass.dt.BallePro;
                    b.X = 0;
                    b.Y = b.Y * this.ClientSize.Height;

                    // Si l'adversaire n'avait pas pu renvoyer la balle
                    if (b.EnDehors == true)
                    {
                        // Le chrono est stoppé
                        timerChrono.Enabled = false;

                        // Le score est modifié
                        scoreClient++;
                        score.Text = scoreServer + "-" + scoreClient;

                        // La balle est réinitialisée pour un nouveau lancé
                        ball.Location = new Point(this.ClientSize.Width / 2 - ball.Width / 2, this.ClientSize.Height / 2 - ball.Height / 2);
                        b = new Balle(ball.Location.X, ball.Location.Y);
                        b.EnDehors = false;
                    }

                    // On vide la variable de transfert de données
                    ClientClass.dt.BallePro = null;
                }
            }

            // Si l'on reçoit un message d'activation du timer par Bluetooth
            if (ServerClass.dt.Timer == true || ClientClass.dt.Timer == true)
            {
                // Relancement du chronomètre
                timerChrono.Enabled = true;

                // S'il manques un nom on le récupère
                if (nameClient == "" || nameServer == "")
                    if (joueur == 0)
                        nameClient = ServerClass.dt.NameJoueur;
                    else
                        nameServer = ClientClass.dt.NameJoueur;
            }

        }

        /// <summary>
        /// Méthode permettant d'écrire dans la zone d'affichage de la fenêtre de configuration bluetooth
        /// </summary>
        /// <param name="text">Texte à inscrire</param>
        /// <param name="type">Type de texte à afficher (permettant de modifier la couleur du texte à afficher)</param>
        public void updateOutputLog(String text, int type)
        {
            this.form.updateConsoleLog(text, type);
        }

        /// <summary>
        /// Méthode déclenchée lors du redimensionnement de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(Object sender, EventArgs e)
        {
            if (joueur == 1)
                raquette.Location = new Point(this.ClientSize.Width - 46, this.ClientSize.Height / 2 - raquette.Height / 2);

            temps.Location = new Point(this.ClientSize.Width / 2 - temps.Width / 2, temps.Location.Y);
            score.Location = new Point(this.ClientSize.Width / 2 - score.Width / 2, score.Location.Y);
        }

        /// <summary>
        /// Méthode exécutée à la fermeture de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Partie_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Initialisation d'un nouvel objet de transfert de données en indiquant que l'on ferme la fenêtre
            dt = new DataTransit();
            dt.Alive = false;
            this.updateOutputLog("Fermeture de la partie en cours", 0);

            // On envois les données et on ferme la connexion
            if (joueur == 1)
            {
                ClientClass.prepareSendData(dt);
                this.form.changeScanButtonActivate(true);
                ClientClass.CloseConnection();
                this.Dispose();
                
            }
            else
            {
                ServerClass.prepareSendData(dt);
                this.form.changeServerButtonActivate(true);
                ServerClass.CloseConnection();
                this.Dispose();
                this.form.InvokeClickServer();
            }

            // La fenêtre de configuration Bluetooth est réaffichée
            this.form.ChangeVisibily(true);
        }

        /// <summary>
        /// Timer servant à chronométrer et à prendre le relais en cas de non activité du timer 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            // Si l'adversaire a fermé la partie, on ferme la partie de notre côté
            if (ServerClass.dt.Alive == false || ClientClass.dt.Alive == false)
            {
                this.Partie_FormClosing(null, null);
            }

            // Si le chrono n'est pas arrivé à 0
            if (int.Parse(temps.Text) > 0 && temps.Text != "")
            {
                // Modifications du temps restant
                temps.Text = (int.Parse(temps.Text) - 1).ToString();
                temps.Location = new Point(this.ClientSize.Width / 2 - temps.Width / 2, temps.Location.Y);
            }
            else
            {
                // Sauvegarde dans la BDD du score
                if (timerMvmt.Enabled)
                {
                    Score scoreData = new Score();
                    scoreData.NomClient = nameClient;
                    scoreData.NomServeur = nameServer;
                    scoreData.ScoreClient = scoreClient;
                    scoreData.ScoreServeur = scoreServer;
                    SingletonDb.Instance.Scores.Add(scoreData);
                    SingletonDb.Instance.SaveChanges();   
                }

                timerMvmt.Enabled = false;

                // On indique au joueur s'il a gagné, perdu, ou s'il y a une égalité
                if (joueur == 0)
                {
                    Instructions.Visible = true;
                    if (scoreServer > scoreClient)
                    {
                        Victoire.Text = "You Win !";
                        Victoire.Location = new Point(this.ClientSize.Width / 2 - Victoire.Width / 2, Victoire.Location.Y);
                        Victoire.Visible = true;
                    }
                    else if (scoreServer < scoreClient)
                    {
                        Victoire.Text = "You Loose !";
                        Victoire.Location = new Point(this.ClientSize.Width / 2 - Victoire.Width / 2, Victoire.Location.Y);
                        Victoire.Visible = true;
                    }
                    else
                    {
                        Victoire.Text = "Par !";
                        Victoire.Location = new Point(this.ClientSize.Width / 2 - Victoire.Width / 2, Victoire.Location.Y);
                        Victoire.Visible = true;
                    }
                }
                else
                {
                    if (scoreServer > scoreClient)
                    {
                        Victoire.Text = "You Loose !";
                        Victoire.Location = new Point(this.ClientSize.Width / 2 - Victoire.Width / 2, Victoire.Location.Y);
                        Victoire.Visible = true;
                    }
                    else if (scoreServer < scoreClient)
                    {
                        Victoire.Text = "You Win !";
                        Victoire.Location = new Point(this.ClientSize.Width / 2 - Victoire.Width / 2, Victoire.Location.Y);
                        Victoire.Visible = true;
                    }
                    else
                    {
                        Victoire.Text = "Par !";
                        Victoire.Location = new Point(this.ClientSize.Width / 2 - Victoire.Width / 2, Victoire.Location.Y);
                        Victoire.Visible = true;
                    }
                }

                // Si l'adversaire a reset la partie, cela reset la partie de notre côté 
                if (ClientClass.dt.Reset == true)
                    this.Reset();
            }
        }

        /// <summary>
        /// Méthode permettant de reset la fenêtre en cours et ses variables
        /// </summary>
        private void Reset()
        {
            Victoire.Visible = false;
            Instructions.Visible = false;

            temps.Text = "100";
            temps.Location = new Point(this.ClientSize.Width / 2 - temps.Width / 2, temps.Location.Y);

            raquette.Location = new Point(raquette.Location.X, this.Height / 2 - raquette.Height / 2);

            scoreClient = 0;
            scoreServer = 0;
            score.Text = scoreServer + "-" + scoreClient;

            if (joueur == 0)
            {
                ball.Visible = true;
                ball.Location = new Point(this.ClientSize.Width / 2 - ball.Width / 2, this.ClientSize.Height / 2 - ball.Height / 2);
                b = new Balle(ball.Location.X, ball.Location.Y);
                b.EnDehors = false;

                // Envois de l'information de reset à l'adversaire
                dt = new DataTransit();
                dt.Reset = true;
                ServerClass.prepareSendData(dt);
            }
            else
            {
                ball.Visible = false;
                b.Vitesse = 0;
            }

            timerMvmt.Enabled = true;
            timerChrono.Enabled = false;
        }
    }
}
