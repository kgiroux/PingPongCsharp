using System;
using System.Collections.Generic;
using System.Text;
using InTheHand;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Ports;
using InTheHand.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using PingPongCsharp;

public class ClientClass
{
    private BluetoothDeviceInfo[] devices_found = null;
    private BluetoothDeviceInfo device_selected = null;
    private BluetoothClient client = null;
    Thread bluetoothScanThread = null;
    Thread readingThread = null;
    Thread bluetoothClientThread = null;
    private Stream stream = null;
    private Guid mUUID = new Guid("293eb187-b6e9-4434-894b-ef81120f0e5b");
    private List<string> items_bluetooth;
    //BluetoothClient client;
    private Form1 form;
   /// <summary>
   /// Constructeur de l'objet Client Class 
   /// </summary>
   /// <param name="form"></param>
    public ClientClass(Form1 form)
	{
        this.form = form;
        items_bluetooth = new List<string>();
	}
    /// <summary>
    /// Methode permettant la connexion au client */
    /// </summary>
    public void connectAsClient(String name_device)
    {
        device_selected = null;
        if (devices_found != null)
        {
            foreach (BluetoothDeviceInfo device in devices_found)
            {
                if (name_device == device.DeviceName)
                {
                    device_selected = device;
                    break;
                }
            }
            this.updateOutputLog("Try to connect to : " + device_selected.DeviceName);
            if (device_selected != null)
            {
                if (pairDevice())
                {
                    this.updateOutputLog("Starting to connect");
                    this.updateOutputLog("Starting connecting Thread !!!");
                    bluetoothClientThread = new Thread(new ThreadStart(ClientConnectThread));
                    bluetoothClientThread.Start();
                }
                else
                {
                    this.updateOutputLog("Failed to connect");
                }
            }
            else
            {
                this.updateOutputLog("Device not found !!! ");
            }
        }
        else
        {
            this.updateOutputLog("Device not found !!! ");
        }
    }

    private void ClientConnectThread()
    {
        this.updateOutputLog("Connect");
        BluetoothClient client = new BluetoothClient();
        Console.WriteLine(device_selected.DeviceAddress);
        try
        {
            Console.WriteLine("ICI 6");
            client.BeginConnect(device_selected.DeviceAddress, mUUID, this.BluetoothClientConnectCallBack, client);
            Console.WriteLine("ICI 7");
        }
        catch (System.Net.Sockets.SocketException ex)
        {
            this.updateOutputLog("Error : " + ex.ToString());
        }
        catch (IOException ex)
        {
            this.updateOutputLog("Error : "+  ex.ToString());
        }
        catch (Exception ex)
        {
            this.updateOutputLog("Error : " + ex.ToString());
        }
        
        
    }
    /// <summary>
    ///  Methode gerant l'envoi et la réception des messages
    /// </summary>
    /// <param name="result"></param>
    void BluetoothClientConnectCallBack(IAsyncResult result)
    {
        client = (BluetoothClient)result.AsyncState;
        try
        {
            client.EndConnect(result);
            stream = client.GetStream();
            stream.ReadTimeout = 1000;
            Console.WriteLine("Ici 8");
            readingThread = new Thread(new ThreadStart(reading));
            readingThread.Start();
            while (true)
            {
                this.updateOutputLog("Sending_Data");
                send_date();
                stream.Write(message, 0, message.Length);
            }
        }
        catch (System.Net.Sockets.SocketException ex )
        {
            Console.WriteLine(ex.Message);
            this.updateOutputLog(ex.Message);
            this.updateOutputLog("Fail to connect to this serveur");
            if (readingThread != null)
            {
                readingThread.Abort();
            }
            this.updateOutputLog("Fermeture des connexions");
            if (stream != null)
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
            }
            this.updateOutputLog("S'agit-il d'un serveur Pour le PINGPONG ???");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            this.updateOutputLog(ex.Message);
            this.updateOutputLog("Fail to connect to this serveur");
            if (readingThread != null)
            {
                readingThread.Abort();
            }
            this.updateOutputLog("Fermeture des connexions");
            if (stream != null)
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
            }
            this.updateOutputLog("S'agit-il d'un serveur Pour le PINGPONG ???");
        }
    }
    /// <summary>
    /// Méthode Thread pour la récéption de message !!!
    /// </summary>
    /// <param name=""></param>
    private void reading()
    {
        byte[] message_recu = new byte[1024];

         try
        {
            while (true)
            {
                this.updateOutputLog("Receiving_Data");
                stream.Read(message_recu, 0, message_recu.Length);
                this.updateOutputLog(Encoding.ASCII.GetString(message_recu));
            }
        }
         catch (Exception ex)
         {
             Console.WriteLine(ex.Message);
         }
    }
    string myPin = "1234";
    bool ready = false;
    public bool pairDevice()
    {

        if (!device_selected.Authenticated)
        {
            if(!BluetoothSecurity.PairRequest(device_selected.DeviceAddress,myPin)){
                System.Console.WriteLine("ICI");
                return false;
            }
        }
        return true;
    }
    
    /// <summary>
    /// Lancement de la recherche Bluetooth via un thread (Permet de ne pas bloquer le programme)
    /// </summary>
    public void startScanBluetoothDevices()
    {
        bluetoothScanThread = new Thread(new ThreadStart(scan));
        bluetoothScanThread.Start();
    }
    /// <summary>
    /// Méthode gérant le scan des appareils bluetooth
    /// </summary>
    private void scan()
    {
        /* Mise à jour du texte dans la fenetre principal afin d'afficher les actions en arrière plan */
        this.updateOutputLog("Starting Scanning ...");
        this.updateOutputLog("Initializing component ...");

        /* Création via la Class contenu dans la InTheHand */
        client = new BluetoothClient();
        this.updateOutputLog("Done\n");
        /* Lancement de la recherche bluetooth */
        this.updateOutputLog("Searching Bluetooth Devices ...");
        BluetoothDeviceInfo[] devices = client.DiscoverDevicesInRange();
        
        /* On récupère un tableau de contenant les appareils */
        foreach (BluetoothDeviceInfo d in devices)
        {
            /* Affichage de listes des appareils disponibles*/
            System.Console.WriteLine(d.DeviceName);
            if (d != null)
            {
                items_bluetooth.Add(d.DeviceName);
            }   
        }
        this.updateOutputLog("Ending of the research");
        this.updateOutputLog("Creation of the list");
        /* Méthode permettant la mise à jour des appareils */
        this.updateListBox();
        this.updateOutputLog("=========================");
        this.devices_found = devices;
    }
    /// <summary>
    /// Getter permettant d'avoir le nom des appareils
    /// </summary>
    /// <returns></returns>
    public List<string> getDevicesName()
    {
        return items_bluetooth;
    }
    /// <summary>
    ///  Méthode permettant d'acceder à la méthode dédié au changement de la liste 
    /// </summary>
    public void updateListBox()
    {
        this.form.updateDevicesList(this.getDevicesName());
    }
    /// <summary>
    ///  Méthode permettant d'acceder à la méthode dédié au changement de la TextBox
    /// </summary>
    /// <param name="text"></param>
    public void updateOutputLog(String text)
    {
        this.form.updateConsoleLog(text);
    }
    /// <summary>
    /// Méthode permettant de fermer la connection
    /// </summary>
    public void CloseConnection()
    {
        client.Close();
    }
    ~ClientClass()
    {
        devices_found = null;
        items_bluetooth = null;
        client = null;
        form = null;
        if (bluetoothScanThread != null)
        {
            bluetoothScanThread.Abort();
        }
        if (readingThread != null)
        {
            readingThread.Abort();
        }
        if (stream != null)
        {
            stream.Close();
        }
        if (bluetoothClientThread != null)
        {
            bluetoothClientThread.Abort();
        }
        
    }
    byte[] message;
    private void send_date()
    {
        int rand = new Random().Next();
        message = Encoding.ASCII.GetBytes("Sending Message" + rand);
        ready = true;
    }


}
