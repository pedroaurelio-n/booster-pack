using Newtonsoft.Json;
using UnityEngine;

public class SettingsProvider<TInterface, TSettings> where TSettings : TInterface
{
    public TInterface Instance { get; }
    
    public SettingsProvider(string jsonName)
    {
        TextAsset json = Resources.Load<TextAsset>($"Settings/{jsonName}");
        TSettings settings = JsonConvert.DeserializeObject<TSettings>(json.text);
        Instance = settings;
    }
}
