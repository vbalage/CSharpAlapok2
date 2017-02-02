using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fuck off!");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double result = Calculate();
            MessageBox.Show(result.ToString());
        }

        private double Calculate()
        {
            double result = 0;
            for (byte i = byte.MinValue; i <= 255; i++)
            {
                result += Math.Sqrt(i);
            }
            return result;
        }
    }
}
