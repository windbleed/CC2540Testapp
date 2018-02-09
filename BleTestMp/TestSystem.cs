using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BleTestMp
{
    public class TestSystem
    {
        public              string          ComNum;
        public              string          InstruList;
        public              double          CableLoss;
        public              string          USB_CC_ID;
        public              string          LogFile;
                
        public              string          ConfigFile = "config.ini";
        public              int              cc_control_x;
        public              int              cc_control_y;
        public              int              continue_tx_x ;
        public              int              continue_tx_y ;

        public int tx_start_x;
        public int tx_start_y;
        public int tx_stop_x;
        public int tx_stop_y;
        public int freq_combo_x;
        public int freq_combo_y;
        public int combo_line_x;
        public int combo_line_y;


          public TestSystem()
          {

           
              StreamReader sr =   new StreamReader(ConfigFile,Encoding.Default);

              ComNum = sr.ReadLine();
              InstruList = sr.ReadLine();
              CableLoss = Convert.ToDouble(sr.ReadLine());
              USB_CC_ID = sr.ReadLine();
              LogFile = sr.ReadLine();
              cc_control_x = Convert.ToInt32(sr.ReadLine());
              cc_control_y = Convert.ToInt32(sr.ReadLine());
              continue_tx_x = Convert.ToInt32(sr.ReadLine());
              continue_tx_y = Convert.ToInt32(sr.ReadLine());
              tx_start_x = Convert.ToInt32(sr.ReadLine());
              tx_start_y = Convert.ToInt32(sr.ReadLine());
              tx_stop_x = Convert.ToInt32(sr.ReadLine());
              tx_stop_y = Convert.ToInt32(sr.ReadLine());
              freq_combo_x = Convert.ToInt32(sr.ReadLine());
              freq_combo_y = Convert.ToInt32(sr.ReadLine());
              combo_line_x = Convert.ToInt32(sr.ReadLine());
              combo_line_y = Convert.ToInt32(sr.ReadLine());

              sr.Close();





          }

        public void SaveEnvironment()
          {

              FileStream aFile = new FileStream(ConfigFile, FileMode.Create);
              StreamWriter sw = new StreamWriter(aFile);

              sw.WriteLine(ComNum);

              sw.WriteLine(InstruList);
              sw.WriteLine(Convert.ToDouble(CableLoss));
              sw.WriteLine(USB_CC_ID);
              sw.WriteLine(LogFile);
              //sw.WriteLine()
              sw.WriteLine(cc_control_x);
              sw.WriteLine(cc_control_y);
              sw.WriteLine(continue_tx_x);
              sw.WriteLine(continue_tx_y);
              sw.WriteLine(tx_start_x);
              sw.WriteLine(tx_start_y);
              sw.WriteLine(tx_stop_x);
              sw.WriteLine(tx_stop_y);
              sw.WriteLine(freq_combo_x);
              sw.WriteLine(freq_combo_y);
              sw.WriteLine(combo_line_x);
              sw.WriteLine(combo_line_y);
         
              sw.Close();


          }



        public void GetComList(ref string[] ComList, ref int ComLength)
        {
           // throw new System.NotImplementedException();

   



            ushort NumPorts, NumIgnorePorts;
            ushort[] PortList = new ushort[10];
            ushort[] IgnorePortList = new ushort[2];

            IgnorePortList[0] = 1;
            IgnorePortList[1] = 2;

            NumIgnorePorts = 2;
            NumPorts = 5;

          //  CppDLL.QLIB_GetAvailablePhonesPortList()
            if (CppDLL.QLIB_GetAvailablePhonesPortList(ref NumPorts, ref PortList[0], NumIgnorePorts, ref IgnorePortList[0]) != 0)
            {
                if (NumPorts > 0)
                {
                    ComLength = NumPorts;
                    for (int i = 0; i < NumPorts; i++)
                        ComList[i] ="COM"+Convert.ToString( (int)PortList[i]);


                }
               
                
            }

      


        }

        public void GetInstList()
        {
          //  throw new System.NotImplementedException();
        }
    }
}
