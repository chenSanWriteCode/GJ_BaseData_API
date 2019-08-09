using System;
using System.Text.RegularExpressions;
using System.Configuration;

namespace GJ_BaseData_API.Infrastructure
{
    public class ConstInfo
    {
        public const string DepartRight = "(2, 3, 4, 5,8, 10)";//8 muping 11 fushan
        public const string DepartAll = "(1,2, 3, 4, 5,8, 10)";
        public readonly static string LogFlag = ConfigurationManager.AppSettings["LogFlag"].ToString();

        

        /// <summary>
        /// 过滤最后的“.”，冒号，中括号以及中括号内部的内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string filterStr(string str)
        {
            return filterStr_maoHao(filterStr_point(filterStr_bracket(str)));
        }
        /// <summary>
        /// 过滤字符串最后的“.” 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string filterStr_point(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str) && str.IndexOf(".")==str.Length-1)
            {
                result=str.Substring(0, str.Length - 1);
            }
            return result;
        }

        /// <summary>
        /// 过滤冒号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string filterStr_maoHao(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str) && str.IndexOf(":")>-1)
            {
                result = str.Replace(":", "");
                filterStr_maoHao(result);
            }
            return result;
        }

        /// <summary>
        /// 过滤中括号以及内部内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string filterStr_bracket(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str) & str.IndexOf("[")>-1)
            {
                result = Regex.Replace(str,@"\[.*\]","");
                filterStr_bracket(result);
            }
            return result;
        }
    }
}