using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace WinForms_uart
{
    public partial class Form1 : Form
    {
        
        internal delegate void SerialDataReceivedEventHandlerDelegate(object sender, SerialDataReceivedEventArgs e);
        delegate void SetTextCallback(string text);



        public Form1()
        {
            InitializeComponent();
            groupBox2.Enabled = false;
            //button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string pname = "COM";
                int BR;
                BR = Convert.ToInt32(textBox1.Text);
                pname = pname + textBox2.Text;


                mySerialPort.PortName = pname;
                mySerialPort.BaudRate = BR;
                //mySerialPort.Close();
                mySerialPort.Open();

                //temp code for readline to work;
                mySerialPort.Handshake = Handshake.None;
                mySerialPort.DtrEnable = true;
                mySerialPort.RtsEnable = true;
                //////

                label3.Text = "";
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;


            }
            catch (IOException ex)
            {
                label3.Text = ex.ToString();
            }
        }


        //accept only numbers for com and baudrate fields
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        
        //allow the serial port to be opened only when there are values at the needed fields
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != ""))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text != "") && (textBox2.Text != ""))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        
        //reading from the serial
        private void mySerialPort_DataRecieved(object sender, SerialDataReceivedEventArgs e)
        {

            
             string r;
             r = mySerialPort.ReadExisting();
             //showing recieved
            this.BeginInvoke(new SetTextCallback(SetText), new object[] { r });
            
            
        }
        private void SetText(string text)
        {
            richTextBox3.Text += text;
        }

        // writing to the port 
        private void button2_Click(object sender, EventArgs e)
        {
            try {
                mySerialPort.Write(richTextBox1.Text);
                richTextBox1.Text = "";
            }
            catch (IOException ex)
            {
                richTextBox1.Text = ex.ToString();
            }

        }

        

        private void Destrustor(object sender, FormClosedEventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                try
                {
                    mySerialPort.Close();
                }
                finally
                {

                }
            }
            
        }

        
    }
}
