using System;
using UnityEngine;
using VContainer.Unity;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameLifetimeScope lifetimeScope;
    
    [SerializeField] SpawnCardUIView spawnCardUIView;
    [SerializeField] CardUIView cardUIViewPrefab;
    
    SettingsManager _settingsManager;

    void Awake ()
    {
        _settingsManager = new SettingsManager();
        _settingsManager.InitializeSettings();

        GameInstaller installer = new(
            _settingsManager.CardListSettings.Instance,
            spawnCardUIView,
            cardUIViewPrefab
        );
        LifetimeScope scope = lifetimeScope.CreateChild(installer);
    }
}
