using System;
using InTheHand;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Ports;
using InTheHand.Net.Sockets;
using System.IO;
using PingPongCsharp;
using System.Threading;
using System.Text;

public class ServerClass
{
    private Form1 form;
    private Guid mUUID = new Guid("293eb187-b6e9-4434-894b-ef81120f0e5b");
    private BluetoothClient bluetoothClient;
    Stream messageStream = null;
    private BluetoothListener bluetoothServerListener;
    Thread bluetoothServerThread;
    /// <summary>
    /// Constructeur de ServerClass
    /// </summary>
    /// <param name="form"></param>
    public ServerClass(Form1 form)
	{
        this.form = form;
	}
    /// <summary>
    ///  Méthode de lancement pour la création du serveur */
    /// </summary>

    bool serverLaunch = false;
    public void connectAsServer()
    {
        
        this.updateOutputLog("Launching Server ...");
        bluetoothServerThread = new Thread(new ThreadStart(start_server));
        if(!serverLaunch)
            bluetoothServerThread.Start();
        else
        {
            this.updateOutputLog("Server already launched");
        }
        serverLaunch = true;
    }
    /// <summary>
    /// Methode de démarrage du serveur
    /// </summary>
    private void start_server()
    {
        this.updateOutputLog("Initializing component...");
        /* Création du listener pour le serveur */
        bluetoothServerListener = new BluetoothListener(this.mUUID);

        Console.WriteLine(this.mUUID);
        bluetoothServerListener.Start();

        bluetoothClient = bluetoothServerListener.AcceptBluetoothClient();
        /* Attend qu'un client se connect */
        this.updateOutputLog("Client connect");
        messageStream = bluetoothClient.GetStream();
        while (true)
        {
            this.updateOutputLog("Receiving Data");
            // Récupération d'un message envoyer au client
            byte[] received_data = new byte[1024];
            // Reception
            try
            {
                messageStream.Read(received_data, 0, received_data.Length);
                this.updateOutputLog(Encoding.ASCII.GetString(received_data));
            }
            catch (IOException e)
            {
                this.updateOutputLog(e.Message);
            }
            
            //this.updateOutputLog("Receiving Data 2");

            String received_string = Encoding.ASCII.GetString(received_data);
            this.updateOutputLog("Received:" + received_string );

            if (received_string.Contains("quit"))
            {
                break;
            }
            byte[] send_data = Encoding.ASCII.GetBytes("Hello world !!!!");
            try
            {
                messageStream.Write(send_data, 0, send_data.Length);
            }
            catch (IOException e)
            {
                this.updateOutputLog(e.Message);
            }
            finally
            {
                this.updateOutputLog("Passage ICI +++ FIN d'envoi !!!!");
            }
            
        }
        bluetoothClient.Close();
    }
    /// <summary>
    /// Méthode qui permet de mettre à jour l'affichage de la texte box
    /// </summary>
    /// <param name="text"></param>
    public void updateOutputLog(String text)
    {
        this.form.updateConsoleLog(text);
    }

    
    public void closeServer()
    {
       //this.bluetoothServerThread.Finalize();
    }
    /// <summary>
    /// Destructeur de la classe 
    /// </summary>
    ~ServerClass()
    {
        if (bluetoothServerListener != null)
        {
            bluetoothServerListener.Stop();
        }
        if (messageStream != null)
        {
            messageStream.Close();
        }
        if (bluetoothServerThread != null)
        {
            bluetoothServerThread.Abort();
        }
        if (bluetoothClient != null)
        {
            bluetoothClient.Close();
        }
    }
}
