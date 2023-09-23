using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardView : PoolableView
{
    [field: SerializeField] public BackgroundColors Colors { get; private set; }
    
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

    Tween rotateTween;

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
            Color alphalessColor = new(color.r, color.g, color.b, element.color.a);
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

    public void SetActiveState (bool value)
    {
        rotateTween?.Kill();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.SetActive(value);

        if (value)
        {
            rotateTween = transform.DORotate(new Vector3(0, 360f, 0), 6f)
                .SetRelative().SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            rotateTween.Play();
        }
    }

    [Serializable]
    public struct BackgroundColors
    {
        public Color MonsterColor;
        public Color MagicColor;
    }
}