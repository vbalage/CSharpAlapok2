using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPIPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            if (d.ShowDialog() == DialogResult.OK)
                textBox1.Text = d.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fs = File.OpenRead(textBox1.Text))
            using (var client = new TcpClient(textBox2.Text, 8088))
            using (var ns = client.GetStream())
            {
                fs.CopyTo(ns);
            }            
        }
    }
}
