using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : PoolableView
{
    [field: Header("Components")]
    [field: SerializeField] public CardAnimationView CardAnimation { get; private set; }
    [SerializeField] ParticleSystem rarityParticles;
    
    //TODO move into a scriptable object or settings class
    [Header("Colors")]
    [SerializeField] List<CardTypeColors> typeColors;
    [SerializeField] List<CardRarityColors> rarityColors;
    
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

    CardRarity _rarity;

    public void PlayParticles ()
    {
        Color selectedColor = rarityColors.FirstOrDefault(x => x.Rarity == _rarity).Color;

        if (selectedColor == default)
            throw new InvalidOperationException($"Desired rarity {_rarity} doesn't have a color configured.");

        ParticleSystem.MainModule main = rarityParticles.main;
        main.startColor = selectedColor;
        rarityParticles.Play();
    }

    public void StopParticles () => rarityParticles.Stop();

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

    public void SetColor (CardType type)
    {
        Color selectedColor = typeColors.FirstOrDefault(x => x.Type == type).Color;

        if (selectedColor == default)
            throw new InvalidOperationException($"Desired type {type} doesn't have a color configured.");
        
        foreach (Image element in coloredElements)
        {
            Color alphalessColor = new(selectedColor.r, selectedColor.g, selectedColor.b, element.color.a);
            element.color = alphalessColor;
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

    //TODO ideally not needed
    public void SetRarity (CardRarity rarity) => _rarity = rarity;

    public void SetActiveState (bool value) => gameObject.SetActive(value);

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