using System;
using System.Data;

namespace EASYS.Utils
{
    public static class JsonHelper
    {
        /// <summary>
        /// Datatable转换为Json
        /// </summary>
        /// <param name="table">Datatable对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(DataTable table)
        {
            if (table == null) return "";
            string jsonString = "[";
            DataRowCollection drc = table.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString += "{";
                foreach (DataColumn column in table.Columns)
                {
                    jsonString += "\"" + ToJson(column.ColumnName) + "\":";
                    if (column.DataType == typeof(DateTime) || column.DataType == typeof(string))
                    {
                        jsonString += "\"" + ToJson(drc[i][column.ColumnName].ToString()) + "\",";
                    }
                    else
                    {
                        jsonString += ToJson(drc[i][column.ColumnName].ToString()) + ",";
                    }
                }
                jsonString = DeleteLast(jsonString) + "},";
            }
            return DeleteLast(jsonString) + "]";
        }

        /// <summary>
        /// String转换为Json
        /// </summary>
        /// <param name="value">String对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            string temstr;
            temstr = value;
            temstr = temstr.Replace("{", "｛").Replace("}", "｝").Replace(":", "：").Replace(",", "，").Replace("[", "【").Replace("]", "】").Replace(";", "；").Replace("\n", "<br/>").Replace("\r", "");
            temstr = temstr.Replace("\t", "  ");
            temstr = temstr.Replace("'", "\'");
            temstr = temstr.Replace(@"\", @"\\");
            temstr = temstr.Replace("\"", "\"\"");
            return temstr;
        }

        /// <summary>
        /// 删除结尾字符
        /// </summary>
        /// <param name="str">需要删除的字符</param>
        /// <returns>完成后的字符串</returns>
        private static string DeleteLast(string str)
        {
            if (str.Length > 1)
            {
                return str.Substring(0, str.Length - 1);
            }
            return str;
        }
    }
}
