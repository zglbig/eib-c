namespace org.zgl
{
    public class SocketFactory
    {
        private ISocket tcp;
        private static SocketFactory instance;

        public static SocketFactory getInstance()
        {
            if (instance == null)
                instance = new SocketFactory();
            return instance;
        }

        public void tcpAsyncRequest(IoMessage ioMessage)
        {
            tcp.async(ioMessage);
        }
        public object tcpSyncRequest(IoMessage ioMessage)
        {
            return tcp.sync(ioMessage);
        }
       
    }
}
