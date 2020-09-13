﻿using RestServer.Common.Logger;
using RestServer.Config;
using RestServer.Filter;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RestServer.Http
{

    public class HttpServer
    {
        #region Fields

        private int Port;
        private TcpListener Listener;
        public HttpProcessor Processor;
        private bool IsActive = true;
        public ILogger logger { get; set; }
        #endregion


        #region Public Methods
        public HttpServer(FilterChain filterChain):this(ServerConfig.Port, filterChain) {
        }
        public HttpServer(int port, FilterChain filterChain)
        {
            this.Port = port;
            this.Processor = new HttpProcessor(filterChain);
        }

        public void Listen()
        {
            this.Listener = new TcpListener(IPAddress.Any, this.Port);
            this.Listener.Start();
            while (this.IsActive)
            {
                TcpClient s = this.Listener.AcceptTcpClient();
                Thread thread = new Thread(() =>
                {
                    this.Processor.HandleClient(s);
                });
                thread.Start();
                Thread.Sleep(1);
            }
        }

        public void Shutdown() {
            IsActive = false;
        }

        #endregion

    }
}


