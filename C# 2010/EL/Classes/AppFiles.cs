using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EL
{
    /// <summary>
    /// מחלקה המכילה את שמות קבצי התוכנה.
    /// </summary>
    class AppFile
    {
        public const string
            CONFIG = AppDirectory.MAIN + "/UserConfiguration.ini",
            CALCULATION = AppDirectory.MAIN + "/Calculation.ini",
            MARQUEE = AppDirectory.MAIN + "/Announce.txt";
        public static string[] LIST = {
                                                CONFIG,
                                                CALCULATION,
                                                MARQUEE
                                            };
        /// <summary>
        /// מציאת טקסט ברירת מחדל של קובץ.
        /// </summary>
        /// <param name="file">שם הקובץ</param>
        /// <returns>טקסט ברירת המחדל</returns>
        static public string GetFileDefaultText(string file)
        {
            string returned = string.Empty;
            switch (file)
            {
                case CONFIG:
                    {
                        returned =
@"LastID=?
Off=?
Permission1=0
Permission2=0
Permission3=0
Permission4=0
Permission5=0
Permission6=0
Permission7=0
Permission8=1";
                        break;
                    }
                case CALCULATION:
                    {
                        returned =
@"Slot1=None
Slot2=None
Slot3=None
Slot4=None
Slot5=None";
                        break;
                    }
                case MARQUEE:
                    {
                        returned =
@"ברוכים הבאים למערכת
ELearning - למידה מתוקשבת


ניתן לערוך את הטקסט שמופיע בתיבה זו דרך לוח הבקרה לניהול";
                        break;
                    }
            }
            return returned;
        }
        /// <summary>
        /// מציאת מיקום קובץ.
        /// </summary>
        /// <param name="name">שם הקובץ</param>
        /// <returns>המיקום המדוייק</returns>
        public static string GetFile(string name)
        {
            return Classes.App.GetProgramDirectory() + "/" + name;
        }
    }
    /// <summary>
    /// מחלקה המכילה את שמות תיקיות התוכנה.
    /// </summary>
    class AppDirectory
    {
        public const string
            MAIN = "EL",
            PROFILES = "EL/Profiles",
            PROFILEPICS = "EL/Profiles/Pics",
            TEMPLATES = "EL/Templates",
            INFO = "EL/InfoFiles";
        public static string[] LIST = {
                                                MAIN,
                                                PROFILES,
                                                PROFILEPICS,
                                                TEMPLATES,
                                                INFO
                                            };
        /// <summary>
        /// מציאת מיקום תיקיה.
        /// </summary>
        /// <param name="name">שם התיקיה</param>
        /// <returns>המיקום המדוייק</returns>
        public static string GetDirectory(string name)
        {
            return Classes.App.GetProgramDirectory() + "/" + name;
        }
    }
}