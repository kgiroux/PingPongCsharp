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
using PingPongCsharp;

public class ClientClass
{

    private BluetoothDeviceInfo[] devices_found = null;
    private BluetoothDeviceInfo device_selected = null;
    private BluetoothClient client = null;
    private Thread bluetoothScanThread = null;
    private Thread readingThread = null;
    private Thread bluetoothClientThread = null;
    private Stream stream = null;
    private Guid mUUID = new Guid("293eb187-b6e9-4434-894b-ef81120f0e5b");
    private List<string> items_bluetooth;
    private int error = 0;
    private Form1 form;
    byte[] messageSend;
    byte[] messageRecu;
    public static Balle b;


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
    /// Methode permettant la connexion au client (tester)*/
    /// </summary>

    public int connectAsClient(String name_device)
    {
       

        error = 0;
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
            this.updateOutputLog("Try to connect to : " + device_selected.DeviceName, 0);
            if (device_selected != null)
            {
                if (pairDevice())
                {
                    this.updateOutputLog("Starting to connect",0);
                    this.updateOutputLog("Starting connecting Thread !!!",0);
                    bluetoothClientThread = new Thread(new ThreadStart(ClientConnectThread));
                    bluetoothClientThread.Start();
                }
                else
                {
                    this.updateOutputLog("Failed to connect",-1);
                    error = -1;
                }
            }
            else
            {
                this.updateOutputLog("Device not found !!! ", -1);
                error = -1;
            }
        }
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
        this.updateOutputLog("Connect",0);
        BluetoothClient client = new BluetoothClient();
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
        client = (BluetoothClient)result.AsyncState;
        try
        {
            client.EndConnect(result);
            stream = client.GetStream();
            stream.ReadTimeout = 1000;
            readingThread = new Thread(new ThreadStart(reading));
            readingThread.Start();
            while (true)
            {
                this.form.setReadyClient(true);
                this.updateOutputLog("Sending_Data",0);
                prepareSendData();
                stream.Write(messageSend, 0, messageSend.Length);
            }
        }
        catch (System.Net.Sockets.SocketException ex )
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
        catch (Exception ex)
        {
            this.error = -1;
            Console.WriteLine(ex.Message);
            this.updateOutputLog(ex.Message,-1);
            this.updateOutputLog("Fail to connect to this serveur",-1);
            if (readingThread != null)
            {
                readingThread.Abort();
            }
            this.updateOutputLog("Fermeture des connexions", -1);
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
    private void reading()
    {
        this.error = -1;
        messageRecu = new byte[1024];

         try
        {
            while (true)
            {
                this.form.setReadyClient(true);
                this.updateOutputLog("Receiving_Data", 0);
                stream.Read(messageRecu, 0, messageRecu.Length);
                this.updateOutputLog(Encoding.ASCII.GetString(messageRecu), 0);

                b = BinaryDeserializeObject(messageRecu);
            }
        }
         catch (Exception ex)
         {
             Console.WriteLine(ex.Message);
         }
    }



    string myPin = "1234";
    public bool pairDevice()
    {

        if (!device_selected.Authenticated)
        {
            if(!BluetoothSecurity.PairRequest(device_selected.DeviceAddress,myPin)){
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
    
    private void prepareSendData()
    {
        int rand = new Random().Next();
        messageSend = Encoding.ASCII.GetBytes("Sending Message" + rand);
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
                return (Balle)formatter.Deserialize(ms);
            }
        }
    }

}
