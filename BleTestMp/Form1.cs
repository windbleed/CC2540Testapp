using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Diagnostics;


namespace BleTestMp
{
    public partial class Form1 : Form
    {

      // System.IntPtr QLIB_COM;
       private Tester Test1;
       private TestSystem TS1;
        


        public Form1()
        {
            InitializeComponent();
            Test1 = new Tester();
            TS1=new TestSystem();


            comboBox1.Text = TS1.ComNum;
            textBox1.Text = TS1.InstruList;
            textBox3.Text = Convert.ToString(TS1.CableLoss);
            textBox2.Text = TS1.LogFile;
            textBox4.Text = TS1.USB_CC_ID;



        }


        ~Form1()
        {

            Dispose(true);
           


         
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {

            TestSystem TS1=new TestSystem();

            string[] ComList1=new string[10];

            int  ListLength=0;

            TS1.GetComList(ref ComList1,ref ListLength);
            comboBox1.Items.Clear();
           

            for(int i=0;i<ListLength;i++)
            {
                comboBox1.Items.Add(ComList1[i]);
            }
           
        }

  
        private void button3_Click(object sender, EventArgs e)
        {



              
            //     CppDLL.SetForegroundWindow();
               
            
                  label4.Visible = false;
                 this.timer1.Enabled = false;
        


          //       SN_Number = Test1.Dut1.GetSN();

            bool testOK;

            testOK = Test1.StartTest();

            IntPtr hwnd=CppDLL.FindWindow(null, "LT602 BLE Test");
            CppDLL.SetForegroundWindow(hwnd);


       //Show logs
                

                     richTextBox1.AppendText("Begin SN Reading..." + "\n");



                     richTextBox1.AppendText("SN Number is " + Test1.TestResult1.SN + "\n");
                     richTextBox1.AppendText(" channel Power is " + Test1.TestResult1.ChPwr_2402 + ", " + Test1.TestResult1.ChPwr_2442 + ", " + Test1.TestResult1.ChPwr_2480 + "\n");
                     richTextBox1.AppendText("Test time finished at " + Test1.TestResult1.TestTime + "\n");


             if(testOK)
            {



                label4.Text = "PASS";
                label4.ForeColor=Color.FromName("Green");
                this.timer1.Enabled = true;
                Test1.SaveToLog();

            }
            else
            {

            
                 label4.Text="Fail";
                 label4.ForeColor=Color.FromName("RED");
                 this.timer1.Enabled = true;
            }

             CppDLL.Delay(5000);
            label4.Visible = true;


             timer2.Enabled = true;

           //  Test1.TestResult1.SN = null;
               


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string temp = comboBox1.Text.Replace("COM", "");
            int com = Convert.ToUInt16(temp);
            Test1.Dut1.SetComNum((ushort)com);
            
            Test1.Inst1.SetAddressGPIB(textBox1.Text);

            Test1.SetIL(Convert.ToDouble(textBox3.Text));

            Test1.SetLog(textBox2.Text);

            Test1.InitTest();

            Test1.Dut1.TxApp1.Set_CC_ID(textBox4.Text);

            Test1.Dut1.TxApp1.cc_control_x = TS1.cc_control_x;
            Test1.Dut1.TxApp1.cc_control_y = TS1.cc_control_y;
            Test1.Dut1.TxApp1.continue_tx_x = TS1.continue_tx_x;
            Test1.Dut1.TxApp1.continue_tx_y = TS1.continue_tx_y;
            Test1.Dut1.TxApp1.tx_start_x = TS1.tx_start_x;
            Test1.Dut1.TxApp1.tx_start_y = TS1.tx_start_y;
            Test1.Dut1.TxApp1.tx_stop_x = TS1.tx_stop_x;
            Test1.Dut1.TxApp1.tx_stop_y = TS1.tx_stop_y;
            Test1.Dut1.TxApp1.freq_combo_x = TS1.freq_combo_x;
            Test1.Dut1.TxApp1.freq_combo_y = TS1.freq_combo_y;
            Test1.Dut1.TxApp1.combo_line_x = TS1.combo_line_x;
            Test1.Dut1.TxApp1.combo_line_y = TS1.combo_line_y;

                


// write to TestSystem enviroment and save it

            

            TS1.ComNum = comboBox1.Text;
            TS1.InstruList = textBox1.Text;
            TS1.CableLoss = Convert.ToDouble(textBox3.Text);
            TS1.LogFile = textBox2.Text;
            TS1.USB_CC_ID = textBox4.Text;


            TS1.SaveEnvironment();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label4.Visible == true)
                label4.Visible = false;
            else label4.Visible = true;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
       
              timer2.Enabled = true;
          

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            
            if (Test1.Dut1.testFinished)
            {

                Test1.Dut1.OpenCom();

                string sn = Test1.Dut1.GetSN();
                //string sn = "123456789012";

                if (sn == "Fail")
                {
                    Test1.Dut1.testFinished = false;
                    return;

                }
                else if (sn == Test1.TestResult1.SN)
                {
                    return;
                }

            }
             
            

            Test1.Dut1.OpenCom();
            string sn_temp = Test1.Dut1.GetSN();
           // string sn_temp = "123456789012";
        
            if (sn_temp == "Fail")

                timer2.Enabled = true;

        
            
            else
            {
                timer2.Enabled = false;
                button3_Click(this,null);

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Application.Exit();
        }
         

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose(false);
         
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
 
        }

     


    }
}
