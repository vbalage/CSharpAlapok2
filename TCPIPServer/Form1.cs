using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPIPServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run((() => Listen()));
        }

        private void Listen()
        {
            IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[1];
            TcpListener listener = new TcpListener(ipAddress, 8088);
            listener.Start();

            while (true)
            {
                Socket handler = listener.AcceptSocket();
                if (handler.Connected)
                {
                    Task.Run(() => ProcessData(handler)).ContinueWith(_=> handler.Close());
                }
            }
        }

        private void ProcessData(Socket socket)
        {
            string filename = Guid.NewGuid() + ".txt";

            using (NetworkStream ns = new NetworkStream(socket))
            {
                
                int read = -1;
                int blockSize = 512;
                byte[] buffer = new byte[blockSize];

                using (var fs = File.OpenWrite(filename))
                {
                    while (true)
                    {
                        read = ns.Read(buffer, 0, blockSize);
                        if (read == 0) break;
                        
                        fs.Write(buffer, 0, read);
                    }
                }
            }

            Action a = () => listView1.Items.Add(filename + " received");
            listView1.Invoke(a);
        }
    }


}
