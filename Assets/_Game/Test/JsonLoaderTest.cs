using UnityEngine;

public class JsonLoaderTest : MonoBehaviour
{
    public ICardListSettings CardListSettings { get; set; }

    public void LogCard ()
    {
        Debug.Log(CardListSettings.Cards[0].ToString());
    }
}