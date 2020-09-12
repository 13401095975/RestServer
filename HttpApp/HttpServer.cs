// Copyright (C) 2016 by David Jeske, Barend Erasmus and donated to the public domain

using HttpApp.Filter;
using HttpApp.Logger;
using SimpleHttpServer;
using SimpleHttpServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SimpleHttpServer
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

        //private static readonly ILog log = LogManager.GetLogger(typeof(HttpServer));

        #region Public Methods
        public HttpServer(int port, FilterChain filterChain)
        {
            this.Port = port;
            this.Processor = new HttpProcessor();
            Processor.SetFilterChain(filterChain);

            //logger.Info("server started at port " + port);
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

        #endregion

    }
}



