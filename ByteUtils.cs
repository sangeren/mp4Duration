using System;
using System.Collections.Generic;
using System.Text;

namespace Duration.Mine.Mp4
{
    internal sealed class ByteUtils
    {
        private ByteUtils()
        {
        }

        public static int IndexOf(byte[] array, byte[] pattern, int offset)
        {
            int success = 0;
            for (int i = offset; i < array.Length; i++)
            {
                if (array[i] == pattern[success])
                {
                    success++;
                }
                else
                {
                    success = 0;
                }

                if (pattern.Length == success)
                {
                    return i - pattern.Length + 1;
                }
            }
            return -1;
        }

    }
}
