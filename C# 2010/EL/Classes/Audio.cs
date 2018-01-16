using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Media;
namespace EL.Classes
{
    /// <summary>
    /// ספריית שימוש בפעולות צלילים ושמע.
    /// </summary>
    class Audio
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        private static void CMD(string command)
        {
            mciSendString(command, null, 0, IntPtr.Zero);
        }
        /// <summary>
        /// פתיחת סאונד.
        /// </summary>
        /// <param name="addr">כתובת או נתיב</param>
        public static void Open(string addr)
        {
            CMD("open \"" + addr + "\" type mpegvideo alias MediaFile");
        }
        /// <summary>
        /// השמעת סאונד שנפתח לפניכן.
        /// </summary>
        /// <param name="loop">השמעה חוזרת</param>
        public static void Play(bool loop = false)
        {
            CMD("play MediaFile" + (loop ? "REPEAT" : ""));
        }
        /// <summary>
        /// הפסקת סאונד שהושמע.
        /// </summary>
        public static void Stop()
        {
            CMD("pause MediaFile");
        }
        /// <summary>
        /// סגירת קובץ שמע שנפתח.
        /// </summary>
        public static void Close()
        {
            CMD("close MediaFile");
        }
        /// <summary>
        /// בדיקת סטטוס שמע נוכחי.
        /// </summary>
        /// <returns>הסטטוס כסטרינג: playing, paused, stopped</returns>
        public static string Status()
        {
            int i = 128;
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(i);
            mciSendString("status MediaFile mode", stringBuilder, i, IntPtr.Zero);
            return stringBuilder.ToString();
        }
    }
}