using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USB2to10
{
    public interface ISerialPortService
    {
        void Open();
        void Close();
        event Action<string> DataReceived;  // データ受信イベント（文字列受信を通知）
    }

}
