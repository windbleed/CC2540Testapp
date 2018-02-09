using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using Ivi.Visa.Interop;

namespace BleTestMp
{
    public class Instrument
    {

        private Ivi.Visa.Interop.FormattedIO488 ioDmm;
        private string AddressGPIB;



        // ioDmm = new FormattedIO488Class();


       public Instrument()
        {
            ioDmm = new FormattedIO488Class(); 
        }


        public string ReadDeviceNum()
        {
            throw new System.NotImplementedException();
        }

        public void OpenInstrument()
        {
          //  throw new System.NotImplementedException();

            try
            {
                ResourceManager grm = new ResourceManager();
                ioDmm.IO = (IMessage)grm.Open(AddressGPIB, AccessMode.NO_LOCK, 2000, "");
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Open failed on " + AddressGPIB + " " + ex.Source + "  " + ex.Message, "EZSample", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ioDmm.IO = null;
            }
         
        }

        public void CloseInstrument()
        {
           // throw new System.NotImplementedException();
        }

        public void SetAddressGPIB(string ADDR)
        {
           // throw new System.NotImplementedException();
            AddressGPIB = ADDR;
        }

        public void SetCentreq(int Freq)
        {
            //throw new System.NotImplementedException();
           
            ioDmm.WriteString(":FREQ:CENT " + Convert.ToString(Freq) + " MHz", true);
         
            // ioDmm.WriteString(":BWID 1MHz", true);
        }

        public double ReadChPower()
        {
            //throw new System.NotImplementedException(); 

            double testResult;

            string StrReturn;
        
            ioDmm.WriteString("READ:CHP?", true);
            StrReturn = ioDmm.ReadString();


            string[] PowerList = StrReturn.Split(',');
            testResult = Convert.ToDouble(PowerList[0]);
            double Den_sity = Convert.ToDouble(PowerList[1]);

         
            
            return testResult;
        }

        public void SetBW(int RBW)
        {
           // throw new System.NotImplementedException();

          //  ioDmm.WriteString(":FREQ:CENT " + FreqValue.Text + " GHz", true);
            ioDmm.WriteString(":BWID "+Convert.ToString(RBW)+"MHz", true);
           // DISP:WIND:TRAC:Y:RLEV 10dBm
        }
        public void SetLev(int level)
        {
       // DISP:WIND:TRAC:Y:RLEV 10dBm

            ioDmm.WriteString("DISP:WIND:TRAC:Y:RLEV " + Convert.ToString(level) + "dBm", true);
        }
    }
}
