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
        //public variable.
        Thread ClientThread;
        string readData = null, sendData = null;
        int i;


        public MainForm()
        {
            InitializeComponent();
        }

        //about click event
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {                   
            SetText("\n--------------\n");
            SetText("\n TEMS - MANAGER\n");
            SetText("   Version 1.0\n");
            SetText("   Team:\n");
            SetText("      *Corentin Blanche\n");
            SetText("      *Catherine Arnaud\n");
            SetText("      *Degueldre Kevin\n");
            SetText("      *Lobet Mathieu\n\n");

            SetText("GIT: https://github.com/omlet05/3TI-ELEC-TEMS \n");
            SetText("\n--------------\n");
        }


        //serial form creation from button
        private void serialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new SerialForm();
            frm.Show();
        }

        //trying to connect to client by pressing "add client" button.
        private void button1_Click(object sender, EventArgs e)
        {
            string ip = IpTextBox.Text;
            int port = Convert.ToInt32(portUpDown.Value);
           
            if (!checkIfCompletedOrNotBlank(ip))
                SetText("IP ou port non valide.\n");
            else
                Client(ip, port);       
        }

        //creating the new client thread
        private void Client(string ip, int port) {        
            
            ClientThread = new Thread(new ThreadStart(() => NewClient(ip, port)));
            ClientThread.IsBackground = true;
            ClientThread.Start();
            
        }

        //checking if ip and port in program are good
        // IN: IP to check
        private bool checkIfCompletedOrNotBlank(String ip){
            bool toReturn;
            IPAddress iptest;
            if (string.IsNullOrWhiteSpace(IpTextBox.Text) || string.IsNullOrWhiteSpace(portUpDown.Text) || !(IPAddress.TryParse(ip, out iptest)))
                toReturn = false;
            else
                toReturn = true;
            return toReturn;
        }

        //thread for one client
        // IN: valid IP & PORT
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

        //INPUT OUTPUT from client message
        // IN: valide tcp client OUT: n/a
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
                        //reception of packet from client.
                        buffSize = clientSocket.ReceiveBufferSize;
                        clientStream.Read(inStream, 0, 4069);
                        string returndata = "";
                        returndata = System.Text.Encoding.ASCII.GetString(inStream);
                        readData = "" + returndata;
                        msg();
                        inStream = new byte[4069];

                        if (sendData != null)
                        {
                            //if sendData is not null => send it to client. 
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


        //Take the packet in good order for output in console
        // with TEMS matching in first packet
        private int msg()
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
                //autoscrolling to the bottom of the console
                this.ServerConsoleBox.ScrollToCaret();
            }
        }

        //closing event
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();
        }

        //closing event
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
       
    }


}
