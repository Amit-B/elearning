using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace EL.Classes
{
    /// <summary>
    /// ספריית קבצים מסוג INI.
    /// </summary>
    class INI
    {
        public enum LineSplitMode { GetKey, GetValue }
        public enum InfoType { Seperator, Comment, FileName, FullFileName, FileExt, FullPath, Lines }
        public enum KeyInfoType { Key, Value, Line }
        private string path = string.Empty;
        private const char seperator = '=', comment = '#';
        /// <summary>
        /// התחלת עבודה עם קובץ מסויים.
        /// </summary>
        /// <param name="path">שם הקובץ</param>
        public INI(string path)
        {
            this.path = path;
        }
        // -------------------- Private Functions --------------------
        private bool NoticeThisLine(string line)
        {
            if (line.Length < 3 || line[0] == comment) return false;
            int num = 0;
            for (int i = 0; i < line.Length; i++) if (line[i] == seperator) num++;
            return num == 1;
        }
        private string SplitThisLine(string line, LineSplitMode mode)
        {
            return line.Split(seperator)[0 + (Convert.ToInt32(mode == LineSplitMode.GetValue))];
        }
        // -------------------- Private Functions --------------------
        /// <summary>
        /// בדיקה האם הקובץ שהתחלנו לעבוד איתו קיים.
        /// </summary>
        /// <returns>ערך בוליאני לקיום הקובץ</returns>
        public bool Exists()
        {
            return File.Exists(path);
        }
        /// <summary>
        /// קבלת ערך מתוך הקובץ לפי מפתח.
        /// </summary>
        /// <param name="key">שם המפתח בתוך הקובץ</param>
        /// <returns>הערך בתור סטרינג לפי שם המפתח או סטרינג ריק אם הפעולה לא הצליחה</returns>
        public string GetKey(string key)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                    if (NoticeThisLine(lines[i]) && SplitThisLine(lines[i], LineSplitMode.GetKey) == key)
                        return SplitThisLine(lines[i], LineSplitMode.GetValue);
            }
            catch
            {
                return string.Empty;
            }
            return string.Empty;
        }
        /// <summary>
        /// שינוי ערך בתוך קובץ לפי מפתח.
        /// </summary>
        /// <param name="key">שם המפתח בתוך הקובץ</param>
        /// <param name="value">הערך החדש</param>
        /// <returns>ערך בוליאני: אמת אם הפעולה הצליחה, אחרת שקר</returns>
        public bool SetKey(string key, string value)
        {
            bool toReturn = false;
            try
            {
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length && !toReturn; i++)
                    if (NoticeThisLine(lines[i]) && SplitThisLine(lines[i], LineSplitMode.GetKey) == key)
                    {
                        lines[i] = key + seperator + value;
                        toReturn = true;
                    }
                File.WriteAllLines(path, lines);
            }
            catch
            {
                return false;
            }
            return toReturn;
        }
        /// <summary>
        /// שינוי שם של מפתח בתוך הקובץ.
        /// </summary>
        /// <param name="oldkey">השם הקודם של המפתח</param>
        /// <param name="newkey">השם החדש של המפתח</param>
        /// <returns>ערך בוליאני: אמת אם הפעולה הצליחה, אחרת שקר</returns>
        public bool RenameKey(string oldkey, string newkey)
        {
            bool toReturn = false;
            try
            {
                string[] lines = File.ReadAllLines(path);
                string val = string.Empty;
                for (int i = 0; i < lines.Length && !toReturn; i++)
                    if (NoticeThisLine(lines[i]) && SplitThisLine(lines[i], LineSplitMode.GetKey) == oldkey)
                    {
                        val = SplitThisLine(lines[i], LineSplitMode.GetValue);
                        lines[i] = newkey + seperator + val;
                        toReturn = true;
                    }
                File.WriteAllLines(path, lines);
            }
            catch
            {
                return false;
            }
            return toReturn;
        }
        /// <summary>
        /// הוספת הערה לתוך קובץ.
        /// </summary>
        /// <param name="text">ההערה</param>
        /// <returns>ערך בוליאני: אמת אם הפעולה הצליחה, אחרת שקר</returns>
        public bool AddComment(string text)
        {
            bool toReturn = false;
            try
            {
                string[] lines = File.ReadAllLines(path);
                int placeOn = -1;
                for (int i = 0; i < lines.Length; i++)
                    if (lines[i].Length > 1 && lines[i][0] == comment)
                        placeOn = i;
                if (placeOn == -1) lines[0] = comment + text;
                else lines[placeOn] += "\r\n" + comment + text;
                File.WriteAllLines(path, lines);
            }
            catch
            {
                return false;
            }
            return toReturn;
        }
        /// <summary>
        /// קבלת מידע על השימוש הנוכחי בספרייה עם הקובץ הנוכחי.
        /// </summary>
        /// <param name="t">סוג המידע להחזרה</param>
        /// <returns>המידע שהתבקש, או כלום אם הפעולה לא הצליחה</returns>
        public object GetInfo(InfoType t)
        {
            try
            {
                switch (t)
                {
                    case InfoType.Seperator: return seperator;
                    case InfoType.Comment: return comment;
                    case InfoType.FileName: return path.Split('\\')[path.Split('\\').Length].Split('.')[0];
                    case InfoType.FullFileName: return path.Split('\\')[path.Split('\\').Length];
                    case InfoType.FileExt: return path.Split('.')[path.Split('.').Length];
                    case InfoType.FullPath: return path;
                    case InfoType.Lines: return File.ReadAllLines(path).Length;
                    default: return null;
                }
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// קבלת מידע על שורה אחת מסויימת מתוך הקובץ הנוכחי.
        /// </summary>
        /// <param name="t">סוג המידע להחזרה</param>
        /// <param name="findByKey">מציאת המידע לפי מפתח</param>
        /// <param name="findByValue">מציאת המידע לפי ערך</param>
        /// <param name="findByLine">מציאת המידע לפי מספר שורה</param>
        /// <returns>המידע שהתבקש, או כלום אם הפעולה לא הצליחה</returns>
        public object GetKeyInfo(KeyInfoType t, string findByKey = "", string findByValue = "", int findByLine = 0)
        {
            try
            {
                if (findByKey.Length > 0)
                {
                    switch (t)
                    {
                        case KeyInfoType.Key: return findByKey;
                        case KeyInfoType.Value: return GetKey(findByKey);
                        case KeyInfoType.Line:
                            {
                                string[] lines = File.ReadAllLines(path);
                                for (int i = 0; i < lines.Length; i++)
                                    if (NoticeThisLine(lines[i]) && SplitThisLine(lines[i], LineSplitMode.GetKey) == findByKey)
                                        return i + 1;
                                return 0;
                            }
                        default: return null;
                    }
                }
                else if (findByValue.Length > 0)
                {
                    switch (t)
                    {
                        case KeyInfoType.Key:
                            {
                                string[] lines = File.ReadAllLines(path);
                                for (int i = 0; i < lines.Length; i++)
                                    if (NoticeThisLine(lines[i]) && SplitThisLine(lines[i], LineSplitMode.GetValue) == findByValue)
                                        return SplitThisLine(lines[i], LineSplitMode.GetKey);
                                return string.Empty;
                            }
                        case KeyInfoType.Value: return findByValue;
                        case KeyInfoType.Line:
                            {
                                string[] lines = File.ReadAllLines(path);
                                for (int i = 0; i < lines.Length; i++)
                                    if (NoticeThisLine(lines[i]) && SplitThisLine(lines[i], LineSplitMode.GetValue) == findByValue)
                                        return i + 1;
                                return 0;
                            }
                        default: return null;
                    }
                }
                else if (findByLine > 0)
                {
                    switch (t)
                    {
                        case KeyInfoType.Key: return SplitThisLine(File.ReadAllLines(path)[findByLine], LineSplitMode.GetKey);
                        case KeyInfoType.Value: return SplitThisLine(File.ReadAllLines(path)[findByLine], LineSplitMode.GetValue);
                        case KeyInfoType.Line: return findByLine;
                        default: return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
        /// <summary>
        /// קבלת המיקום של הקובץ הנוכחי או שינוי המיקום.
        /// </summary>
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                File.Move(path, value);
                path = value;
            }
        }
        /// <summary>
        /// קבלת שם הקובץ הנוכחי או שינוי שם הקובץ.
        /// </summary>
        public string FileName
        {
            get
            {
                return GetInfo(InfoType.FullFileName).ToString();
            }
            set
            {
                string newpath = (path.Remove(path.LastIndexOf('\\') + 1, GetInfo(InfoType.FullFileName).ToString().Length)) + value;
                File.Move(path, newpath);
                path = newpath;
            }
        }
    }
}