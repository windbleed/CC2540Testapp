using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BleTestMp
{
    public class CppDLL
    {

       public const uint WM_MOUSEMOVE = 0x0200;
       public const uint WM_LBUTTONDOWN = 0x0201;
       public const uint WM_LBUTTONUP = 0x0202;
       public const uint WM_LBUTTONDBLCLK = 0x0203;
       public const uint WM_MouseWheel = 0x020A;
       public const uint WM_Close = 0x10;

       public const uint WM_Command = 0x112;
       public const uint SC_Max = 0xf030;


       [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
       public static extern bool SetForegroundWindow(IntPtr hWnd); //WINAPI 设置当前活动窗体的句柄

        [DllImport("QMSL_MSVC6R.dll", EntryPoint = "QLIB_GetAvailablePhonesPortList", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QLIB_GetAvailablePhonesPortList
            (ref ushort iNumPorts, ref ushort pPortList, ushort iNumIgnorePorts, ref ushort pIgnorePortList);


        [DllImport("QMSL_MSVC6R.dll", EntryPoint = "QLIB_DIAG_NV_READ_F", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QLIB_DIAG_NV_READ_F
            (System.IntPtr hResourceContext, ushort iItemID,  byte[] pItemData, int iLength,ref ushort iStatus);

        [DllImport("QMSL_MSVC6R.dll", EntryPoint = "QLIB_ConnectServerWithWait", CallingConvention = CallingConvention.Cdecl)]
        public static extern  System.IntPtr  QLIB_ConnectServerWithWait
            (uint iComPort, ulong iWait_msorePortList);


        [DllImport("QMSL_MSVC6R.dll", EntryPoint = "QLIB_IsPhoneConnected", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QLIB_IsPhoneConnected
            (System.IntPtr hResourceContext);

        [DllImport("kernel32.dll ")]
        public static extern bool CloseHandle(IntPtr hProcess);


        [DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);


        public enum ShowWindowCommands : int
        {

            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,    //用最近的大小和位置显示，激活
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 10
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpszOp,
            string lpszFile,
            string lpszParams,
            string lpszDir,
            ShowWindowCommands FsShowCmd
            );

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]//这里是从user32里面导入FindWindow函数,下同
        public static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();


        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            uint Msg,            // 消息ID
            IntPtr wParam,         // 参数1
            IntPtr lParam            // 参数2
        );

        [DllImport("kernel32.dll")]
        static extern uint GetTickCount();


        public static void Delay(uint ms)
        {
            uint start = GetTickCount();
            while (GetTickCount() - start < ms)
            {
                Application.DoEvents();
            }
        }


    
    }


     //  result=QLIB_DIAG_NV_READ_F(Handler_Com,2824,SN,12,&status);
 // result=1;

 //   Handler_Com=QLIB_ConnectServerWithWait(com_num,2000);

 //   online_status=QLIB_IsPhoneConnected(Handler_Com);
}
