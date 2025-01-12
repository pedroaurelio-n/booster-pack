using System;
using DG.Tweening;
using UnityEngine;

public class FadeToBlackManager : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeToBlackObject;

    FadeTransitionOptions Options => GameGlobalOptions.Instance.FadeTransition;
    
    public void FadeIn (Action completeCallback)
    {
        DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 1, Options.FadeDuration)
            .OnComplete(() => completeCallback?.Invoke());
    }
    
    public void FadeOut (Action completeCallback)
    {
        fadeToBlackObject.alpha = 1;
        DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 0, Options.FadeDuration)
            .OnComplete(() => completeCallback?.Invoke());
    }
}