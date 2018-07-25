using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Dottatec.Utils
{
    public class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static bool Logado => (!(string.IsNullOrEmpty(NomeL) && string.IsNullOrEmpty(SenhaL)));

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;


        private const string NomeKey = "Nome_key";
        private static readonly string NomeDefault = string.Empty;


        private const string SenhaKey = "Senha_key";
        private static readonly string SenhaDefault = string.Empty;

        #endregion


        public static string GeneralSettings
        {
            get => AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SettingsKey, value);
        }


        public static string NomeL
        {
            get => AppSettings.GetValueOrDefault(NomeKey, NomeDefault);
            set => AppSettings.AddOrUpdateValue(NomeKey, value);
        }


        public static string SenhaL
        {
            get => AppSettings.GetValueOrDefault(SenhaKey, SenhaDefault);
            set => AppSettings.AddOrUpdateValue(SenhaKey, value);
        }
    }
}
