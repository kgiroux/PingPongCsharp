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
    public partial class ConfigurationPanel : Form
    {
        Welcome wlcm;
        private ClientClass clt;
        Partie p = null;
        private ServerClass svr;
        private Boolean ready = false;
        private Boolean ready_client = false;
        private Thread launching_partie = null;
        List<string> item = null;
        /* Constructeur de la fenetre*/
        
        /// <summary>
        /// Constructeur de la form1
        /// </summary>
        public ConfigurationPanel(Welcome wlcm)
        {
            InitializeComponent();
            serveur.Enabled = false;
            scan_button.Enabled = false;
            item = new List<string>();
            this.wlcm = wlcm;
        }

        /* Méthode qui charge la fenetre */
        /// <summary>
        /// Méthode chargeant la fenetre 1 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigurationPanel_Load(object sender, EventArgs e)
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
            textBox1.Enabled = false;
            scan_button.Enabled = false;
            serveur.Enabled = false;
            this.ready = false;
            /* Créer un objet ServerClass qui va être l'objet contenir toute les méthodes concernant le serveur */
            /* On lui passe l'objet form pour pouvoir modifier les textes dans la liste et la textBox */
            svr = new ServerClass(this);
            Launching_partie(0);
            /* Lancement de la methode pour lancer le serveur */
            svr.connectAsServer();
            
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
                textBox1.Enabled = true;
                this.updateConsoleLog("Fermeture des connexions", -1);
                if (svr != null)
                {
                    ServerClass.CloseConnection();
                }
                if (clt != null)
                {
                    ClientClass.CloseConnection();
                }
        }

        /* Bouton Scan qui va être chercher les appareils pour le client */
        /// <summary>
        /// Méthode dédiée à la gestion du click sur le bouton scan 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scan_button_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            this.updateDevicesList(new List<string>());
            this.ready_client = false;
            clt = new ClientClass(this);
            
            clt.startScanBluetoothDevices();
        }
        
        /// <summary>
        /// Méthode permettant de mettre à jour la liste des appareils disponibles via la détection bluetooth
        /// </summary>
        /// <param name="item_return"></param>
        public void updateDevicesList(List<string> item_return)
        {
            this.updateConsoleLog("Création de la liste des appareils disponibles",0);
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
        /// <summary>
        /// Message de LOG 
        /// </summary>
        /// <param name="text"> Texte à faire apparaitre dans la text view</param>
        /// <param name="type">Type de message (erreur, normale, success) </param>
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
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Event lors du double click sur un item de la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxDevice_DoubleClick(object sender, EventArgs e)
        {
            serveur.Enabled = false;
            textBox1.Enabled = false;
            /* Lance la connexion au serveur */
            Launching_partie(1);
            clt.connectAsClient(listBoxDevice.SelectedItem.ToString());
        }


        /// <summary>
        /// Libère la boucle de la méthode ClientConnected
        /// </summary>
        /// <param name="ready">True si la connexion est faite</param>
        public void setReady(Boolean ready)
        {
            this.ready = ready;
        }
        /// <summary>
        /// Libère la boucle de la méthode ClientConnectedServer
        /// </summary>
        /// <param name="ready">True si l'acceptation est faite</param>
        public void setReadyClient(Boolean ready)
        {
            this.ready_client = ready;
        }



        /// <summary>
        /// Methode qui va attendre une connexion d'un client
        /// </summary>
        private void ClientConnected()
        {
            // Serveur
            while (!ready) ;
            p = new Partie(0, this);
            ready = false;
            this.ChangeVisibily(false);
            p.ShowDialog();
        }
        /// <summary>
        /// Methode qui va attendre l'acceptation du serveur
        /// </summary>
        private void ClientConnectedServer()
        {
            // Client
            while (!ready_client) ;
            p = new Partie(1,this);
            ready_client = false;
            this.ChangeVisibily(false);
            p.ShowDialog();
            
        }

        /// <summary>
        /// Lancement des methodes d'écoute de la connexion (serveur) et de l'acceptation (client) 
        /// </summary>
        /// <param name="mode">1 pour le Serveur, 0 pour le client</param>
        private void Launching_partie(int mode)
        {
            if (mode == 0)
            {
                this.updateConsoleLog("Lancement serveur",1);
                launching_partie = new Thread(new ThreadStart(ClientConnected));
                launching_partie.IsBackground = true;
                launching_partie.Start();
            }
            else if(mode == 1)
            {
                launching_partie = new Thread(new ThreadStart(ClientConnectedServer));
                launching_partie.IsBackground = true;
                launching_partie.Start();
            }
            else
            {
                Console.WriteLine("ICI");
            }
            
        }
        

       /// <summary>
       /// Change le status du bouton Server
       /// </summary>
       /// <param name="activated">status du bouton</param>
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
        /// <summary>
        /// Change le status du bouton Scan
        /// </summary>
        /// <param name="activated">status du bouton</param>
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

        /// <summary>
        /// Change la visibilité de la form de configuration
        /// </summary>
        /// <param name="activated">status de la form</param>

        public void ChangeVisibily(Boolean activated)
        {
            Func<int> del = delegate()
            {
                this.Visible = activated;
                if (activated)
                {
                    this.Show();
                }else{
                    this.Hide();
                }
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
        /// <summary>
        /// Permet de simuler un click
        /// </summary>
        public void InvokeClickServer()
        {
            Func<int> del = delegate()
            {
                serveur_click(null, null);
                return 0;
            };
            Invoke(del);
        }

        public void InvokeClickStop()
        {
            Func<int> del = delegate()
            {
                stop_action(null, null);
                return 0;
            };
        }
        /// <summary>
        /// Event de fermeture 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigurationPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.InvokeClickStop();
            this.Dispose();
            this.wlcm.Dispose();
        }
        /// <summary>
        /// Event pour le changement du texte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            // Si le texte est différent de la chaine vide
            if (!("".Equals(textBox1.Text)))
            {
                serveur.Enabled = true;
                scan_button.Enabled = true;
            }
            // Sinon si le text est différent de null
            else
            {
                serveur.Enabled = false;
                scan_button.Enabled = false;
            }
        }

    }
}