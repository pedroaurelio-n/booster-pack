public class SettingsManager
{
    public SettingsProvider<ICardListSettings, CardListSettings> CardListSettings
    {
        get;
        private set;
    }

    public SettingsProvider<IBoosterPackSettings, BoosterPackSettings> BoosterPackSettings
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
        BoosterPackSettings = new SettingsProvider<IBoosterPackSettings, BoosterPackSettings>("booster-pack-settings");
    }
}