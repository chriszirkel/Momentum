// Helpers/Settings.cs
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Momentum.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string AuthenticatedKey = nameof(AuthenticatedKey);
        private static readonly bool AuthenticatedDefault = default(bool);

        private const string AuthenticationProviderKey = nameof(AuthenticationProviderKey);
        private static readonly MobileServiceAuthenticationProvider AuthenticationProviderDefault = default(MobileServiceAuthenticationProvider);

        private const string CurrentUserIdKey = nameof(CurrentUserIdKey);
        private const string CurrentUserIdDefault = default(string);

        #endregion

        public static bool IsAuthenticated
        {
            get { return AppSettings.GetValueOrDefault(AuthenticatedKey, AuthenticatedDefault); }
            set { AppSettings.AddOrUpdateValue(AuthenticatedKey, value); }
        }

        public static MobileServiceAuthenticationProvider AuthenticationProvider
        {
            get { return AppSettings.GetValueOrDefault(AuthenticationProviderKey, AuthenticationProviderDefault); }
            set { AppSettings.AddOrUpdateValue(AuthenticationProviderKey, value); }
        }

        public static string CurrentUserId
        {
            get { return AppSettings.GetValueOrDefault(CurrentUserIdKey, CurrentUserIdDefault); }
            set { AppSettings.AddOrUpdateValue(CurrentUserIdKey, value); }
        }

        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
            }
        }

    }
}