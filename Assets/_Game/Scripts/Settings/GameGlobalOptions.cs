using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "GameGlobalOptions", menuName = "GameGlobalOptions")]
public class GameGlobalOptions : ScriptableObject
{
    [field: SerializeField] public CardAnimationOptions CardAnimation { get; private set; }
    [field: SerializeField] public CardColorsOptions CardColors { get; private set; }
    [field: SerializeField] public LayerMasksOptions LayerMasks { get; private set; }
    [field: SerializeField] public FadeTransitionOptions FadeTransition { get; private set; }

    public static GameGlobalOptions Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            
            _instance = Resources.Load<GameGlobalOptions>("GameGlobalOptions");
            if (_instance == null)
                throw new NullReferenceException($"GameGlobalOptions instance was not found in Resources.");

            return _instance;
        }
    }

    static GameGlobalOptions _instance;
}

[Serializable]
public class CardAnimationOptions
{
    [field: SerializeField] public Ease RotationEasing { get; private set; } = Ease.Linear;
    [field: SerializeField] public Ease ScaleEasing { get; private set; } = Ease.OutSine;
    [field: SerializeField] public Ease ResetEasing { get; private set; } = Ease.OutSine;
        
    [field: SerializeField] public float RotationDuration { get; private set; } = 8f;
    [field: SerializeField] public float ScaleDuration { get; private set; } = 0.25f;
    [field: SerializeField] public float ResetDuration { get; private set; } = 0.5f;

    [field: SerializeField] public float ScaleMultiplier { get; private set; } = 1.20f;
}

[Serializable]
public class CardColorsOptions
{
    [field: SerializeField] public List<CardTypeColors> TypeColors { get; private set; }
    [field: SerializeField] public List<CardRarityColors> RarityColors { get; private set; }
    
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

[Serializable]
public class LayerMasksOptions
{
    [field: SerializeField] public LayerMask InteractableLayers { get; private set; }
}

[Serializable]
public class FadeTransitionOptions
{
    [field: SerializeField] public float FadeDuration { get; private set; } = 0.8f;
}
