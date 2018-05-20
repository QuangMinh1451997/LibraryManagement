using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    public static class Helper
    {
        // format : "{0:000}"
        public static string CreateAutomatedCode(string currCodeMax, int vtEndText, string strFormatNumber)
        {
            string str = currCodeMax;
            if (str != "")
            {
                string strText = str.Substring(0, vtEndText);
                int number = int.Parse(str.Substring(vtEndText))+1;
                return strText + String.Format(strFormatNumber, number);
            }
            return "";
        }

        public static bool IsImage(string extension)
        {
            string[] imgExtension = { ".jpg", ".png", ".jpeg" };
            return imgExtension.Contains(extension.ToLowerInvariant());
        }
    }
}