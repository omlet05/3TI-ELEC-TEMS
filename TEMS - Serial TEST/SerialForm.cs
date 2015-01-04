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
        //public variables declaration.
        //delegate is for SERIAL RX.
        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        delegate void SetTextCallback(string text);
        private static string TxString = string.Empty;
        string RxString = string.Empty;
        bool relais = true;

        //initialize form & new event for reception.
        public SerialForm()
        {
            InitializeComponent();
            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPortRead_DataReceived);
        }

        

        // load FORM1 & populate COM list with available one.
        // if no COM detected feature will be disabled (in catch).
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

        //event for openport button clicking
        //will open connection if COM selected in the list
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
                            MessageBox.Show("Error when opening the " + serialPort.PortName + " port: " + ex.Message);
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

        //thread to receive data invoke the callback to print data in the console
        private void serialPortRead_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                RxString = serialPort.ReadExisting();
                if (RxString != String.Empty)
                {
                    this.BeginInvoke(new SetTextCallback(SetText), new object[] { "\n"+RxString });
                }
            }
            catch (System.TimeoutException) {
                MessageBox.Show("Timeout !");
                serialPort.Close();
                sendButton.Enabled = false;
                newLibeBut.Enabled = false;
                openPort.Enabled = true;
                closePort.Enabled = false;
                cboPorts.Enabled = true;
                newLibeBut.Enabled = false;
            }
        }

        //set text in console
        // IN: string
        private void SetText(string text)
        {
            this.readTextBox.Text += text+"\n";
        }


        //Send message over COM by pressing button.
        private void sendButton_Click(object sender, EventArgs e)
        {
            TxString = writeTextBox.Text;
            serialPort.Write(TxString);
        }


        //flush console & command prompt
        private void clearButton_Click(object sender, EventArgs e)
        {
            readTextBox.Text = String.Empty;
            writeTextBox.Text = String.Empty;
        }

        //switching relay by #A* & #b* send.
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

        //Event when a COM is selected in the list.
        private void cboPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.PortName = Convert.ToString(cboPorts.Text);
            WichCom.Text = Convert.ToString(cboPorts.Text);
        }


        //Event when closing the port
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
