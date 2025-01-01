using UnityEngine;
using DG.Tweening;
using static GameGlobalSettings.CardAnimation;

public class CardAnimationView : MonoBehaviour, IMouseInteractable
{
    [SerializeField] Transform targetTransform;
    
    Tween _rotateTween;
    Tween _scaleTween;

    //TODO figure out dependency
    CardView _cardView;
    Transform _transform;
    Vector3 _initialScale;

    public void Initialize (CardView cardView)
    {
        _transform = targetTransform != null ? targetTransform : transform;

        _cardView = cardView;
        _transform = transform;
        _initialScale = _transform.localScale;
    }

    public void StartRotationAndResetScale ()
    {
        KillTweens();
        
        _scaleTween = _transform.DOScale(_initialScale, SCALE_DURATION).SetEase(SCALE_EASING);
        
        _rotateTween = _transform.DORotate(new Vector3(0, 360f, 0), ROTATION_DURATION)
            .SetRelative().SetLoops(-1, LoopType.Incremental).SetEase(ROTATION_EASING);
    }

    public void ResetRotationAndZoomIn ()
    {
        KillTweens();
        
        _rotateTween = _transform.DORotate(Vector3.zero, RESET_DURATION).SetEase(RESET_EASING);
        _scaleTween = _transform.DOScale(_initialScale * SCALE_MULTIPLIER, SCALE_DURATION).SetEase(SCALE_EASING);
    }

    public void StopRotation ()
    {
        KillTweens();

        _transform.rotation = Quaternion.Euler(Vector3.zero);
        _transform.localScale = _initialScale;
    }

    //TODO move to CardView
    public void OnEnter ()
    {
        _cardView.PlayParticles();
        ResetRotationAndZoomIn();
    }

    public void OnExit ()
    {
        _cardView.StopParticles();
        StartRotationAndResetScale();
    }

    void KillTweens ()
    {
        _rotateTween?.Kill();
        _scaleTween?.Kill();
    }
}
