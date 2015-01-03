using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.Net;

namespace TEMS___Serial_TEST
{
    public partial class SerialForm : Form
    {
        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        delegate void SetTextCallback(string text);
        private static string TxString = string.Empty;
        string RxString = string.Empty;
        string localIP = string.Empty;



        public SerialForm()
        {
            InitializeComponent();
            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPortRead_DataReceived);
        }

        


        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                string[] ArrayComPortsNames = null;
                int index = -1;
                string ComPortName = null;

                ArrayComPortsNames = SerialPort.GetPortNames();

                Console.WriteLine(index);
                do
                {
                    index += 1;
                    cboPorts.Items.Add(ArrayComPortsNames[index]);
                } while (!((ArrayComPortsNames[index] == ComPortName) || (index == ArrayComPortsNames.GetUpperBound(0))));



                if (index == ArrayComPortsNames.GetUpperBound(0))
                {
                    ComPortName = ArrayComPortsNames[0];
                    Array.Sort(ArrayComPortsNames);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Pas de COM disponible.\n Communication série désactivée.");
                cboPorts.Visible = false;
                openPort.Enabled = false;
            }



            //disable buttons
            sendButton.Enabled = false;
            newLibeBut.Enabled = false;
            closePort.Enabled = false;
            cboPorts.Enabled = true;

            WichCom.Text = serialPort.PortName.ToString();
        }     

        private void openPort_Click(object sender, EventArgs e)
        {

            try
            {
                // make sure port isn't open	
                if (!serialPort.IsOpen)// && !serialPortRead.IsOpen)
                {

                    // set status
                    readTextBox.Text = serialPort.PortName + " est prêt!";
                    //open serial port 
                    try {
                        serialPort.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur durant l'ouverture du port: {0}", ex.Message);
                    }


                    // prevent reinitiation with button
                    openPort.Enabled = false;
                    //disable buttons
                    sendButton.Enabled = true;
                    newLibeBut.Enabled = true;
                    closePort.Enabled = true;
                    cboPorts.Enabled = false;
                }
                else
                    readTextBox.Text = "Le port n'est pas ouvert.";
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // close ports
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        //datareceived handler
        private void serialPortRead_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                RxString = serialPort.ReadExisting();
                if (RxString != String.Empty)
                {
                    this.BeginInvoke(new SetTextCallback(SetText), new object[] { RxString });
                }
            }
            catch (System.TimeoutException) {
                MessageBox.Show("Timeout !");
            }
        }
        private void SetText(string text)
        {
            this.readTextBox.Text += text+"\n";
        }
        // Read/Update to textbox
        private void DisplayText(object s, EventArgs e)
        {
            readTextBox.AppendText(RxString+"\n");
        }

        // send message over serial COM
        private void sendButton_Click(object sender, EventArgs e)
        {
            TxString = writeTextBox.Text;
            serialPort.Write(TxString);
        }

        private void writeTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TxString = writeTextBox.Text;
            serialPort.Write(TxString);
        }



        private void clearButton_Click(object sender, EventArgs e)
        {
            readTextBox.Text = String.Empty;
            writeTextBox.Text = String.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort.Write("\n");
        }

        private void writeTextBox_TextChanged(object sender, EventArgs e)
        {

        }



        private void getHostIP()
        {
            System.Net.IPHostEntry _IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress _IPAddress in _IPHostEntry.AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                    localIP = _IPAddress.ToString();
            }
        }

        private void cboPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.PortName = Convert.ToString(cboPorts.Text);
            WichCom.Text = Convert.ToString(cboPorts.Text);
        }

        private void WichCom_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void closePort_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            sendButton.Enabled = false;
            newLibeBut.Enabled = false;
            openPort.Enabled = true;
            closePort.Enabled = false;
            cboPorts.Enabled = true;
        }

        private void listenBouttonClick(object sender, EventArgs e)
        {
            try{
                getHostIP();
            readTextBox.AppendText("IP du serveur: "+localIP+".\n");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void readTextBox_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
