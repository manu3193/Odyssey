using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GUIOdyssey.LogicLayer
{
    public class Mp3Player : IDisposable
    {
        public bool Repeat { get; set; }

        public Mp3Player(string fileName)
        {
            const string FORMAT = @"open ""{0}"" type mpegvideo alias MediaFile";
            string command = String.Format(FORMAT, fileName);
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public void Play()
        {
            string command = "play MediaFile";
            if (Repeat) command += " REPEAT";
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public void Stop()
        {
            string command = "stop MediaFile";
            mciSendString(command, null, 0, IntPtr.Zero);

        }
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand,
            StringBuilder strReturn, int iReturnLength, IntPtr handCallback);

        public void Dispose()
        {
            string command = "close Mediafile";
            mciSendString(command, null, 0, IntPtr.Zero);
        }
    }
}