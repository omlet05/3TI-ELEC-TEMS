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
        bool relais = true;


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
                MessageBox.Show("No Com available.\n Com features is disabled.");
                cboPorts.Visible = false;
                openPort.Enabled = false;
            }



            //disable buttons
            sendButton.Enabled = false;
            newLibeBut.Enabled = false;
            closePort.Enabled = false;
            cboPorts.Enabled = true;
            newLibeBut.Enabled = false;
        }     

        private void openPort_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(WichCom.Text))
            {
                try
                {
                    // make sure port isn't open	
                    if (!serialPort.IsOpen)
                    {              
                        
                        //open serial port 
                        try
                        {
                            serialPort.Open();
                            readTextBox.Text = serialPort.PortName + " is ready!";
                            // prevent reinitiation with button
                            openPort.Enabled = false;
                            //disable buttons
                            sendButton.Enabled = true;
                            newLibeBut.Enabled = true;
                            closePort.Enabled = true;
                            cboPorts.Enabled = false;
                            newLibeBut.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur durant l'ouverture du port: {0}", ex.Message);
                        }

                    }
                    else
                        readTextBox.Text = "The port is already used.\n";
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No port Selected!");
            }
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
            
            if (relais)
            {
                serialPort.Write("#A*");
                relais = false;
            }
            else
            {
                serialPort.Write("#B*");
                relais = true;
            }
            

        }

        private void cboPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.PortName = Convert.ToString(cboPorts.Text);
            WichCom.Text = Convert.ToString(cboPorts.Text);
        }


        private void closePort_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            sendButton.Enabled = false;
            newLibeBut.Enabled = false;
            openPort.Enabled = true;
            closePort.Enabled = false;
            cboPorts.Enabled = true;
            newLibeBut.Enabled = false;
        }


       
    }
}
