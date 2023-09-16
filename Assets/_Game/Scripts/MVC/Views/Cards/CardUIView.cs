using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIView : MonoBehaviour
{
    [field: SerializeField] public BackgroundColors Colors { get; private set; }
    
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Image backgroundImage;

    public void SetTitleText (string text) => titleText.text = text;
    public void SetBackgroundColor (Color color) => backgroundImage.color = color;

    [Serializable]
    public struct BackgroundColors
    {
        public Color MonsterColor;
        public Color MagicColor;
    }
}