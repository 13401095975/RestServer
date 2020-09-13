
using RestServer.Config;
using RestServer.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RestServer.Http
{
    public class HttpProcessor
    {

        #region Fields

        private FilterChain filterChain { get; set; }

        #endregion

        #region Constructors

        public HttpProcessor(FilterChain filterChain)
        {
            this.filterChain = filterChain;
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

            filterChain.Handle(request, ref response);

            FormatHttpResponse(request, ref response);
            WriteResponse(outputStream, response);

            outputStream.Flush();
            outputStream.Close();
            outputStream = null;

            inputStream.Close();
            inputStream = null;

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
                response.Headers.Add("Content-Type", ServerConfig.DefaultContentType);
            }

            byte[] content = response.Data;

            if (ServerConfig.EnableCompress && response.Data.Length > ServerConfig.MinCompressSize)
            {
                content = Compress(content);
                response.Headers.Add("Content-Encoding", ServerConfig.DefaultContentEncoding);
            }

            response.Headers.Add("Content-Length", content.Length.ToString());
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
                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++;
                }

                string value = line.Substring(pos, line.Length - pos);
                headers.Add(name, value);
            }

            string content = null;
            if (headers.Contains("Content-Length"))
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
