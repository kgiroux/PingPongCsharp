using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PingPongCsharp
{
    public partial class Form1 : Form
    {
        private ClientClass clt;
        Partie p = null;
        private ServerClass svr;
        private Boolean ready = false;
        private Boolean ready_client = false;
        private Thread launching_partie = null;
        List<string> item = null;
        /* Constructeur de la fenetre*/
        
        /// <summary>
        /// Constructeur de la form 1
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            item = new List<string>();
        }

        /* Méthode qui charge la fenetre */
        /// <summary>
        /// Méthode chargeant la fenetre 1 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /* Methode Click pour le bouton */
        /* Méthode concernant le serveur */
        /// <summary>
        /// Méthode dédiée à la gestion du click sur le bouton Client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serveur_click(object sender, EventArgs e)
        {
            /* Désactivation du bouton Client et du bouton Scan */
            /* Configuration du Serveur */
            scan_button.Enabled = false;
            serveur.Enabled = false;
            /* Créer un objet ServerClass qui va être l'objet contenir toute les méthodes concernant le serveur */
            /* On lui passe l'objet form pour pouvoir modifier les textes dans la liste et la textBox */
            svr = new ServerClass(this);
            
            Launching_partie(0);
            /* Lancement de la methode pour lancer le serveur */
            svr.connectAsServer();
        }
        /* Méthode de lancement Client, cela va gerer la connection en fonction de la cible choisit */
        /// <summary>
        /// Méthode dédiée à la gestion du click sur le bouton Client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_Click(object sender, EventArgs e)
        {
            /* Désactive le bouton Server */
            serveur.Enabled = false;
            /* Lance la connexion au serveur */
            Console.WriteLine(listBoxDevice.SelectedItem.ToString());
            clt.connectAsClient(listBoxDevice.SelectedItem.ToString());
        }
        /* Bouton Stop qui va permettre de quitter le serveur et le client */
        /// <summary>
        /// Méthode dédiée à la gestion du click sur le bouton Stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void stop_action(object sender, EventArgs e){
                serveur.Enabled = true;
                scan_button.Enabled = true;
                textBox1.Text = "";
                Partie p = new Partie(1,this);
                p.Show();
        }

        /* Bouton Scan qui va être chercher les appareils pour le client */
        /// <summary>
        /// Méthode dédiée à la gestion du click sur le bouton scan 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scan_button_Click(object sender, EventArgs e)
        {
            this.updateDevicesList(new List<string>());
            clt = new ClientClass(this);
            
            clt.startScanBluetoothDevices();
        }
        
        /* Méthode permettant de mettre à jour la liste du contenu dans la liste Box */
        /// <summary>
        /// Méthode permettant de mettre à jour la liste des appareils disponibles via la détection bluetooth
        /// </summary>
        /// <param name="item_return"></param>
        public void updateDevicesList(List<string> item_return)
        {
            this.updateConsoleLog("Création de la liste des appareils disponibles");
            /* Création d'une méthode sécurisé (pointeur sur une fonction) pour changer le contenu de la liste. Cette méthode est anonyme*/
            Func<int> del = delegate()
            {
                /* Remplacement de la liste actuelle par null (RESET)*/
                listBoxDevice.DataSource = null;
                /* Création de la liste avec les nouvelles données */
                listBoxDevice.DataSource = item_return;
                return 0;
            };
            Invoke(del);
        }

        /* Méthode permettant de mettre du texte dans notre textBox */

        /// <summary>
        ///     Permet de mettre à jour l'object TextBox dédié à l'affichage des logs 
        /// </summary>
        /// <param name="text"></param>
        public void updateConsoleLog(string text)
        {
            /* Création d'une méthode sécurisé (pointeur sur une fonction) pour changer le contenu de la liste. Cette méthode est anonyme*/
            Func<int> del = delegate()
            {
                /* Rajoute du texte à la texte box */
                //outPutLog.ForeColor= Color.Red ;
                outPutLog.AppendText(text + System.Environment.NewLine);
                return 0;
            };
            try
            {
                Invoke(del);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void updateConsoleLog(string text, int type)
        {
            /* Création d'une méthode sécurisé (pointeur sur une fonction) pour changer le contenu de la liste. Cette méthode est anonyme*/
            Func<int> del = delegate()
            {
                Color color;
                /* Rajoute du texte à la texte box */
                switch (type)
                {
                    case 0 :
                        color = Color.Black;
                        break;
                    case 1 :
                        color = Color.Green;
                        break;
                    case -1 :
                        color = Color.Red;
                        break;
                    default :
                        color = Color.Black;
                        break;
                }
                outPutLog.SelectionStart = outPutLog.TextLength;
                outPutLog.SelectionLength = 0;
                outPutLog.SelectionColor = color;
                outPutLog.AppendText(text + System.Environment.NewLine);
                outPutLog.SelectionColor = outPutLog.ForeColor;
                return 0;
            };
            try
            {
                Invoke(del);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void listBoxDevice_DoubleClick(object sender, EventArgs e)
        {
            serveur.Enabled = false;
            /* Lance la connexion au serveur */
            Launching_partie(1);
            clt.connectAsClient(listBoxDevice.SelectedItem.ToString());
        }

        private void ClientConnected()
        {
            // Serveur
            while (!ready) ;
            p = new Partie(0, this);
            p.ShowDialog();
            ready = false;
            this.ChangeVisibily(false);
        }

        private void ClientConnectedServer()
        {
            // Client
            while (!ready_client) ;
            p = new Partie(1,this);
            p.ShowDialog();
            ready_client = false;
            this.ChangeVisibily(false);
        }

        
        private void Launching_partie(int mode)
        {
            if (mode == 0)
            {
                launching_partie = new Thread(new ThreadStart(ClientConnected));
                launching_partie.Start();
            }
            else if(mode == 1)
            {
                launching_partie = new Thread(new ThreadStart(ClientConnectedServer));
                launching_partie.Start();
            }
            else
            {
                Console.WriteLine("ICI");
            }
            
        }

        public void setReady(Boolean ready)
        {
            this.ready = ready;
        }

        public void setReadyClient(Boolean ready)
        {
            this.ready_client = ready;
        }

        public void changeServerButtonActivate(Boolean activated)
        {
            Func<int> del = delegate()
            {
                this.serveur.Enabled = activated;
                return 0;
            };
            try
            {
                Invoke(del);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine("====>>> changeServerButtonActivate " + ex.Message);
            }
        }

        public void changeScanButtonActivate(Boolean activated)
        {
            Func<int> del = delegate()
            {
                this.scan_button.Enabled = activated;
                return 0;
            };
            try
            {
                Invoke(del);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine("====>>> changeScanButtonActivate " + ex.Message);
            }
        }

        public void ChangeVisibily(Boolean activated)
        {
            Func<int> del = delegate()
            {
                this.Visible = activated;
                this.Hide();
                return 0;
            };
            try
            {
                Invoke(del);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine("====>>> changeScanButtonActivate " + ex.Message);
            }
        }
    }
}