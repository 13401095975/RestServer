using System;
using System.IO;
using System.IO.Compression;

namespace RestServer.Common.Compress
{
    class DeflateCompress
    {
        public static byte[] Compress(byte[] data)
        {
            try
            {
                Stream stream = new MemoryStream(data);
                using (var compressStream = new MemoryStream())
                using (var compressor = new DeflateStream(compressStream, CompressionMode.Compress))
                {
                    stream.CopyTo(compressor);
                    compressor.Close();
                    return compressStream.ToArray();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
