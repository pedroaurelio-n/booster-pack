using UnityEngine;
using DG.Tweening;

public class CardAnimationView : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    
    Tween _rotateTween;
    Tween _scaleTween;
    
    Transform _transform;
    Vector3 _initialScale;
    
    CardAnimationOptions _options;
    
    public void Initialize ()
    {
        _options = GameGlobalOptions.Instance.CardAnimation;
        
        _transform = targetTransform != null ? targetTransform : transform;
        _initialScale = _transform.localScale;
    }

    public void StartRotationAndResetScale ()
    {
        KillTweens();
        
        _scaleTween = _transform.DOScale(_initialScale, _options.ScaleDuration).SetEase(_options.ScaleEasing);
        
        _rotateTween = _transform.DORotate(new Vector3(0, 360f, 0), _options.RotationDuration)
            .SetRelative().SetLoops(-1, LoopType.Incremental).SetEase(_options.RotationEasing);
    }

    public void ResetRotationAndZoomIn ()
    {
        KillTweens();
        
        _rotateTween = _transform.DORotate(Vector3.zero, _options.ResetDuration).SetEase(_options.ResetEasing);
        _scaleTween = _transform.DOScale(_initialScale * _options.ScaleMultiplier, _options.ScaleDuration)
            .SetEase(_options.ScaleEasing);
    }

    public void StopRotation ()
    {
        KillTweens();

        _transform.rotation = Quaternion.Euler(Vector3.zero);
        _transform.localScale = _initialScale;
    }

    void KillTweens ()
    {
        _rotateTween?.Kill();
        _scaleTween?.Kill();
    }
}
