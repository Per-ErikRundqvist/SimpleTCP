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

namespace TCPServer
{
    public partial class ServerForm : Form
    {
        TcpListener lyssnare;
        TcpClient klient;
        int port = 12345;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                lyssnare = new TcpListener(IPAddress.Any, port);
                lyssnare.Start();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }

            buttonStart.Enabled = false;
            StartaMottagning();
        }

        public async void StartaMottagning ()
        {
            try
            {
                klient = await lyssnare.AcceptTcpClientAsync();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }

            StartReading(klient);
        }

        public async void StartReading (TcpClient k)
        {
            byte[] buffert = new byte[1024];

            int n = 0;
            try
            {
                n = await k.GetStream().ReadAsync(buffert, 0, buffert.Length);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }

            textInbox.AppendText(Encoding.Unicode.GetString(buffert, 0, n));
        }
    }
}
