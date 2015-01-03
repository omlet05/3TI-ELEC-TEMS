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
using System.Text.RegularExpressions;

namespace TEMS___Serial_TEST
{
    public partial class MainForm : Form
    {
        Thread ClientThread;
        string readData = null, sendData = null;
        int i;


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
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string ip = IpTextBox.Text;
            int port = Convert.ToInt32(portUpDown.Value);
           
            if (!checkIfCompletedOrNotBlank(ip))
                SetText("IP ou port non valide.\n");
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

            System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
            try
            {
                
                clientSocket.Connect(ip, port);
                SetText("Connected to new TEMS Server on"+ip+":"+port+".\n");
                Thread clientThread = new Thread(() => MessageIO(clientSocket));
                clientThread.IsBackground = true;
                clientThread.Start();
                
            }
            catch (Exception ex)
            {
                SetText("\nErreur: " + ex.Message + "\n");
            }
        }

        private void MessageIO(TcpClient clientSocket)
        {
            NetworkStream clientStream = clientSocket.GetStream();
            int buffSize;
            byte[] inStream = new byte[4069];

            while (true)
            {
                buffSize = 0;
                try
                {  
                    if (clientStream.DataAvailable)
                    {
                        buffSize = clientSocket.ReceiveBufferSize;
                        clientStream.Read(inStream, 0, 4069);
                        string returndata = "";
                        returndata = System.Text.Encoding.ASCII.GetString(inStream);
                        readData = "" + returndata;
                        msgsplittcp();
                        inStream = new byte[4069];

                        if (sendData != null)
                        {
                            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(sendData);
                            clientStream.Write(outStream, 0, outStream.Length);
                            clientStream.Flush();
                            sendData = null;
                        }
                    }
                    
                }
                catch(Exception e)
                {
                    SetText("\nErreur: " + e.Message + "\n");
                }
            }
        }
        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
            {
                SetText(Environment.NewLine + ">> " + readData);
            }
        }

        private int msgsplittcp()
        {
            
            if(readData.Contains("TEMS"))
                i = 0;
            
            switch (i)
            {
                case 0:
                    SetText(Environment.NewLine + ">>" + readData + " ");
                    i++;
                    break;
                case 1:
                    SetText(" Current:" + readData + " ");
                    i++;
                    break;
                case 2:

                    SetText(" Max:" + readData + " ");
                    i++;
                    break;

                case 3:
                    SetText(" Min:" + readData + " ");
                    i++;
                    break;
                default:
                    SetText(readData + "\n");
                    break;
            }
            readData = "";
            return i;
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
