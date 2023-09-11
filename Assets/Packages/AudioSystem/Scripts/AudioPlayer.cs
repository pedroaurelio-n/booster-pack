using System;
using System.Collections;
using UnityEngine;
 
namespace PedroAurelio.AudioSystem
{
    public class AudioPlayer : MonoBehaviour
    {
        public bool IsLooping { get => _isLooping; set => _isLooping = value; }
        public bool HasAddedInstance { get => _hasAddedInstance; set => _hasAddedInstance = value; }

        private AudioManager _manager;
        private AudioSource _audioSource;
        private AudioClipSO _clipSO;
        private AudioClip _clip;

        private int _id;
        private bool _isPlaying;
        private bool _isLooping;
        private bool _hasAddedInstance;
        private bool _hasRemovedInstance;

        private void Awake() => _audioSource = GetComponent<AudioSource>();

        public void DisableAudioPlayer()
        {
            _audioSource.playOnAwake = false;
            _audioSource.enabled = false;
        }

        public void Setup(AudioManager manager)
        {
            _manager = manager;
            _hasRemovedInstance = false;
        }

        public void Setup(AudioManager manager, AudioClipSO clipSO, Vector3 position)
        {
            _manager = manager;
            _hasRemovedInstance = false;

            _clipSO = clipSO;
            _clip = _clipSO.GetClip();

            if (_clip == null)
                return;

            transform.position = position;

            _audioSource.clip = _clip;
            _audioSource.loop = _clipSO.Loop;
            _audioSource.spatialBlend = _clipSO.SpatialBlend;
            _audioSource.pitch = clipSO.Pitch;
            _audioSource.outputAudioMixerGroup = _clipSO.MixerGroup;
        }

        public void PlayAudio(float fadeInDuration, Action releaseAction)
        {
            _audioSource.enabled = true;

            if (fadeInDuration == 0f)
                _audioSource.volume = _clipSO.Volume;
            else
            {
                _audioSource.volume = 0f;
                StartCoroutine(FadeVolumeCoroutine(_clipSO.Volume, fadeInDuration));
            }

            _audioSource.Play();

            _isPlaying = true;
            
            if (!_audioSource.loop && !_isLooping)
                StartCoroutine(StopAfterClipCoroutine(releaseAction));
        }

        private IEnumerator StopAfterClipCoroutine(Action releaseAction)
        {
            yield return new WaitForSeconds(_clip.length);
            
            DisableAudio(releaseAction);
        }

        public void StopAudio(float fadeDuration, Action releaseAction)
        {
            if (!_isPlaying)
                return;

            if (!gameObject.activeInHierarchy)
            {
                DisableAudio(releaseAction);
                return;
            }
            
            if (fadeDuration <= 0f)
                DisableAudio(releaseAction);
            else
                StartCoroutine(FadeVolumeCoroutine(0f, fadeDuration, releaseAction));
        }

        public void PauseAudio(bool value)
        {
            if (_clipSO == null || !_clipSO.Pausable)
                return;

            _audioSource.Pause(value);
        }

        private IEnumerator FadeVolumeCoroutine(float targetVolume, float fadeDuration, Action releaseAction = null)
        {
            var timeElapsed = 0f;
            var startVolume = _audioSource.volume;

            while (timeElapsed < fadeDuration)
            {
                _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, timeElapsed / fadeDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _audioSource.volume = targetVolume;

            if (targetVolume == 0f)
                DisableAudio(releaseAction);
        }

        private void DisableAudio(Action releaseAction)
        {
            RemoveClipInstance();
            _clipSO = null;
            _isPlaying = false;

            releaseAction.Invoke();
        }

        public IEnumerator ResetParent()
        {
            yield return null;
            transform.parent = _manager.transform;
        }

        private void RemoveClipInstance()
        {
            if (_clipSO == null || _hasRemovedInstance)
                return;

            _clipSO.RemoveInstance();
            _hasRemovedInstance = true;
        }

        private void OnDisable()
        {
            RemoveClipInstance();

            if (_manager == null)
                return;
            
            if (transform.parent != _manager.transform)
                AudioManager.Instance.StartCoroutine(ResetParent());
        }
    }
}