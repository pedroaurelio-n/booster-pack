using Newtonsoft.Json;
using UnityEngine;

public class SettingsProvider<TInterface, TClass> where TClass : TInterface
{
    public TInterface Instance { get; }
    
    public SettingsProvider(string jsonName)
    {
        TextAsset json = Resources.Load<TextAsset>($"Settings/{jsonName}");
        TClass settings = JsonConvert.DeserializeObject<TClass>(json.text);
        Instance = settings;
    }
}
