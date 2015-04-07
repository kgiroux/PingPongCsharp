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
    private List<string> items_bluetooth;
    BluetoothClient client;
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
        BluetoothDeviceInfo device_selected = null;
        foreach(BluetoothDeviceInfo device in devices_found){
            if (name_device == device.DeviceName)
            {
                device_selected = device;
                break;
            }
        }
        this.updateOutputLog("Try to connect to : " + device_selected.DeviceName);

        if (pairDevice())
        {
            this.updateOutputLog("Starting to connect");
            this.updateOutputLog("Starting connecting Thread !!!");
            Thread bluetoothClientThread = new Thread(new ThreadStart(ClientConnectThread));
            bluetoothClientThread.Start();
        }
        else
        {
            this.updateOutputLog("Failed to connect");
        }
    }

    private void ClientConnectThread()
    {
        //BluetoothClient client = 
    }

    public bool pairDevice()
    {
        return true;
    }
    
    /// <summary>
    /// Lancement de la recherche Bluetooth via un thread (Permet de ne pas bloquer le programme)
    /// </summary>
    public void startScanBluetoothDevices()
    {
        Thread bluetoothScanThread = new Thread(new ThreadStart(scan));
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
    }


}
