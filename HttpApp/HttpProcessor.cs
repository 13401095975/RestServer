// Copyright (C) 2016 by David Jeske, Barend Erasmus and donated to the public domain

using HttpApp;
using HttpApp.Filter;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SimpleHttpServer
{
    public class HttpProcessor
    {

        #region Fields

        private FilterChain filterChain { get; set; }

        #endregion

        #region Constructors

        public HttpProcessor()
        {
            filterChain = new FilterChain();
        }

        #endregion



        #region Public Methods

        public void SetFilterChain(FilterChain filterChain)
        {
            this.filterChain = filterChain;
        }

        public void HandleClient(TcpClient tcpClient)
        {
            Stream inputStream = GetInputStream(tcpClient);
            Stream outputStream = GetOutputStream(tcpClient);
            HttpRequest request = ParseRequest(inputStream);

            HttpResponse response = new HttpResponse();
            filterChain.doFilter(request, ref response);

            GenerateResponse(request, ref response);
            WriteResponse(outputStream, response);

            outputStream.Flush();
            outputStream.Close();
            outputStream = null;

            inputStream.Close();
            inputStream = null;

        }

        private void GenerateResponse(HttpRequest request, ref HttpResponse response) {
            if (response.Content == null)
            {
                if (response.StatusCode != HttpStatusCode.Ok.ToString())
                {
                    response.ContentAsUTF8 = string.Format("{0} {1} <p> {2}", response.StatusCode, request.Url, response.StatusDescription);
                }
            }

            if (response.Content == null)
            {
                response.Content = new byte[] { };
            }

            // default to application/json content type
            if (!response.Headers.ContainsKey("Content-Type"))
            {
                response.Headers["Content-Type"] = "application/json";
            }

            byte[] content = response.Content;

            if (ServerConfig.EnableCompress && response.Content.Length > ServerConfig.MinCompressSize)
            {
                content = Compress(content);
                response.Headers["Content-Encoding"] = "gzip";
            }

            response.Headers["Content-Length"] = content.Length.ToString();
            response.Content = content;
        }

        // this formats the HTTP response...
        private static void WriteResponse(Stream stream, HttpResponse response)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("HTTP/1.0 {0} {1}\r\n", response.StatusCode, response.StatusDescription))
                .Append(string.Join("\r\n", response.Headers.Select(x => string.Format("{0}: {1}", x.Key, x.Value))))
                .Append("\r\n\r\n");
            byte[] bytes = Encoding.UTF8.GetBytes(builder.ToString());
            stream.Write(bytes, 0 , bytes.Length);

            stream.Write(response.Content, 0, response.Content.Length);
        }

        public static byte[] Compress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Private Methods

        private static string ReadHttpLine(Stream stream)
        {
            int next_char;
            string data = "";
            while (true)
            {
                next_char = stream.ReadByte();
                if (next_char == '\n') { break; }
                if (next_char == '\r') { continue; }
                if (next_char == -1) { Thread.Sleep(1); continue; };
                data += Convert.ToChar(next_char);
            }
            return data;
        }

        protected virtual Stream GetOutputStream(TcpClient tcpClient)
        {
            return tcpClient.GetStream();
        }

        protected virtual Stream GetInputStream(TcpClient tcpClient)
        {
            return tcpClient.GetStream();
        }


        private HttpRequest ParseRequest(Stream inputStream)
        {
            //Read Request Line
            string request = ReadHttpLine(inputStream);

            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("invalid http request line");
            }
            string method = tokens[0].ToUpper();
            string url = tokens[1];
            string protocolVersion = tokens[2];

            //Read Headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string line;
            while ((line = ReadHttpLine(inputStream)) != null)
            {
                if (line.Equals(""))
                {
                    break;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }
                string name = line.Substring(0, separator);
                int pos = separator + 1;
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++;
                }

                string value = line.Substring(pos, line.Length - pos);
                headers.Add(name, value);
            }

            string content = null;
            if (headers.ContainsKey("Content-Length"))
            {
                int totalBytes = Convert.ToInt32(headers["Content-Length"]);
                int bytesLeft = totalBytes;
                byte[] bytes = new byte[totalBytes];

                while (bytesLeft > 0)
                {
                    byte[] buffer = new byte[bytesLeft > 1024 ? 1024 : bytesLeft];
                    int n = inputStream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);

                    bytesLeft -= n;
                }

                content = Encoding.UTF8.GetString(bytes);
            }

            string[] p = url.Split('?');
            string query = "";
            string path = url;
            if (p.Length == 2)
            {
                path = p[0];
                query = p[1];
            }


            return new HttpRequest()
            {
                Method = method,
                Url = url,
                Path = path,
                QueryString = query,
                Headers = headers,
                Content = content
            };
        }

        #endregion

    }
}
