public class SettingsManager
{
    public SettingsProvider<ICardListSettings, CardListSettings> CardListSettings
    {
        get;
        private set;
    }

    public SettingsManager ()
    {
        InitializeSettings();
    }

    void InitializeSettings ()
    {
        CardListSettings = new SettingsProvider<ICardListSettings, CardListSettings>("card-list-settings");
    }
}