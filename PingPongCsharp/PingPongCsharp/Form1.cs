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
        public Form1()
        {
            InitializeComponent();
            client.Enabled = false;
            item = new List<string>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (name.Equals("") || name.Equals(null))
            {
                name = "AnonymeServeur";
            }
            System.Console.WriteLine(name);
            client.Enabled = false;
            ServerClass srv = new ServerClass();
            srv.connectAsServer();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (name.Equals("") || name.Equals(null))
            {
                name = "AnonymeClient";
            }
            System.Console.WriteLine(name);
            serveur.Enabled = false;
            
            clt.connectAsClient();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            serveur.Enabled = true;
            client.Enabled = true;
            textBox1.Text = "";
        }

        private void scan_button_Click(object sender, EventArgs e)
        {
            client.Enabled = false;
            clt = new ClientClass(this);
            clt.startScanBluetoothDevices();
        }

        public void updateDevicesList(List<string> item_return)
        {
            item = null;
            item = new List<string>();
            item = item_return;
            Func<int> del = delegate()
            {
                listBox1.DataSource = null;
                listBox1.DataSource = item;
                return 0;
            };
            Invoke(del);
        }


        public void updateConsoleLog(string text)
        {

            Func<int> del = delegate()
            {
                outPutLog.AppendText(text);
                return 0;
            };
            Invoke(del);
        }
    }
}