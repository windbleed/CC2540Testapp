using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace BleTestMp
{
    public class DUT
    {
       public System.IntPtr QLIB_COM;

        private ushort ComNum;
        private string SerialNumber;
        public TxApp TxApp1;
        public bool testFinished=false;

        public DUT()
        {
            TxApp1 = new TxApp();
            TxApp1.InitApp();
        }


        ~DUT()
        {
            CppDLL.CloseHandle(QLIB_COM);

        }

        public string GetSN()
        {
           // throw new System.NotImplementedException();
          

            byte[] SN = new byte[14];
            ushort status = 0;
            ushort result = 0;
            result = CppDLL.QLIB_DIAG_NV_READ_F(QLIB_COM, 2824, SN, 14, ref status);
            string s2 = System.Text.Encoding.Default.GetString(SN);

            SerialNumber = s2;




            byte ConnectStatus;
             ConnectStatus = CppDLL.QLIB_IsPhoneConnected(QLIB_COM);

             if (ConnectStatus == 0)
                 return ("Fail");
             else
                 //    MessageBox.Show("Success Open port");




                 return (s2);
        }

        public void SetSN(string serialNumber)
        {
          //  throw new System.NotImplementedException();
        }

        public void Init_TX_Env()
        {
            TxApp1.StartControl();
        }
        public void closeApp()
        {
            TxApp1.exitApp();
        }


        public void FTM_BLE_TX(int Freq,uint time)
        {
           // throw new System.NotImplementedException();
           
         //   TxApp1.StartControl();
             TxApp1.FocusFreqComb();

             // TxApp1.ResetFreqComb();


            //  TxApp1.SetFreq(Freq);

             TxApp1.endTx();
             TxApp1.SetFreq(Freq);
             TxApp1.StartTx();
            // CppDLL.Delay(time);
          //  TxApp1.endTx();
          //  TxApp1.ResetFreqComb();

   




        }

        public void OpenCom()
        {
           // throw new System.NotImplementedException();
            
           QLIB_COM = CppDLL.QLIB_ConnectServerWithWait((uint)this.ComNum, 2000);
        }

        public void SetComNum(UInt16 ComNumber)
        {
            //throw new System.NotImplementedException();
            this.ComNum = ComNumber;
        }

        public void End_BLE_TX()
        {
          //  throw new System.NotImplementedException();
            TxApp1.endTx();
            TxApp1.endControl();
        }
    }
}
