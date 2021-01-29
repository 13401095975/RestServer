using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HttpMultipartParser
{
    public class BytesReader
    {

        private byte[] buffer;

        private int totalLength = 0;
        private int curPos = 0;

        public BytesReader(byte[] input) : this(input, new UTF8Encoding())
        {
        }

        public BytesReader(byte[] input, Encoding encoding)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            buffer = input;
            totalLength = input.Length;

        }


        public string ReadLine()
        {
            if (curPos == totalLength)
            {
                return null;
            }
            int start = curPos;
            int end = curPos;
            // Note the following common line feed chars:
            // \n - UNIX   \r\n - DOS   \r - Mac
            while (curPos < totalLength && buffer[curPos] != 0x0d && buffer[curPos] != 0x0a) {
                curPos++;
            }

            if (curPos == totalLength) {
                end = curPos;
                return Encoding.UTF8.GetString(buffer, start, end - start);
            }

            if (buffer[curPos] == 0x0d && buffer[curPos+1] == 0x0a) {
                end = curPos;
                curPos += 2;
            }
            if (buffer[curPos] == 0x0d || buffer[curPos] == 0x0a)
            {
                end = curPos;
                curPos += 1;
            }

            if (start == end) {
                return "";
            }

            return Encoding.UTF8.GetString(buffer, start, end - start);

        }

        public byte[] ReadToEnd() {
            int length = totalLength - curPos;
            byte[] bs = new byte[length];
            Buffer.BlockCopy(buffer, curPos, bs, 0, length);
            return bs;
        }


    }
}
