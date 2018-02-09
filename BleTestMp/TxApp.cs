using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BleTestMp
{
    public class TxApp
    {
        private IntPtr MainHwd;
        private IntPtr ControlHwd;
        private IntPtr FreqCombHwd;

        private int CurrentFreq=2402;
        private int CurrentTxStatus=0;
        private string USB_ID_CC="3473";

        public int current_screen_x_pixels = 800;
        public int current_screen_y_pixels = 600;

        public const int screen_x_base = 1366, screen_y_base = 768;

        //int x = 204;1366
        //int y = 413;768
        // open cc_control bar on main window
       
        public  int cc_control_x=204, cc_control_y=413;

       // float cc_control_x_div = 1366 / 204;
        
         //  x = 68;
         //  y = 187;
        //   open tx continue tab on control window;
        public   int continue_tx_x=68, continue_tx_y=187;
       // int x = 1201;
       // int y = 663;

        // start TX button on control window;

        public  int tx_start_x = 1201, tx_start_y = 663;

       // int x = 1299;
       //  int y = 665;
       // stop tx button on control window;

        public  int tx_stop_x = 1299, tx_stop_y = 665;


        // x = 482;
        //    y = 122;
        // freq combo select button on control window;

        public  int freq_combo_x = 482, freq_combo_y = 122;


        //int x = 462;
       // int y = 134;

        public  int combo_line_x = 462, combo_line_y = 134;







        public void Set_CC_ID(string ID)
        {
            USB_ID_CC = ID;
        }
            
         ~TxApp()
        {
            //throw new System.NotImplementedException();

             // CppDLL.CloseHandle(FreqCombHwd);
             // CppDLL.CloseHandle(ControlHwd);
             // CppDLL.CloseHandle(MainHwd);

              CppDLL.PostMessage(MainHwd, CppDLL.WM_Close, IntPtr.Zero, IntPtr.Zero);

        }

        public void InitApp()
        {
          //  throw new System.NotImplementedException();

            CppDLL.ShowWindowCommands x_mode = CppDLL.ShowWindowCommands.SW_SHOWNORMAL;
            string app = @"C:\Program Files\Texas Instruments\SmartRF Tools\SmartRF Studio 7\bin\startup_window.exe";
            string app_path = @"C:\Program Files\Texas Instruments\SmartRF Tools\SmartRF Studio 7\bin\";
            //run app         
            CppDLL.ShellExecute(IntPtr.Zero, "open", app, null, app_path, x_mode);
            CppDLL.Delay(1000);


            IntPtr hwnd = CppDLL.FindWindow(null, "SmartRF Studio 7 - Texas Instruments");
            MainHwd = hwnd;


        }

        public int get_current_x(int x_pixel)
        {
            float result = (float)x_pixel * (float)current_screen_x_pixels / (float)screen_x_base;
            return ((int)x_pixel);
        }

        public int get_current_y(int y_pixel)
        {
            float result = (float)y_pixel * (float)current_screen_y_pixels / (float)screen_y_base;
            return ((int)y_pixel);
        }


        public void exitApp()
        {
            CppDLL.PostMessage(MainHwd, CppDLL.WM_Close, IntPtr.Zero, IntPtr.Zero);
        }
        public void FocusFreqComb()
        {
           // throw new System.NotImplementedException();
           int x =get_current_x(freq_combo_x);//int((float) freq_combo_x*(float)(float)current_screen_x_pixels/(float)screen_x_base);
           int y =get_current_y(freq_combo_y);

            IntPtr lParam = (IntPtr)((y << 16) | x); // The coordinates 
            IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 





           CppDLL. PostMessage(ControlHwd,CppDLL. WM_LBUTTONDOWN, wParam, lParam);
           CppDLL. PostMessage(ControlHwd,CppDLL. WM_LBUTTONUP, wParam, lParam);

            CppDLL.Delay(1000);

            //x = 482;
            //y = 122;

            //lParam = (IntPtr)((y << 16) | x); // The coordinates 
            //wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 





           // CppDLL.PostMessage(ControlHwd,CppDLL. WM_LBUTTONDOWN, wParam, lParam);
           // CppDLL.PostMessage(ControlHwd, CppDLL.WM_LBUTTONUP, wParam, lParam);












            IntPtr control_window, freq_window, second_hw, third_hw;

            control_window = ControlHwd;
            freq_window = CppDLL.FindWindowEx(control_window, IntPtr.Zero, null, "");
            second_hw = CppDLL.FindWindowEx(control_window, freq_window, null, "");
            third_hw = CppDLL.FindWindowEx(control_window, second_hw, null, "");


            freq_window = CppDLL.FindWindowEx(third_hw, IntPtr.Zero, null, "qt_scrollarea_viewport");
            second_hw =CppDLL. FindWindowEx(freq_window, IntPtr.Zero, null, "centralWidgetMain");

            freq_window = CppDLL.FindWindowEx(second_hw, IntPtr.Zero, null, "");

            third_hw = CppDLL.FindWindowEx(freq_window, IntPtr.Zero, null, "RFParameters");
            second_hw = third_hw;

            third_hw = IntPtr.Zero;

            for (int count = 0; count < 9; count++)
            {

                freq_window = CppDLL.FindWindowEx(second_hw, third_hw, "QWidget", null);//RF Parameters
                third_hw = freq_window;
            }


            FreqCombHwd = freq_window;

         //   x = 463;
          //  y = 115;

         //   lParam = (IntPtr)((y << 16) | x); // The coordinates 
        //    wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 





         //   CppDLL.PostMessage(ControlHwd, CppDLL.WM_LBUTTONDOWN, wParam, lParam);
         //   CppDLL.PostMessage(ControlHwd, CppDLL.WM_LBUTTONUP, wParam, lParam);


        }

        public void SetFreq(int freq)
        {
          //  throw new System.NotImplementedException();

            CppDLL.Delay(100);
            int deltafreq = (freq - CurrentFreq) / 2;


            if (deltafreq>0)

            for (int count = 0; count < (deltafreq); count++)
            {
              int  x = get_current_x(combo_line_x);
              int  y = get_current_y(combo_line_y);

               IntPtr wParam = (IntPtr)(  (-120<<16)|0x00);
               IntPtr lParam = (IntPtr)((y << 16) | x);


                CppDLL.PostMessage(FreqCombHwd, CppDLL.WM_MouseWheel, wParam, lParam);
                CppDLL.Delay(100);
                

            }


            else
                for (int count = 0; count < (-1*deltafreq); count++)
                {
                    int x = get_current_x(combo_line_x);
                    int y = get_current_y(combo_line_y);

                    IntPtr wParam = (IntPtr)((120 << 16) | 0x00);
                    IntPtr lParam = (IntPtr)((y << 16) | x);


                    CppDLL.PostMessage(FreqCombHwd, CppDLL.WM_MouseWheel, wParam, lParam);
                    CppDLL.Delay(100);


                }

            CurrentFreq = freq;

        }

        public void StartControl()
        {
           // throw new System.NotImplementedException();

            CppDLL.Delay(2000);

            //Set_CC_ID(CC_ID);

            IntPtr hwnd = MainHwd;

            int x = get_current_x(cc_control_x);
            int y = get_current_y(cc_control_y);

            IntPtr lParam = (IntPtr)((y << 16) | x); // The coordinates 
            IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 





           CppDLL.PostMessage(hwnd, CppDLL.WM_LBUTTONDOWN, wParam, lParam);
           CppDLL.PostMessage(hwnd, CppDLL.WM_LBUTTONUP, wParam, lParam);

           CppDLL.PostMessage(hwnd, CppDLL.WM_LBUTTONDBLCLK, wParam, lParam);
           CppDLL.PostMessage(hwnd, CppDLL.WM_LBUTTONUP, wParam, lParam);


           CppDLL.Delay(6000);


           hwnd = CppDLL.FindWindow(null, USB_ID_CC+" - CC2540 - Device Control Panel");
           ControlHwd = hwnd;

           x = get_current_x(continue_tx_x);
           y = get_current_y(continue_tx_y);

           lParam = (IntPtr)((y << 16) | x); // The coordinates 
           wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 





           CppDLL.PostMessage(hwnd, CppDLL.WM_LBUTTONDOWN, wParam, lParam);
           CppDLL.PostMessage(hwnd, CppDLL.WM_LBUTTONUP, wParam, lParam);
          //maxim control window
           CppDLL.PostMessage(ControlHwd, CppDLL.WM_Command, (IntPtr)0xf030, (IntPtr)0);


        }

        public void StartTx()
        {
           // throw new System.NotImplementedException();


            if (CurrentTxStatus != 0)
            {
                endTx();
                CppDLL.Delay(100);
            }
           
          int  x = get_current_x(tx_start_x);
          int  y = get_current_y(tx_start_y);

           IntPtr lParam = (IntPtr)((y << 16) | x); // The coordinates 
           IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 





            CppDLL.PostMessage(ControlHwd, CppDLL.WM_LBUTTONDOWN, wParam, lParam);
            CppDLL.PostMessage(ControlHwd, CppDLL.WM_LBUTTONUP, wParam, lParam);
            CurrentTxStatus = 1;
        }

        public void endTx()
        {
           // throw new System.NotImplementedException();

         int   x = get_current_x(tx_stop_x);
         int   y = get_current_y(tx_stop_y);

            IntPtr lParam = (IntPtr)((y << 16) | x); // The coordinates 
            IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 





            CppDLL.PostMessage(ControlHwd, CppDLL.WM_LBUTTONDOWN, wParam, lParam);
            CppDLL.PostMessage(ControlHwd, CppDLL.WM_LBUTTONUP, wParam, lParam);
        }

        public void ResetFreqComb()
        {
            //throw new System.NotImplementedException();
            endTx();

            for (int count = 0; count < ((CurrentFreq - 2402) / 2); count++)
            {
                int x = get_current_x(combo_line_x);
                int y = get_current_y(combo_line_y);

                IntPtr wParam = (IntPtr)( 120<<16);
                IntPtr lParam = (IntPtr)((y << 16) | x);


                CppDLL.PostMessage(FreqCombHwd, CppDLL.WM_MouseWheel, wParam, lParam);
                CppDLL.Delay(100);

                

            }
          //  CurrentFreq = 2402;
        }

        public void endControl()
        {
            //throw new System.NotImplementedException();
            CppDLL.PostMessage(ControlHwd, CppDLL.WM_Close, IntPtr.Zero, IntPtr.Zero);
           // CppDLL.PostMessage(ControlHwd, CppDLL.WM_Command, (IntPtr)0xf030, (IntPtr)0);
            
            
        }
    }
}
