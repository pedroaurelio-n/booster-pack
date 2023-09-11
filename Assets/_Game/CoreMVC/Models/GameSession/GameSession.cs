using System;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] JsonLoaderTest jsonLoaderTest;
    
    SettingsManager _settingsManager;

    void Awake ()
    {
        _settingsManager = new SettingsManager();
        _settingsManager.InitializeSettings();

        jsonLoaderTest.CardListSettings = _settingsManager.CardListSettings.Instance;
        jsonLoaderTest.LogCard();
    }
}
