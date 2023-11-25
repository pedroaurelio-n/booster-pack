using UnityEngine;
using DG.Tweening;
using static GameGlobalSettings.CardAnimation;

public class CardAnimationView : MonoBehaviour
{
    Tween rotateTween;

    public void StartRotation ()
    {
        rotateTween = transform.DORotate(new Vector3(0, 360f, 0), ROTATION_DURATION)
            .SetRelative().SetLoops(-1, LoopType.Incremental).SetEase(ROTATION_EASING);
        rotateTween.Play();
    }

    public void ResetRotation ()
    {
        rotateTween?.Kill();
        rotateTween = transform.DORotate(Vector3.zero, RESET_DURATION).SetEase(RESET_EASING);
    }

    public void StopRotation ()
    {
        rotateTween?.Kill();
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
