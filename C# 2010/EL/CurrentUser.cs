using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EL
{
    /// <summary>
    /// עבודה עם המשתמש הנוכחי בתוכנה.
    /// </summary>
    class CurrentUser
    {
        /// <summary>
        /// ת.ז של המשתמש הנוכחי בתוכנה.
        /// </summary>
        public static string ID { get; set; }
        /// <summary>
        /// בדיקת האם המשתמש הנוכחי הוא מנהל.
        /// </summary>
        public static bool Administrator { get; set; }
    }
}