﻿using System;
using System.Collections.Generic;
using System.Text;
using InTheHand;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Ports;
using InTheHand.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Threading;
using System.Linq;
using PingPongCsharp;

///<author>
///Cyril LEFEBVRE & Kévin Giroux
///</author>
public class ClientClass
{
    private BluetoothDeviceInfo[] devices_found = null;
    private BluetoothDeviceInfo device_selected = null;
    private static BluetoothClient client = null;
    private static Thread bluetoothScanThread = null;
    private static Thread readingThread = null;
    private static Thread bluetoothClientThread = null;
    private static Stream stream = null;
    private Guid mUUID = new Guid("293eb187-b6e9-4434-894b-ef81120f0e5b");
    private List<string> items_bluetooth;
    private int error = 0;
    private ConfigurationPanel form;
    static byte[] messageSend;
    static byte[] messageRecu;
    static bool messageAvailable; 
    public static DataTransit dt = new DataTransit();

   /// <summary>
   /// Constructeur de l'objet Client Class 
   /// </summary>
   /// <param name="form"></param>
    public ClientClass(ConfigurationPanel form)
	{
        this.form = form;
        items_bluetooth = new List<string>();
	}

    /// <summary>
    /// Methode permettant la connexion au client (tester)*/
    /// </summary>
    public int connectAsClient(String name_device)
    {
        error = 0;
        device_selected = null;
        if (devices_found != null)
        {
            // Si on trouver le nom de l'appareil que sur lequel nous avons cliqué
            foreach (BluetoothDeviceInfo device in devices_found)
            {
                if (name_device == device.DeviceName)
                {
                    device_selected = device;
                    break;
                }
            }
            // On essaye de se connecter
            this.updateOutputLog("Try to connect to : " + device_selected.DeviceName, 0);
            if (device_selected != null)
            {
                // Si les appareils sont déjà associer;
                if (pairDevice())
                {
                    this.updateOutputLog("Starting to connect",0);
                    this.updateOutputLog("Starting connecting Thread !!!",0);
                    bluetoothClientThread = new Thread(new ThreadStart(ClientConnectThread));
                    bluetoothClientThread.IsBackground = true;
                    bluetoothClientThread.Start();
                }
                // Sinon impossible de se connecter
                else
                {
                    this.updateOutputLog("Failed to connect",-1);
                    error = -1;
                }
            }
            // Appareil non trouvé
            else
            {
                this.updateOutputLog("Device not found !!! ", -1);
                error = -1;
            }
        }
        // Liste des appareil vide
        else
        {
            this.updateOutputLog("Device not found !!! ", -1);
            error = -1;
        }
        return error;
    }

    /// <summary>
    /// Thread Pour la connexion du client
    /// </summary>
    private void ClientConnectThread()
    {
        /// Démarrage du thread pour se connecter au serveur
        this.updateOutputLog("Connect",0);
        client = new BluetoothClient();
        Console.WriteLine(device_selected.DeviceAddress);
        try
        {
            client.BeginConnect(device_selected.DeviceAddress, mUUID, this.BluetoothClientConnectCallBack, client);
        }
        catch (System.Net.Sockets.SocketException ex)
        {
            this.error = -1;
            this.updateOutputLog("Error : " + ex.ToString(),-1);
        }
        catch (IOException ex)
        {
            this.error = -1;
            this.updateOutputLog("Error : "+  ex.ToString(),-1);
        }
        catch (Exception ex)
        {
            this.error = -1;
            this.updateOutputLog("Error : " + ex.ToString(),-1);
        }
    }

    /// <summary>
    ///  Methode gerant l'envoi et la réception des messages
    /// </summary>
    /// <param name="result"></param>
    void BluetoothClientConnectCallBack(IAsyncResult result)
    {
        /// Récupération des messages de façon asynchrones
        client = (BluetoothClient)result.AsyncState;
        try
        {
            client.EndConnect(result);
            stream = client.GetStream();
            readingThread = new Thread(new ThreadStart(reading));
            readingThread.IsBackground = true;
            readingThread.Start();
            while (true)
            {
                messageSend = new byte[1024];
                while (!messageAvailable) ;
                try
                {
                    // Ecriture du messages
                    this.updateOutputLog(Encoding.ASCII.GetString(messageSend),0);
                    stream.Write(messageSend, 0, messageSend.Length);
                }
                catch (IOException e)
                {
                    this.updateOutputLog(e.Message, -1);
                }
                finally
                {
                    this.updateOutputLog("Passage ICI +++ FIN d'envoi !!!!", 0);
                }
                this.form.setReady(true);
                messageAvailable = false;
            }
        }
        // cas d'erreur
        // Fermeture des thread et des sockets
        catch ( Exception ex )
        {
            this.error = -1;
            Console.WriteLine(ex.Message);
            this.updateOutputLog(ex.Message,-1);
            this.updateOutputLog("Fail to connect to this serveur",-1);
            if (readingThread != null)
            {
                readingThread.Abort();
            }
            this.updateOutputLog("Fermeture des connexions",-1);
            if (stream != null)
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
            }
        }
    }

    /// <summary>
    /// Méthode Thread pour la récéption de message !!!
    /// </summary>
    /// <param name=""></param>
    private async void reading()
    {
        this.error = -1;
        messageRecu = new byte[1024];
        messageRecu.Initialize();
        byte[] message_test = new byte[1024];
        message_test.Initialize();
        try
        {
            while (true)
            {
                this.form.setReadyClient(true);
                await stream.ReadAsync(messageRecu, 0, messageRecu.Length);
                this.updateOutputLog("ICI " + Encoding.ASCII.GetString(messageRecu), 0);
                dt = BinaryDeserializeObject(messageRecu);
                messageRecu = new byte[1024];
                messageRecu.Initialize();
            }
        }
         catch (Exception ex)
         {
             CloseConnection();
             this.updateOutputLog(ex.Message, -1);
         }
    }

    /// <summary>
    /// Methode pour l'association des appareils
    /// </summary>
    public bool pairDevice()
    {
        string myPin = "1234";
        if (!device_selected.Authenticated)
        {
            if(!BluetoothSecurity.PairRequest(device_selected.DeviceAddress,myPin)){
                this.updateOutputLog("Failed to pair devices", -1);
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
        bluetoothScanThread.IsBackground = true;
        try
        {
            bluetoothScanThread.Start();
        }
        catch (ThreadStartException th)
        {
            this.updateOutputLog(th.Message, -1);
            ClientClass.CloseConnection();
            this.updateOutputLog("Erreur lancement du processus", -1);
        }
    }

    /// <summary>
    /// Méthode gérant le scan des appareils bluetooth
    /// </summary>
    private void scan()
    {
        /* Mise à jour du texte dans la fenetre principal afin d'afficher les actions en arrière plan */
        this.updateOutputLog("Starting Scanning ...", 0);
        this.updateOutputLog("Initializing component ...", 0);

        /* Création via la Class contenu dans la InTheHand */
        client = new BluetoothClient();
        this.updateOutputLog("Done", 0);
        /* Lancement de la recherche bluetooth */
        this.updateOutputLog("Searching Bluetooth Devices ...", 0);
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
        this.updateOutputLog("Ending of the research", 0);
        this.updateOutputLog("Creation of the list", 0);
        /* Méthode permettant la mise à jour des appareils */
        this.updateListBox();
        this.updateOutputLog("=========================", 0);
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
    public void updateOutputLog(String text, int type)
    {
        this.form.updateConsoleLog(text,type);
    }
    /// <summary>
    /// Méthode permettant de fermer la connection
    /// Suppression des thread
    /// Fermeture des sockets
    /// </summary>
    public static void CloseConnection()
    {
        if (client != null)
        {
            client.Close();
        } 
        if (bluetoothScanThread != null)
        {
            if (bluetoothScanThread.IsAlive == true)
            {
                bluetoothScanThread.Abort();
            }
            else
            {
                bluetoothClientThread = null;
            }
            
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
            if (bluetoothClientThread.IsAlive == true)
            {
                bluetoothClientThread.Abort();
            }
            else
            {
                bluetoothClientThread = null;
            }
        }
    }
    /// <summary>
    /// Destructeur de la classe
    /// </summary>
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
        try
        {
            this.form.changeScanButtonActivate(true);
        }
        catch (System.NullReferenceException ex)
        {
            Console.WriteLine("==> " +ex.Message);
        }
    }

    /// <summary>
    /// Message qui lance la préparation du message
    /// </summary>
    /// <param name="b">Passage de Balle</param>
    public static void prepareSendData(DataTransit b)
    {
        messageSend = BinarySerializeObject(b);
        messageAvailable = true;
    }
    /// <summary>
    /// Methode qui serialise un objet en tableau de Byte. Cette méthode sert à préparer un message à envoyé
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    private static byte[] BinarySerializeObject(DataTransit b)
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
            return streamMemory.ToArray();
        }
    }
    /// <summary>
    /// Methode pour désarialiser un objet après reception de celui-ci
    /// </summary>
    /// <param name="bt">Passage du tableau de Byte qui a été reçu</param>
    /// <returns>Renvoi un objet de type Balle</returns>
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

}
