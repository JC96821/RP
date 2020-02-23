using MySql.Data.MySqlClient;
using System.Data;

namespace EASYS.Utils
{
    public static class DbCommand
    {
        private static string conn = ConfigHelper.getDbConn();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql">sql文</param>
        /// <returns></returns>
        public static DataTable doSearch(string sql)
        {
            DataTable dt = new DataTable();

            MySqlConnection con = new MySqlConnection(conn);
            MySqlCommand cmd;
            MySqlDataAdapter msda;
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                msda = new MySqlDataAdapter(cmd);
                msda.Fill(dt);
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();

            }
            return dt;
        }

        /// <summary>
        /// 事务处理
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool doTransacte(string sql)
        {
            int rs = 0;
            MySqlConnection con = new MySqlConnection(conn);
            MySqlCommand cmd;
            try
            {
                con.Open();
                cmd = new MySqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                rs = cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }

            if (rs == 0)
            {
                return false;
            }
            return true;
        }

    }
}
