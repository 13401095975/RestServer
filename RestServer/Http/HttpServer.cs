using RestServer.Common.Logger;
using RestServer.Config;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RestServer.Http
{

    public class HttpServer
    {
        #region Fields
        private bool AbortSig = false;
        private int Port;
        private TcpListener Listener;
        public HttpProcessor Processor;
        public ILogger logger { get; set; }
        #endregion


        #region Public Methods
        public HttpServer(ProcessChain processChain):this(ServerConfig.Port, processChain) {
        }
        public HttpServer(int port, ProcessChain processChain)
        {
            this.Port = port;
            this.Processor = new HttpProcessor(processChain);
        }

        public void Listen()
        {
            this.Listener = new TcpListener(IPAddress.Any, this.Port);
            this.Listener.Start();
            while (!AbortSig)
            {
                if (Listener.Pending())
                {
                    TcpClient client = this.Listener.AcceptTcpClient();
                    Thread thread = new Thread(() =>
                    {
                        this.Processor.HandleClient(client);
                    });
                    thread.Start();
                }
                else {
                    Thread.Sleep(50);
                }
                
            }
            Listener.Stop();
        }

        public void Shutdown() {
            AbortSig = true;
        }

        #endregion

    }
}



