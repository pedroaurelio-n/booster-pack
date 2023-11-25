using DG.Tweening;

public static class GameGlobalSettings
{
    public static class CardAnimation
    {
        public const Ease ROTATION_EASING = Ease.Linear;
        public const Ease RESET_EASING = Ease.OutSine;
        
        public const float ROTATION_DURATION = 8f;
        public const float RESET_DURATION = 0.5f;
    }
}