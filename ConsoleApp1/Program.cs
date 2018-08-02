using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;
using ConsoleApp1.Common;
using ConsoleApp1.DataModel;

namespace ConsoleApp1
{
    class Program
    {
        //声明 API 函数
        public const uint LVM_FIRST = 0x1000;
        public const uint LVM_GETITEMCOUNT = LVM_FIRST + 4;
        public const uint LVM_GETITEMW = LVM_FIRST + 75;

        [DllImport("user32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.DLL")]
        public static extern IntPtr FindWindow(string lpszClass, string lpszWindow);
        [DllImport("user32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent,
            IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            out uint dwProcessId);

        public const uint PROCESS_VM_OPERATION = 0x0008;
        public const uint PROCESS_VM_READ = 0x0010;
        public const uint PROCESS_VM_WRITE = 0x0020;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess,
            bool bInheritHandle, uint dwProcessId);
        public const uint MEM_COMMIT = 0x1000;
        public const uint MEM_RELEASE = 0x8000;

        public const uint MEM_RESERVE = 0x2000;
        public const uint PAGE_READWRITE = 4;

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
           uint dwSize, uint dwFreeType);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
           IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
           IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

        public struct LVITEM
        {
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            public IntPtr pszText; // string 
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public int cColumns;
            public IntPtr puColumns;
        }
        public static int LVIF_TEXT = 0x0001;
        static void Main(string[] args)
        {

            iAutomationElement uielement = new iAutomationElement();                        
            uielement.enumRoot();
            uielement.FindByName("安信安睿终端");
           
        }

        /// <summary>
        /// 中投证券交易模块自动化Demo
        /// </summary>
        /// <param name="_elemet">模块元素节点</param>
        static void ZTWindwosFindDemo(AutomationElement _elemet)
        {

        }

        /// <summary>
        /// 之前的测试代码垃圾桶
        /// </summary>
        static void zhushi()
        {
            //uielement.enumNode(uielement.node);
            //uielement.FindByClassName("Afx:");
            //Console.WriteLine("--------------------------------------------");
            //uielement.enumNode(uielement.node);
            //AutomationElement ui = uielement.SearchWindowsByName("", "AfxWnd42");
            //if(ui!=null)
            //    uielement.GetViewList(ui,5);



            //AutomationElement ae = AutomationElement.RootElement;
            //StringBuilder sb = new StringBuilder();
            /////获得主窗口ui元素
            //var main = ae.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NativeWindowHandleProperty, Int32.Parse("00AB096A", System.Globalization.NumberStyles.HexNumber)));

            ///// 获得

            //var s2 = main.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NativeWindowHandleProperty, Int32.Parse("00090A70", System.Globalization.NumberStyles.HexNumber)));

            //Console.WriteLine(s2.Current.Name);
            ////var s2 = st.FindAll(TreeScope.Descendants, PropertyCondition.TrueCondition);
            //var s3 = s2.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NativeWindowHandleProperty, Int32.Parse("00010A72", System.Globalization.NumberStyles.HexNumber)));
            //Console.WriteLine(s3.Current.ClassName);
            //int count = (int)SendMessage((IntPtr)s3.Current.NativeWindowHandle, LVM_GETITEMCOUNT, 0, 0);
            //Console.WriteLine(count);
            //uint vProcessId;
            //GetWindowThreadProcessId((IntPtr)s3.Current.NativeWindowHandle, out vProcessId);

            //IntPtr vProcess = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ |
            //    PROCESS_VM_WRITE, false, vProcessId);
            //IntPtr vPointer = VirtualAllocEx(vProcess, IntPtr.Zero, 4096,
            //    MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);

            //for (int i = 0; i < count; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        byte[] vBuffer = new byte[256];
            //        LVITEM[] vItem = new LVITEM[1];
            //        vItem[0].mask = LVIF_TEXT;
            //        vItem[0].iItem = i;
            //        vItem[0].iSubItem = j;
            //        vItem[0].cchTextMax = vBuffer.Length;
            //        vItem[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVITEM)));
            //        uint vNumberOfBytesRead = 0;
            //        WriteProcessMemory(vProcess, vPointer,
            //            Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0),
            //            Marshal.SizeOf(typeof(LVITEM)), ref vNumberOfBytesRead);
            //        SendMessage((IntPtr)s3.Current.NativeWindowHandle, LVM_GETITEMW, i, vPointer.ToInt32());
            //        ReadProcessMemory(vProcess,
            //            (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVITEM))),
            //            Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0),
            //            vBuffer.Length, ref vNumberOfBytesRead);

            //        string vText = Marshal.PtrToStringUni(
            //            Marshal.UnsafeAddrOfPinnedArrayElement(vBuffer, 0));
            //        Console.WriteLine(vText);
            //    }
            //}


            ////var s4 = s3.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NativeWindowHandleProperty, Int32.Parse("000D05A6", System.Globalization.NumberStyles.HexNumber)));
            ////Console.WriteLine(s3.Current.ClassName);
            ////var editor = s4[0];
            //////SendMessage(editor.Current.NativeWindowHandle, 0x0102, (IntPtr)50, (IntPtr)0);

            ////InvokeButton(editor);
            ////Console.WriteLine(editor.Current.ClassName);
            ////ValuePattern value = (ValuePattern)editor.GetCurrentPattern(ValuePattern.Pattern);
            ////value.SetValue("100");
            ////var s3 = s2.FindAll(TreeScope.Children, PropertyCondition.TrueCondition);
            ////for (int i = 0; i < main.Count; i++)
            ////{
            ////    Console.WriteLine(main[i].Current.ClassName +"    "+ Convert.ToString(main[i].Current.NativeWindowHandle, 16));
            ////}
        }

        /// <summary>
        /// /模拟点击
        /// </summary>
        /// <param name="e"></param>
        private static void InvokeButton(AutomationElement e)
        {
            InvokePattern invoke = (InvokePattern)e.GetCurrentPattern(InvokePattern.Pattern);
            invoke.Invoke();
        }
        public static string Character(int asc)
        {
            //asc = asc + 65536;
            Encoding asciiEncoding = Encoding.GetEncoding("GB18030");
            Byte[] chrByte = BitConverter.GetBytes((short)asc);
            string strCharacter = string.Empty;
            if (asc < 0 || asc > 255)
            {
                Byte[] chrByteStr = new byte[2];
                chrByteStr[0] = chrByte[1];
                chrByteStr[1] = chrByte[0];
                strCharacter = asciiEncoding.GetString(chrByteStr);
            }
            else
            {
                Byte[] chrByteStr = new byte[1];
                chrByteStr[0] = chrByte[0];
                strCharacter = asciiEncoding.GetString(chrByteStr);
            }
            return (strCharacter);
        }
    }

    
}
