using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Clonium
{
    public static class GameConfigManager
    {
        public static void SaveGameConfig(string field, int size, int numberOfPlayers)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["FieldName"].Value = field;
            config.AppSettings.Settings["Size"].Value = size.ToString();
            config.AppSettings.Settings["NumberOfPlayers"].Value = numberOfPlayers.ToString();

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void LoadGameConfig(out string field, out int size, out int numberOfPlayers)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            field = config.AppSettings.Settings["FieldName"].Value;
            size = int.Parse(config.AppSettings.Settings["Size"].Value);
            numberOfPlayers = int.Parse(config.AppSettings.Settings["NumberOfPlayers"].Value);
        }
    }
}
