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
    public partial class Form1 : Form
    {
        private ClientClass clt;
        private ServerClass svr;
        List<string> item = null;
        /* Constructeur de la fenetre*/
        
        /// <summary>
        /// Constructeur de la form 1
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            client.Enabled = false;
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
            client.Enabled = false;
            scan_button.Enabled = false;
            serveur.Enabled = false;
            /* Créer un objet ServerClass qui va être l'objet contenir toute les méthodes concernant le serveur */
            /* On lui passe l'objet form pour pouvoir modifier les textes dans la liste et la textBox */
            svr = new ServerClass(this);
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
                client.Enabled = false;
                scan_button.Enabled = true;
                textBox1.Text = "";
                Partie p = new Partie();
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
            client.Enabled = false;
            clt = new ClientClass(this);
            clt.startScanBluetoothDevices();

            if (listBoxDevice.DataSource != null)
            {
                client.Enabled = true;
            }
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
                outPutLog.AppendText(text + System.Environment.NewLine);
                return 0;
            };
            Invoke(del);
        }

        
    }
}