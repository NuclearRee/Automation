using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;
using ConsoleApp1.Common;
using ConsoleApp1.DataModel;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        //大智慧预警数据
        static Dictionary<string, DataItem> DZH_DataList = new Dictionary<string, DataItem>();
        //大智慧预警数据DataListView AutomationElementObj
        static AutomationElement DZH_uiElement;
        //中投证券买入按钮 AutomationElementObj
        static AutomationElement ZT_BuyButtonElement;
        //中投证券买入下单 证券代码框 AutomationElementObj
        static AutomationElement ZT_BuySecuritiesCode;
        //中投证券买入 买入数量 AutomationElementObj
        static AutomationElement ZT_BuyNum;
        //中投证券买入 持仓单DataListView AutomationElementObj
        static AutomationElement ZT_BuyListView;
        //中投证券买入下单按钮 AutomationElementObj
        static AutomationElement ZT_BuyOrder;
        //中投证券买入确认 AutomationElementObj
        static AutomationElement ZT_BuyConfirm;

        //中投证券卖出按钮 AutomationElementObj
        static AutomationElement ZT_SaleButtonElement;
        //中投证券卖出下单 证券代码框 AutomationElementObj
        static AutomationElement ZT_SaleSecuritiesCode;
        //中投证券卖出 卖出数量 AutomationElementObj
        static AutomationElement ZT_SaleNum;
        //中投证券卖出 持仓单DataListView AutomationElementObj
        static AutomationElement ZT_SaleListView;
        //中投证券卖出下单按钮 AutomationElementObj
        static AutomationElement ZT_SaleOrder;
        //中投证券卖出确认 AutomationElementObj
        static AutomationElement ZT_SaleConfirm;

        //中投证券持仓单数据
        static Dictionary<string, DataItem> ZT_DataList = new Dictionary<string, DataItem>();

        static AutomationElement buyWindowsElement;
        static void Main(string[] args)
        {


            //对象初始化
            initialization();
            //ReadWarmingOrder()
            SaleOrder("600428", "100");


        }

        static  void ReadWarmingOrder()
        {
            while (true)
            {
                DZH_DataList.Clear();
                var data = new iAutomationElement();
                DZH_DataList = data.GetViewList(DZH_uiElement, 5);
                foreach(var item in DZH_DataList)
                {
                    
                }
            }

        }
        /// <summary>
        /// 股票买入
        /// </summary>
        /// <param name="_securitiesCode">证券代码</param>
        /// <param name="_num">数量</param>
        static void BuyOrder(string _securitiesCode,string _num)
        {
            var orderClick = new iAutomationElement();
            orderClick.InvokeButton(ZT_BuyButtonElement);
            orderClick.WriteTextBox(ZT_BuySecuritiesCode, "\b\b\b\b\b\b");
            orderClick.WriteTextBox(ZT_BuySecuritiesCode, _securitiesCode);
            orderClick.WriteTextBox(ZT_BuyNum, "\b\b\b\b\b\b");
            orderClick.WriteTextBox(ZT_BuyNum, _num);
            orderClick.InvokeButton(ZT_BuyOrder);
            if (ZT_BuyConfirm == null)
                GetConfirm("买入确认");
        }
        /// <summary>
        /// 股票卖出
        /// </summary>
        /// <param name="_securitiesCode">证券代码</param>
        /// <param name="_num">数量</param>
        static void SaleOrder(string _securitiesCode, string _num)
        {
            var orderClick = new iAutomationElement();
            orderClick.InvokeButton(ZT_SaleButtonElement);
            orderClick.WriteTextBox(ZT_SaleSecuritiesCode, "\b\b\b\b\b\b");
            orderClick.WriteTextBox(ZT_SaleSecuritiesCode, _securitiesCode);
            orderClick.WriteTextBox(ZT_SaleNum, "\b\b\b\b\b\b");
            orderClick.WriteTextBox(ZT_SaleNum, _num);
            orderClick.InvokeButton(ZT_SaleOrder);
            if(ZT_SaleConfirm==null)
                GetConfirm("卖出确认");
        }

        /// <summary>
        /// 基础数据初始化
        /// </summary>
        static void initialization()
        {
            //读取需要的句柄以及UIElement
            //1.读取预警列表UIElement     DZH_uiElement  
            GetReadWaringListViewElement();
            //2.买入按钮UIElement
            GetZT_OrderButtonElement("买入");
            //3.卖出按钮UIElement
            GetZT_OrderButtonElement("卖出");
            //4.获取买入界面UIElement
            var click = new iAutomationElement();
            //点击买入按钮
            click.InvokeButton(ZT_BuyButtonElement);
            //获取持仓单UIElement
            GetZTViewListElement("买入下单");
            //获取买入界面证券代码TextBox UIElement
            GetZTSecodeElement("买入下单");
            //输入证券代码
            click.WriteTextBox(ZT_BuySecuritiesCode, "000001");
            //获取NumBoxUIElement
            GetNumboxElement("买入下单");
            //获取ZT_BuyOrderUIElement
            GetZTOrder("买入下单");
            //5.获取卖出界面UIElement           
            //点击买入按钮
            click.InvokeButton(ZT_SaleButtonElement);
            //获取持仓单UIElement
            GetZTViewListElement("卖出下单");
            //获取买入界面证券代码TextBox UIElement
            GetZTSecodeElement("卖出下单");
            //输入证券代码
            click.WriteTextBox(ZT_SaleSecuritiesCode, "000001");
            //获取NumBoxUIElement
            GetNumboxElement("卖出下单");
            //获取ZT_SaleOrderUIElement
            GetZTOrder("卖出下单");
        }

        /// <summary>
        /// 获取确认交易按钮 UIElement
        /// </summary>
        /// <param name="_type">"买入确认" or "卖出确认"</param>
        static void GetConfirm(string _type)
        {
            var uielement = new iAutomationElement();
            var elementlist = uielement.enumRoot();
            elementlist = uielement.FindByName("中投证券", elementlist);
            elementlist = uielement.enumNode(elementlist[0]);
            if (elementlist.Count > 1)
            {
                foreach (AutomationElement item in elementlist)
                {
                    var list = uielement.enumDescendants(item, _type);
                    if (list.Count > 0)
                    {
                        if(_type == "买入确认")
                        {
                            ZT_BuyConfirm = list[0];
                        }
                        else if(_type =="卖出确认")
                        {
                            ZT_SaleConfirm = list[0];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 大智慧预警表读取
        /// </summary>
        static void GetReadWaringListViewElement()
        {
            var uielement = new iAutomationElement();
            var elemlentlist = uielement.enumRoot();
            elemlentlist = uielement.FindByName("大智慧", elemlentlist);           
            elemlentlist = uielement.enumNode(elemlentlist[0]);           
            elemlentlist = uielement.FindByName("预警", elemlentlist);
            //foreach (AutomationElement item in elemlentlist)
            //{
            //    Console.WriteLine(item.Current.Name + "" + item.Current.ClassName);
            //}
            elemlentlist = uielement.enumNode(elemlentlist[0]);
            elemlentlist = uielement.FindByName("List2", elemlentlist);
            DZH_uiElement = elemlentlist[0];
            DZH_DataList = uielement.GetViewList(elemlentlist[0],5);
           
        }

        /// <summary>
        /// 获取持仓单UIElement
        /// 调用前需要点击买入or卖出按钮切换界面
        /// </summary>
        /// <param name="_type"  >"买入下单"or "卖出下单"</param>
        static void GetZTViewListElement(string _type)
        {
            var uielement = new iAutomationElement();
            var elementlist = uielement.enumRoot();
            elementlist = uielement.FindByName("中投证券", elementlist);
            elementlist = uielement.enumNode(elementlist[0]);
            if (elementlist.Count > 1)
            {
                foreach (AutomationElement item in elementlist)
                {
                    var list = uielement.enumDescendants(item, _type);
                    if (list.Count > 0)
                    {
                        buyWindowsElement = TreeWalker.RawViewWalker.GetParent(list[0]);
                        elementlist = uielement.enumNode(buyWindowsElement);
                        foreach (var count in elementlist)
                            Console.WriteLine(count.Current.ClassName + " " + count.Current.Name);
                        elementlist = uielement.FindByClassName("SysListView32", elementlist);
                        if(_type == "买入下单")
                        {
                            ZT_BuyListView = elementlist[0];
                        }
                        else if(_type == "卖出下单")
                        {
                            ZT_SaleListView = elementlist[0];
                        }
                        
                        ZT_DataList = uielement.GetViewList(elementlist[0], 19);

                    }
                }
            }
        }

       

        /// <summary>
        /// 中投证券买入or点击
        /// </summary>
        /// <param name="_type">"买入"or"卖出"</param>
        static void GetZT_OrderButtonElement(string _type)
        {
            var uielement = new iAutomationElement();
            var elementlist = uielement.enumRoot();
            elementlist = uielement.FindByName("中投证券", elementlist);
            elementlist = uielement.enumNode(elementlist[0]);
            if (elementlist.Count > 1)
            {
                foreach (AutomationElement item in elementlist)
                {
                    var list = uielement.enumDescendants(item, "锁定");
                    if (list.Count > 0)
                    {
                        buyWindowsElement = TreeWalker.RawViewWalker.GetParent(list[0]);
                        elementlist = uielement.enumNode(buyWindowsElement);
                        elementlist = uielement.FindByName(_type, elementlist);
                        if(_type == "买入")
                        {
                            ZT_BuyButtonElement = elementlist[0];
                        }
                        else  if(_type == "卖出")
                        {
                            ZT_SaleButtonElement = elementlist[0];
                        }
                        
                        //uielement.InvokeButton(elementlist[0]);
                    }
                }
            }
        }
       
        /// <summary>
        /// 获取NumBox的UIElement
        /// </summary>
        /// <param name="_type">"买入下单" or "卖出下单"</param>
        static void  GetNumboxElement(string _type)
        {
            var uielement = new iAutomationElement();
            var elementlist = uielement.enumRoot();
            elementlist = uielement.FindByName("中投证券", elementlist);
            elementlist = uielement.enumNode(elementlist[0]);
            if (elementlist.Count > 1)
            {
                foreach (AutomationElement item in elementlist)
                {
                    var list = uielement.enumDescendants(item, _type);
                    if (list.Count > 0)
                    {
                        buyWindowsElement = TreeWalker.RawViewWalker.GetParent(list[0]);
                        elementlist = uielement.enumNode(buyWindowsElement);
                        elementlist = uielement.FindByClassName("Edit", elementlist);
                        foreach(var i in elementlist)
                        {
                            if(i.Current.Name.ToString() ==""||i.Current.Name.ToString() == string.Empty)
                            {
                                if (_type == "买入下单")
                                {
                                    ZT_BuyNum = i;
                                }
                                else if (_type == "卖出下单")
                                {
                                    ZT_SaleNum = i;
                                }
                            }
                        }
                        
                        //uielement.WriteTextBox(elementlist[0], "\b\b\b\b\b\b");
                        //uielement.WriteTextBox(elementlist[0], "000005");
                    }
                }
            }
        }

        /// <summary>
        /// 获取证券代码TextBoxUIElement
        /// </summary>
        /// <param name="_type">"买入下单" or "卖出下单"</param>
        static void GetZTSecodeElement(string _type)
        {
            var uielement = new iAutomationElement();
            var elementlist = uielement.enumRoot();
            elementlist = uielement.FindByName("中投证券", elementlist);
            elementlist = uielement.enumNode(elementlist[0]);
            if (elementlist.Count > 1)
            {
                foreach (AutomationElement item in elementlist)
                {
                    var list = uielement.enumDescendants(item, _type);
                    if (list.Count > 0)
                    {
                        buyWindowsElement = TreeWalker.RawViewWalker.GetParent(list[0]);
                        elementlist = uielement.enumNode(buyWindowsElement);
                        elementlist = uielement.FindByClassName("AfxWnd42", elementlist);
                        if(_type == "买入下单")
                        {
                            ZT_BuySecuritiesCode = elementlist[0];
                        }
                        else  if(_type == "卖出下单")
                        {
                            ZT_SaleSecuritiesCode = elementlist[0];
                        }
                        //uielement.WriteTextBox(elementlist[0], "\b\b\b\b\b\b");
                        //uielement.WriteTextBox(elementlist[0], "000005");
                    }
                }
            }

        }
       
       /// <summary>
       /// 获取下单按钮的UIELement
       /// </summary>
       /// <param name="_type">"买入下单" or "卖出下单"</param>
        static void GetZTOrder(string _type)
        {
            var uielement = new iAutomationElement();
            var elementlist = uielement.enumRoot();
            elementlist = uielement.FindByName("中投证券", elementlist);
            elementlist = uielement.enumNode(elementlist[0]);
            if (elementlist.Count > 1)
            {
                foreach (AutomationElement item in elementlist)
                {
                    var list = uielement.enumDescendants(item, _type);
                    if (list.Count > 0)
                    {
                   
                        if (_type == "买入下单")
                        {
                             ZT_BuyOrder = list[0];
                        }
                        else if (_type == "卖出下单")
                        {
                             ZT_SaleOrder = list[0];
                        }
                        //uielement.WriteTextBox(elementlist[0], "\b\b\b\b\b\b");
                        //uielement.WriteTextBox(elementlist[0], "000005");
                    }
                }
            }
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

      
    }

    
}
