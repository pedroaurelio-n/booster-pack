using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : PoolableView, IMouseInteractable
{
    public event Action OnEntered;
    public event Action OnExited;
    
    [field: Header("Components")]
    [field: SerializeField] public CardAnimationView CardAnimation { get; private set; }
    [field: SerializeField] public ParticleSystem RarityParticles { get; private set; }
    
    [field: Header("Contents")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] GameObject[] starIcons;
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] Image artIcon;
    [SerializeField] GameObject attackContainer;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] GameObject defenseContainer;
    [SerializeField] TextMeshProUGUI defenseText;
    [SerializeField] Image[] coloredElements;

    public void SetTitleText (string text) => titleText.text = text;

    public void SetDescriptionText (string text) => descriptionText.text = text;

    public void SetTypeText (CardType type)
    {
        if (type == CardType.Monster)
        {
            typeText.gameObject.SetActive(false);
            return;
        }
        
        typeText.gameObject.SetActive(true);
        typeText.text = $"({type.ToText()})";
    }

    public void SetArtSprite (Sprite sprite) => artIcon.sprite = sprite;

    public void SetColor (Color color)
    {
        foreach (Image element in coloredElements)
        {
            color.a = element.color.a;
            element.color = color;
        }
    }

    public void SetLevel (int? level)
    {
        if (!level.HasValue)
        {
            foreach (GameObject icon in starIcons)
                icon.SetActive(false);
            return;
        }
        
        for (int i = 0; i < starIcons.Length; i++)
        {
            GameObject currentIcon = starIcons[i];
            currentIcon.SetActive(i + 1 <= level.Value);
        }
    }

    public void SetAttack (int? attack)
    {
        if (!attack.HasValue)
        {
            attackContainer.SetActive(false);
            return;
        }
        
        attackContainer.SetActive(true);
        attackText.text = attack.ToString();
    }
    
    public void SetDefense (int? defense)
    {
        if (!defense.HasValue)
        {
            defenseContainer.SetActive(false);
            return;
        }
        
        defenseContainer.SetActive(true);
        defenseText.text = defense.ToString();
    }

    public void SetActiveState (bool value) => gameObject.SetActive(value);
    
    public void OnEnter () => OnEntered?.Invoke();

    public void OnExit () => OnExited?.Invoke();

    [Serializable]
    public struct CardTypeColors
    {
        public CardType Type;
        public Color Color;
    }
    
    [Serializable]
    public struct CardRarityColors
    {
        public CardRarity Rarity;
        public Color Color;
    }
}