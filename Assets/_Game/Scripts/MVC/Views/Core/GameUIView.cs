using System;
using DG.Tweening;
using UnityEngine;

public class GameUIView : MonoBehaviour
{
    const float FADE_DURATION = 0.8f;
    
    [field: SerializeField] public Transform ButtonContainer { get; private set; }
    [field: SerializeField] public Transform ChangeSceneContainer { get; private set; }

    [SerializeField] CanvasGroup fadeToBlackObject;
    
    //TODO pedro: isolate fade to it's own class
    public void FadeIn (Action completeCallback)
    {
        DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 1, FADE_DURATION).OnComplete(() => completeCallback?.Invoke());
    }
    
    public void FadeOut ()
    {
        fadeToBlackObject.alpha = 1;
        DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 0, FADE_DURATION);
    }
}