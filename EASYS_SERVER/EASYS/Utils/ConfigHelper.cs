using System.Configuration;

namespace EASYS.Utils
{
    public static class ConfigHelper
    {
        /// <summary>
        /// 获取db连接串
        /// </summary>
        /// <returns></returns>
        public static string getDbConn()
        {
            return ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        }

        /// <summary>
        /// WebConfig配置数据获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getAppConfig(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

        /// <summary>
        /// 修改和添加custom配置数据 如果相应的Key存在则修改 如不存在则添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static bool setAppConfig(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings[key].Value = value;
                }
                else
                {
                    config.AppSettings.Settings.Add(key, value);
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
