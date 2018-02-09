using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BleTestMp
{
    public class Tester
    {
        public Instrument Inst1;
        public DUT Dut1;
        public string LogFile;
        public double InsertLoss;
        public TestResult TestResult1;

        public Tester()
        {
             Inst1 = new Instrument();
             Dut1 = new DUT();
             TestResult1 = new TestResult();
            
        }

       

        public bool StartTest()
        {
            //throw new System.NotImplementedException();

      
           //1. Get SN number from DUT        

            Dut1.OpenCom();
            TestResult1.SN = Dut1.GetSN();

           //2. Set Spectrum analyzer para
 
     
            Inst1.SetCentreq(2402);
            Inst1.SetBW(1);
            Inst1.SetLev(10);

            
            //3.DUT send TX 

             Dut1.Init_TX_Env();
          
             Dut1.FTM_BLE_TX(2402,5000);

             CppDLL.Delay(1000);

             TestResult1.ChPwr_2402 = Inst1.ReadChPower() + InsertLoss-3;// 3 is delta value between continue tx with burst tx

             Inst1.SetCentreq(2442);
             Inst1.SetBW(1);
             Inst1.SetLev(10);

             Dut1.FTM_BLE_TX(2442, 5000);

             CppDLL.Delay(1000);

             TestResult1.ChPwr_2442 = Inst1.ReadChPower() + InsertLoss - 3;// 3 is delta value between continue tx with burst tx



             Inst1.SetCentreq(2480);
             Inst1.SetBW(1);
             Inst1.SetLev(10);

             Dut1.FTM_BLE_TX(2480, 5000);

             CppDLL.Delay(1000);

             TestResult1.ChPwr_2480 = Inst1.ReadChPower() + InsertLoss - 3;// 3 is delta value between continue tx with burst tx

             Dut1.End_BLE_TX();


            //4. Spectrum Read



             Dut1.testFinished = true;

            TestResult1.TestTime = System.DateTime.Now.ToString();

            if ((TestResult1.ChPwr_2402 < 3.8 && TestResult1.ChPwr_2402 > 0) && (TestResult1.ChPwr_2442 < 3.8 && TestResult1.ChPwr_2442 > 0) &&(TestResult1.ChPwr_2480 < 3.8 && TestResult1.ChPwr_2480 > 0) &&(TestResult1.SN != "Fail"))
                return true;

            else
                return false;




        }

        public void SaveToLog()
        {
           // throw new System.NotImplementedException();

            FileStream aFile = new FileStream(LogFile, FileMode.Append);
            StreamWriter sw = new StreamWriter(aFile);

            sw.WriteLine("");

            sw.Write(TestResult1.SN);
            sw.Write("   ");
            sw.Write(Convert.ToDouble(TestResult1.ChPwr_2402).ToString("0.00"));
            sw.Write("   ");
            sw.Write(Convert.ToDouble(TestResult1.ChPwr_2442).ToString("0.00"));
            sw.Write("   ");
            sw.Write(Convert.ToDouble(TestResult1.ChPwr_2480).ToString("0.00"));

            sw.Write("   ");
            sw.Write(TestResult1.TestTime);
            sw.Close();
        }

        public void InitTest()
        {
           // throw new System.NotImplementedException();

            Inst1.OpenInstrument();
            Inst1.SetCentreq(2440);
            Inst1.SetBW(1);
            Inst1.SetLev(10);
        }

        public void SetIL(double insertLoss)
        {
          //  throw new System.NotImplementedException();

            this.InsertLoss = insertLoss;
        }

        public void SetLog(string FileName)
        {
           // throw new System.NotImplementedException();

            LogFile = FileName;
        }
    }
}
