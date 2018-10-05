using System;
using System.Configuration;

namespace SampleApp.Connection
{
    class AppSettingReader
    {
        private static AppSettingReader _instance;

        public string DbConnection
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["Connection"]);
            }
        }

        public AppSettingReader()
        {

        }

        public static AppSettingReader GetAppSettingReader()
        {
            if (_instance == null)
            {
                _instance = new AppSettingReader();
            }
            return _instance;
        }
    }
}
