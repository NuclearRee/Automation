using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Common
{
  public class NumCalculation
    {
        /// <summary>
        /// 获得买入数量
        /// </summary>
        /// <param name="_datalist">对应股票数据</param>
        /// <param name="_canBuyNum">可买数量</param>
        /// <returns></returns>
        static int GetBuyNum(string [] _dataList,int _canBuyNum)
        {
            //盘口数量 = 卖1量 + 卖2量 + 卖3量
            int HandicapNum = (Convert.ToInt32(_dataList[20]) + Convert.ToInt32(_dataList[22]) + Convert.ToInt32(_dataList[24])) / 100;
            if (_canBuyNum > 20000)
                _canBuyNum = 20000;
            if (_canBuyNum < HandicapNum)
                return _canBuyNum;
            else
                return HandicapNum;
        }
    }
}
