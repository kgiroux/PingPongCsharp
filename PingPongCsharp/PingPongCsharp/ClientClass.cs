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
    private ListBox list;
    private Form1 form;
    public ClientClass(Form1 form)
	{
        this.form = form;
        items_bluetooth = new List<string>();
	}
    public void connectAsClient()
    {
        System.Console.WriteLine("Hello Client");
        
    }

    public void startScanBluetoothDevices()
    {
        Thread bluetoothScanThread = new Thread(new ThreadStart(scan));
        bluetoothScanThread.Start();
    }
    private void scan()
    {
        this.updateOutputLog("Starting Scanning ...\n");
        this.updateOutputLog("Initializing component ...");
        BluetoothClient client = new BluetoothClient();
        this.updateOutputLog("Done\n");
        this.updateOutputLog("Searching Bluetooth Devices ...\n");
        BluetoothDeviceInfo[] devices = client.DiscoverDevicesInRange();
        foreach (BluetoothDeviceInfo d in devices)
        {
            System.Console.WriteLine(d.DeviceName);
            if (d != null)
            {
                items_bluetooth.Add(d.DeviceName);
            }   
        }
        this.updateOutputLog("Ending of the research\n");
        this.updateOutputLog("Creation of the list");
        this.updateListBox();
        this.updateOutputLog("... Done\n");
        this.updateOutputLog("=========================\n");
        this.devices_found = devices;
    }
    public List<string> getDevicesName()
    {
        return items_bluetooth;
    }

    public void updateListBox()
    {
        this.form.updateDevicesList(this.getDevicesName());
    }

    public void updateOutputLog(String text)
    {
        this.form.updateConsoleLog(text);
    }
}
