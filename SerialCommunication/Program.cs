using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialCommunication
{
    class Program
    {
        static SerialPort sp = new SerialPort("COM19");

        static void Main(string[] args)
        {
            var sps = SerialPort.GetPortNames();

            sp.StopBits = StopBits.One;
            sp.Parity = Parity.None;
            sp.Handshake = Handshake.None;
            sp.DataBits = 8;
            sp.DtrEnable = false;
            sp.RtsEnable = false;
            sp.BaudRate = 38400;
            sp.NewLine = ((char)13).ToString(); //cr
            sp.DataReceived += Sp_DataReceived;
            //sp.Encoding 
            sp.Open();
            Console.WriteLine("Port state: " + sp.IsOpen);
            Console.ReadLine();
            sp.WriteLine("M");
            sp.Close(); // finally blockban
        }

        private static void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            sp.ReadExisting();
        }
    }
}
