public class SettingsManager
{
    public SettingsProvider<ICardListSettings, CardListSettings> CardListSettings
    {
        get;
        private set;
    }

    public void InitializeSettings ()
    {
        CardListSettings = new SettingsProvider<ICardListSettings, CardListSettings>("card-list-settings");
    }
}