using System;
using System.IO;
using System.Text;

namespace Duration.Mine.Mp4
{
    /// <summary>
    /// mp4 format
    /// </summary>
    public class Mp4Duration
    {
        /// <summary>
        /// get mp4 duration
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>second</returns>
        public static double GetMp4Duration(string fileName)
        {
            ulong timeScale = 0;
            ulong timeUnits = 0;
            int byteSize = 4;

            string str = "mvhd";
            var charStr = Encoding.ASCII.GetBytes(str);

            byte[] array = File.ReadAllBytes(fileName);
            var indexOfCuurent = ByteUtils.IndexOf(array, charStr, 0);
            indexOfCuurent += 4;

            if (array[indexOfCuurent] == 1)
            {
                byteSize = 8;
            }
            indexOfCuurent += 4;//now point to the createa date

            //byte[] createDateByte = new byte[byteSize];
            //Array.Copy(array, indexOfCuurent, createDateByte, 0, byteSize);
            //var createDate = BitConverter.ToUInt32(createDateByte, 0);
            indexOfCuurent += byteSize;
            indexOfCuurent += byteSize;

            byte[] timeScaleByte = new byte[4];
            Array.Copy(array, indexOfCuurent, timeScaleByte, 0, 4);
            timeScale = BitConverter.ToUInt32(timeScaleByte, 0);

            indexOfCuurent += 4;
            byte[] timeUnitsByte = new byte[4];
            Array.Copy(array, indexOfCuurent, timeUnitsByte, 0, 4);
            timeUnits = BitConverter.ToUInt32(timeUnitsByte, 0);

            timeScale = InfoFlip(timeScale);
            timeUnits = InfoFlip(timeUnits);

            return timeUnits * 1.0 / timeScale;
        }

        static UInt64 InfoFlip(UInt64 vla)
        {
            UInt64 tem = 0;
            tem += (vla & 0x000000FF) << 24;
            tem += (vla & 0xFF000000) >> 24;
            tem += (vla & 0x0000FF00) << 8;
            tem += (vla & 0x00FF0000) >> 8;
            return tem;
        }
    }
}
