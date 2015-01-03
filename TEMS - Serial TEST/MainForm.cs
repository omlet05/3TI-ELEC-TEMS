using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections;

namespace TEMS___Serial_TEST
{
    public partial class MainForm : Form
    {
        public static Hashtable clientsList = new Hashtable(); 
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        Thread ClientThread;
        string readData = null, sendData = null;


        public MainForm()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetText("TEMS - about\n");
        }



        private void serialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new SerialForm();
            frm.Show();
            this.Enabled = false;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string ip = IpTextBox.Text;
            int port = Convert.ToInt32(portUpDown.Value);
           
            if (!checkIfCompletedOrNotBlank(ip))
            {
                SetText("IP ou port non valide.\n");
            }
            else
                Client(ip, port);       
        }

        private void Client(string ip, int port) {        
            
            ClientThread = new Thread(new ThreadStart(() => NewClient(ip, port)));
            ClientThread.IsBackground = true;
            ClientThread.Start();
            
        }

        private bool checkIfCompletedOrNotBlank(String ip){
            bool toReturn;
            IPAddress iptest;
            if (string.IsNullOrWhiteSpace(IpTextBox.Text) || string.IsNullOrWhiteSpace(portUpDown.Text) || !(IPAddress.TryParse(ip, out iptest)))
                toReturn = false;
            else
                toReturn = true;
            return toReturn;
        }

        private void NewClient(String ip, int port)
        {

            try
            {
                clientSocket.Connect(ip, port);
                readData = "Conected to TEMS Server ...";
                
                msg();
                Thread ct1Thread = new Thread(setMessage);
                Thread ct2Thread = new Thread(getMessage);
                ct1Thread.IsBackground = true;
                ct2Thread.IsBackground = true;
                ct1Thread.Start();
                ct2Thread.Start();
            }
            catch (Exception ex)
            {
                SetText("Erreur: " + ex.Message + "\n");
            }
        }

        private void setMessage()
        {
            serverStream = clientSocket.GetStream();
            while (true )
            {
                if (sendData != null)
                {
                    
                    byte[] outStream = System.Text.Encoding.ASCII.GetBytes(sendData);
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                }
            }
        }
        
        private void getMessage()
        {
            serverStream = clientSocket.GetStream();
            while (true)
            {
                
                int buffSize = 0;
                byte[] inStream = new byte[10025];
                buffSize = clientSocket.ReceiveBufferSize;
                serverStream.Read(inStream, 0, buffSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                readData = "" + returndata;
                msg();
            }
        }

        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
                SetText(Environment.NewLine+"=> " + readData);
        } 
         
        
        //Add Text to ServerConsoleBox with scrolling to the end. And invoke creation if needed.
        delegate void SetTextCallback(string text);

        private void SetText(string txt)
        {
            if (this.ServerConsoleBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { txt });
            }
            else
            {
                
                    this.ServerConsoleBox.AppendText(txt);
                    this.ServerConsoleBox.ScrollToCaret();

            }
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
    }


}
