using Newtonsoft.Json;
using UnityEngine;

public class SettingsProvider<T, U> where U : T
{
    public T Instance { get; }
    
    public SettingsProvider(string jsonName)
    {
        TextAsset json = Resources.Load<TextAsset>($"Settings/{jsonName}");
        U settings = JsonConvert.DeserializeObject<U>(json.text);
        Instance = settings;
    }
}
