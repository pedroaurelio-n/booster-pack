using System.Collections;
using UnityEngine;
 
namespace PedroAurelio.AudioSystem
{
    public class PlayAudioEvent : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private AudioClipSO clipSO;

        [Header("Play/Stop Settings")]
        [SerializeField] private bool playOnStart;
        [SerializeField] private bool playOnNextEnable;
        [SerializeField] private bool stopOnDisable;

        [Header("Fade Settings")]
        [SerializeField] private float fadeInDuration = 0f;
        [SerializeField] private float fadeOutDuration = 0f;

        [Header("Loop Settings")]
        [SerializeField] private bool playLoop;
        [SerializeField] private float loopInterval = 0.1f;

        [Header("Other Settings")]
        [SerializeField] private Vector2 delayRange = Vector2.zero;
        [SerializeField] private Transform objectToFollow;

        private bool _hasPlayedOnStart;
        private AudioPlayer _audioPlayer;

        private Coroutine _playLoopCoroutine;

        private void OnValidate()
        {
            ClampDelay();
            CheckLoopSettings();
            loopInterval = Mathf.Clamp(loopInterval, 0.1f, float.MaxValue);
        }

        private void ClampDelay()
        {
            delayRange.x = Mathf.Clamp(delayRange.x, 0f, delayRange.y);
            delayRange.y = Mathf.Clamp(delayRange.y, delayRange.x, float.MaxValue);
        }

        private void CheckLoopSettings()
        {
            if (clipSO == null)
                return;
            
            if (clipSO.Loop && playLoop)
            {
                Debug.LogWarning($"Can't use play loop with default AudioSource loop.");
                playLoop = false;
            }
        }

        private void Start()
        {
            if (playOnStart)
            {
                PlayAudio();
                _hasPlayedOnStart = true;
            }
        }

        public void PlayAudio()
        {
            if (playLoop)
                _playLoopCoroutine = StartCoroutine(PlayLoop());
            else
                PlaySingle();
        }

        private void PlaySingle()
        {
            var delay = delayRange == Vector2.zero ? 0f : Random.Range(delayRange.x, delayRange.y);
            _audioPlayer = AudioManager.Instance.RequestAudioPlayer(clipSO, fadeInDuration, transform.position, delay, objectToFollow);

            if (!playLoop && !clipSO.Loop)
                _audioPlayer = null;
        }

        private IEnumerator PlayLoop()
        {
            _audioPlayer = AudioManager.Instance.RequestAudioPlayer();
            var interval = new WaitForSeconds(loopInterval);
            _audioPlayer.IsLooping = true;

            while (true)
            {
                var delay = delayRange == Vector2.zero ? 0f : Random.Range(delayRange.x, delayRange.y);
                AudioManager.Instance.PlayAudioPlayer(_audioPlayer, clipSO, fadeInDuration, transform.position, delay, objectToFollow);
                yield return new WaitForSeconds(delay);
                yield return interval;
            }
        }

        public void StopAudio()
        {
            if (_audioPlayer == null)
                return;
            
            AudioManager.Instance.StopAudioPlayer(_audioPlayer, fadeOutDuration);
            _audioPlayer = null;
        }

        private void OnEnable()
        {
            if (!_hasPlayedOnStart)
                return;

            if (playOnNextEnable)
                PlayAudio();
        }

        private void OnDisable()
        {
            if (stopOnDisable)
                StopAudio();
        }
    }
}