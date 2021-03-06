﻿
using HttpMultipartParser;
using RestServer.Common.Compress;
using RestServer.Common.Logger;
using RestServer.Config;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RestServer.Http
{
    public class HttpProcessor
    {

        #region Fields

        private ProcessChain processChain { get; set; }

        private ILogger logger = LoggerFactory.GetLogger();

        #endregion

        #region Constructors

        public HttpProcessor(ProcessChain filterChain)
        {
            this.processChain = filterChain;
        }

        #endregion

        #region Public Methods

        public void SetFilterChain(ProcessChain filterChain)
        {
            this.processChain = filterChain;
        }

        public void HandleClient(TcpClient tcpClient)
        {
            Stream inputStream = GetInputStream(tcpClient);
            Stream outputStream = GetOutputStream(tcpClient);

            HttpRequest request = ParseRequest(inputStream);
            HttpResponse response = new HttpResponse();

            try
            {
                FormatHttpResponse(request, ref response);
                WriteResponse(outputStream, response);
            }
            catch (Exception e)
            {
                logger.Warn("error occur:"+e.ToString());
            }
            finally
            {
                outputStream.Flush();
                outputStream.Close();

                inputStream.Close();
            }

        }

        private void FormatHttpResponse(HttpRequest request, ref HttpResponse response) {
            if (response.Data == null)
            {
                if (response.StatusCode != HttpStatusCode.Ok.ToString())
                {
                    response.ContentAsUTF8 = string.Format("{0} {1} <p> {2}", response.StatusCode, request.Url, response.StatusDescription);
                }
            }

            if (response.Data == null)
            {
                response.Data = new byte[] { };
            }

            if (!response.Headers.Contains("Content-Type"))
            {
                response.Headers.SetContentType(HttpConfig.DefaultContentType);
            }

            byte[] content = response.Data;

            if (ServerConfig.EnableCompress && response.Data.Length > ServerConfig.MinCompressSize)
            {
                content = GzipCompress.Compress(content);
                response.Headers.SetContentEncoding(HttpConfig.DefaultContentEncoding);
            }

            response.Headers.SetContentLength(content.Length.ToString());

            //添加通用header
            response.Headers.SetDate(DateTime.UtcNow.ToString("r"));
            response.Headers.SetServer(ServerConfig.ServerName);

            response.Data = content;
        }

        private void WriteResponse(Stream stream, HttpResponse response)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("HTTP/1.0 {0} {1}\r\n", response.StatusCode, response.StatusDescription))
                .Append(response.Headers.ToString())
                .Append("\r\n\r\n");
            byte[] bytes = Encoding.UTF8.GetBytes(builder.ToString());
            stream.Write(bytes, 0 , bytes.Length);

            stream.Write(response.Data, 0, response.Data.Length);
        }

        #endregion

        #region Private Methods

        private string ReadHttpLine(Stream stream)
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
            HttpHeaders  headers = new HttpHeaders();
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
                
                string value = line.Substring(pos, line.Length - pos).Trim();
                headers.Add(name, value);
            }

            string content = null;
            byte[] bytes = null;
            if (headers.Contains("Content-Length"))
            {
                int totalBytes = Convert.ToInt32(headers["Content-Length"]);
                int bytesLeft = totalBytes;
                bytes = new byte[totalBytes];

                while (bytesLeft > 0)
                {
                    byte[] buffer = new byte[bytesLeft > 1024 ? 1024 : bytesLeft];
                    int n = inputStream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);

                    bytesLeft -= n;
                }

            }

            //List<Multipart> multiparts = new List<Multipart>();
            MultipartFormDataParser formData = null;
            if (headers.Contains("Content-Type")) {
                string tp = headers["Content-Type"];
                if (tp.StartsWith("multipart/form-data"))
                {
                    formData = MultipartFormDataParser.Parse(bytes);
                }
                
            }

            string[] p = url.Split('?');
            string query = "";
            string path = url;
            if (p.Length == 2)
            {
                path = p[0];
                query = p[1];
            }
            else {
                query = content;
            }


            return new HttpRequest()
            {
                Method = method,
                Url = url,
                HttpVersion = protocolVersion,
                Path = path,
                QueryString = query,
                Headers = headers,
                //Content = content,
                BodyBytes = bytes,
                MultipartFormData = formData
            };
        }

        #endregion

    }
}
