using System;
using InTheHand;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Ports;
using InTheHand.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using PingPongCsharp;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

public class ServerClass
{
    private Form1 form;
    private Guid mUUID = new Guid("293eb187-b6e9-4434-894b-ef81120f0e5b");
    private static BluetoothClient bluetoothClient;
    private static Stream messageStream = null;
    private static BluetoothListener bluetoothServerListener;
    private static Thread bluetoothServerThread = null;
    private static Thread readingThread= null;
    static byte[]  messageRecu;
    static byte[]  messageSend;
    public static DataTransit dt = new DataTransit(); 
    private static bool messageAvailable = false;
    private static bool serverLaunch = false;

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

    
    public void connectAsServer()
    {      
        this.updateOutputLog("Launching Server ...",0);
        bluetoothServerThread = new Thread(new ThreadStart(start_server));
        if(!serverLaunch)
            bluetoothServerThread.Start();
        else
        {
            this.updateOutputLog("Server already launched",0);
        }
        serverLaunch = true;
    }
    /// <summary>
    /// Methode de démarrage du serveur
    /// </summary>
    private void start_server() 
    {
        this.updateOutputLog("Initializing component...",0);
        /* Création du listener pour le serveur */
        bluetoothServerListener = new BluetoothListener(this.mUUID);

        Console.WriteLine(this.mUUID);
        try
        {
            bluetoothServerListener.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        

        bluetoothClient = bluetoothServerListener.AcceptBluetoothClient();
        /* Attend qu'un client se connect */
        this.updateOutputLog("Client connect",0);
        messageStream = bluetoothClient.GetStream();
        readingThread = new Thread(new ThreadStart(reading));
        readingThread.Start();

        try
        {
            while (true)
            {
                messageSend = new byte[1024];
                while (!messageAvailable);
                try
                {
                    messageStream.Write(messageSend, 0, messageSend.Length);
                }
                catch (IOException e)
                {
                    this.updateOutputLog("====>>>" + e.Message, -1);
                }
                finally
                {
                    this.updateOutputLog("Passage ICI +++ FIN d'envoi !!!!", 0);
                }
                this.form.setReady(true);
                messageAvailable = false;

            }
        }
        catch (System.Net.Sockets.SocketException ex)
        {
            Console.WriteLine("=== > 4" + ex.Message);
            this.updateOutputLog(ex.Message, -1);
            this.updateOutputLog("Fail to connect to this serveur", -1);
            if (readingThread != null)
            {
                readingThread.Abort();
            }
            this.updateOutputLog("Fermeture des connexions", -1);
            if (messageStream != null)
            {
                messageStream.Close();
            }
            if (bluetoothClient != null)
            {
                bluetoothClient.Close();
            }

            try
            {
                this.form.changeServerButtonActivate(true);
            }
            catch (System.NullReferenceException ex1)
            {
                Console.WriteLine("==>   this.form.changeServerButtonActivate " + ex1.Message);
            }
       
        }
        catch (Exception ex)
        {
            Console.WriteLine("=== > 4" +ex.Message);
            this.updateOutputLog(ex.Message, -1);
            this.updateOutputLog("Fail to connect to this serveur", -1);
            if (readingThread != null)
            {
                readingThread.Abort();
            }
            this.updateOutputLog("Fermeture des connexions", -1);
            if (messageStream != null)
            {
                messageStream.Close();
            }
            if (bluetoothClient != null)
            {
                bluetoothClient.Close();
            }

            try
            {
                this.form.changeServerButtonActivate(true);
            }
            catch (System.NullReferenceException ex1)
            {
                Console.WriteLine("==>   this.form.changeServerButtonActivate 2" + ex1.Message);
            }
        }
        bluetoothClient.Close();
    }
    /// <summary>
    /// Methode de lecture pour le serveur. Il s'agit d'un thread qui attend un message
    /// </summary>
    private async void reading()
    {
        messageRecu = new byte[1024];
        messageRecu.Initialize();
        byte[] message_test = new byte[1024];
        message_test.Initialize();
        try
        {
            while (true)
            {
                this.form.setReady(true);
                int x = await messageStream.ReadAsync(messageRecu, 0, messageRecu.Length);
                this.updateOutputLog("ICI " + Encoding.ASCII.GetString(messageRecu), 0);
                dt = BinaryDeserializeObject(messageRecu);
                messageRecu = new byte[1024];
                messageRecu.Initialize();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("=== > 5" +ex.Message);
            CloseConnection();
        }
    }

    /// <summary>
    /// Méthode qui permet de mettre à jour l'affichage de la texte box
    /// </summary>
    /// <param name="text"></param>
    public void updateOutputLog(String text , int type)
    {
        this.form.updateConsoleLog(text,type);
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
    /// <summary>
    /// Methode préparant les messages pour le serveur
    /// </summary>
    /// <param name="b"> Objet à envoyer</param>
    public static void prepareSendData(DataTransit b)
    {
        messageSend = BinarySerializeObject(b);
        messageAvailable = true;
    }
    /// <summary>
    /// Methode sérialisant un objet pour le transformer en tableau de Byte
    /// </summary>
    /// <param name="b">Objet à sérialiser</param>
    /// <returns>Renvoi un tableau de Byte contenant le message à sérialiser</returns>
    private static byte[] BinarySerializeObject(DataTransit dt)
    {
        if (dt == null)
        {
            return new byte[0];
        }
        else
        {
            MemoryStream streamMemory = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(streamMemory, dt);
            return streamMemory.ToArray();
        }
    }
    /// <summary>
    /// Message à déserialiser 
    /// </summary>
    /// <param name="bt">Tableau provenant du message recu</param>
    /// <returns>Renvoi un objet BALLE</returns>
    private static DataTransit BinaryDeserializeObject(byte[] bt)
    {
        if (bt == null)
        {
            return null;
        }
        else
        {
            if (bt.Length == 0)
            {
                return null;
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(bt);
                return (DataTransit)formatter.Deserialize(ms);
            }
        }
    }

    /// <summary>
    /// Methode fermant les objets et threads utilisés
    /// </summary>
    public static void CloseConnection()
    {
        if (bluetoothClient != null)
        {
            bluetoothClient.Close();
            
        }
        if (bluetoothServerListener != null)
        {
            try
            {
                bluetoothServerListener.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        if (bluetoothServerThread != null)
        {
            bluetoothServerThread.Abort();
        }
        if (readingThread != null)
        {
            readingThread.Abort();
        }
        if (messageStream != null)
        {
            messageStream.Close();
        }
        if (readingThread != null)
        {
            readingThread.Abort();
        }
        serverLaunch = false;
    }
}
