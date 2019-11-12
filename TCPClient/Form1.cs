using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCPClient
{
    public partial class KlientForm : Form
    {
        TcpClient klient = new TcpClient();
        int port = 12345;

        public KlientForm()
        {
            InitializeComponent();
            klient.NoDelay = true;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {

            StartSending("Hi!");
           
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (!klient.Connected)
            {
                StartConnection();
            }
        }

        public async void StartConnection ()
        {
            try
            {
                IPAddress adress = IPAddress.Parse(textIPAdress.Text);
                await klient.ConnectAsync(adress, port);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }

            buttonSend.Enabled = true;
            buttonConnect.Enabled = false;
        }

        public async void StartSending (string message)
        {
            byte[] utData = Encoding.Unicode.GetBytes(message);

            try
            {
                await klient.GetStream().WriteAsync(utData, 0, utData.Length);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }
        }
    }
}
