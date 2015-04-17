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
using System.Text;

public class ServerClass
{
    private Form1 form;
    private Guid mUUID = new Guid("293eb187-b6e9-4434-894b-ef81120f0e5b");
    private BluetoothClient bluetoothClient;
    Stream messageStream = null;
    private bool ready = true;
    private BluetoothListener bluetoothServerListener;
    Thread bluetoothServerThread;
    Thread readingThread= null;
    static byte[]  messageRecu;
    static byte[]  messageSend;
    public static Balle b; 
    static bool messageAvailable = false;

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
        bluetoothServerListener.Start();

        bluetoothClient = bluetoothServerListener.AcceptBluetoothClient();
        /* Attend qu'un client se connect */
        this.updateOutputLog("Client connect",0);
        messageStream = bluetoothClient.GetStream();
        readingThread = new Thread(new ThreadStart(reading));
        readingThread.Start();
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
                this.updateOutputLog(e.Message,-1);
            }
            finally
            {
                this.updateOutputLog("Passage ICI +++ FIN d'envoi !!!!",0);
            }
            this.form.setReady(true);
            messageAvailable = false;
            
        }
        bluetoothClient.Close();
    }

    private void reading()
    {
        messageRecu = new byte[1024];
        try
        {
            while (true)
            {
                this.form.setReady(true);
                this.updateOutputLog("Receiving_Data", 0);
                messageStream.Read(messageRecu, 0, messageRecu.Length);
                this.updateOutputLog(Encoding.ASCII.GetString(messageRecu), 0);

                b = BinaryDeserializeObject(messageRecu);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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

    public static void prepareSendData(Balle b)
    {
        messageSend = BinarySerializeObject(b);
        messageAvailable = true;
    }


    private static byte[] BinarySerializeObject(Balle b)
    {
        if (b == null)
        {
            return new byte[0];
        }
        else
        {
            MemoryStream streamMemory = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(streamMemory, b);
            return streamMemory.GetBuffer();
        }
    }

    private static Balle BinaryDeserializeObject(byte[] bt)
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
                return (Balle) formatter.Deserialize(ms);
            }
        }
    }
}
