using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpMultipartParser
{
    class ByteArrayUtils
    {
        public static List<byte[]> Split(byte[] bytes, byte[] spliter) {
            List<byte[]> list = new List<byte[]>();

            int lastIndex = -1;
            while (true) {
                int index = Search(bytes, lastIndex+1, spliter);

                if (index != -1)
                {
                    if (lastIndex != -1)
                    {
                        int count = index - lastIndex - spliter.Length;
                        byte[] split = new byte[count];
                        Buffer.BlockCopy(bytes, lastIndex + spliter.Length, split, 0, count);
                        list.Add(Trim(split));
                    }
                    
                    lastIndex = index;
                    
                }
                else {
                    break;
                }
            }
            return list;

        }

        private static byte[] Trim(byte[] bs) {
            int start = 0;
            int end = bs.Length - 1;

            if (bs[start] == 0x0d && bs[start+1] == 0x0a) {
                start += 2;
            }
            else if (bs[start] == 0x0d || bs[start] == 0x0a)
            {
                start += 1;
            }
            if (bs[start] == 0x0d && bs[start + 1] == 0x0a)
            {
                start += 2;
            }
            else if (bs[start] == 0x0d || bs[start] == 0x0a)
            {
                start += 1;
            }

            if (bs[end - 2] == 0x0d && bs[end - 1] == 0x0a) {
                end -= 2;
            }
            else if (bs[end - 1] == 0x0d || bs[end - 1] == 0x0a)
            {
                end -= 1;
            }
            
            int count = end - start;
            byte[] buffer = new byte[count];
            Buffer.BlockCopy(bs, start, buffer, 0, count);
            return buffer;
        }

        public static int Search(byte[] haystack, int startIndex,byte[] needle)
        {
            var charactersInNeedle = new HashSet<byte>(needle);
            int totalLength = haystack.Length;
            var length = needle.Length;
            var index = startIndex;
            while (index + length <= totalLength)
            {
                // Worst case scenario: Go back to character-by-character parsing until we find a non-match
                // or we find the needle.
                if (charactersInNeedle.Contains(haystack[index + length - 1]))
                {
                    var needleIndex = 0;
                    while (haystack[index + needleIndex] == needle[needleIndex])
                    {
                        if (needleIndex == needle.Length - 1)
                        {
                            // Found our match!
                            return index;
                        }

                        needleIndex += 1;
                    }

                    index += 1;
                    index += needleIndex;
                    continue;
                }

                index += length;
            }

            return -1;
        }

    }
}
