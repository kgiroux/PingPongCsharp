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
        public Form1()
        {
            InitializeComponent();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serveur.Enabled = true;
            client.Enabled = true;
            textBox1.Text = "";
        }
    }
}