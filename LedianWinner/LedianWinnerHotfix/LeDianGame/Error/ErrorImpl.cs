
using org.zgl.error;

namespace org.zgl
{
    public class ErrorImpl : IError
    {
        public void err(int errorCode, string[] msg)
        {
            AppErrorDataTable datable = AppErrorDataTable.get(errorCode);
            string st = datable.value;//从静态表中获取
            string str = st;
            if (msg != null && msg.Length > 0) {
                str = string.Format(st, msg);
            }
            XUIMidMsg.QuickMsg(str);
        }
    }
}
