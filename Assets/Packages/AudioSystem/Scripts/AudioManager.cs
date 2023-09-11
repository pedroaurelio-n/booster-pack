using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio.AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Settings")]
        [SerializeField] private int initialPoolCount;

        private List<AudioPlayer> _audioPlayerPool;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);

            _audioPlayerPool = new List<AudioPlayer>();
            InitializePool(initialPoolCount);
        }

        #region Pool Methods
        private AudioPlayer CreateAudioPlayer()
        {
            var newPlayer = new GameObject("AudioPlayer");
            newPlayer.transform.parent = transform;

            newPlayer.AddComponent<AudioSource>();
            var audioPlayer = newPlayer.AddComponent<AudioPlayer>();

            _audioPlayerPool.Add(audioPlayer);
            return audioPlayer;
        }

        private AudioPlayer GetAudioPlayer()
        {
            foreach (AudioPlayer audioPlayer in _audioPlayerPool)
            {
                if (!audioPlayer.gameObject.activeInHierarchy)
                {
                    audioPlayer.gameObject.SetActive(true);
                    return audioPlayer;
                }
            }

            return CreateAudioPlayer();
        }

        private void ReleaseAudioPlayer(AudioPlayer audioPlayer)
        {
            audioPlayer.DisableAudioPlayer();
            audioPlayer.gameObject.SetActive(false);
        }

        private void InitializePool(int count)
        {
            for (int i = 0; i < count; i++)
                CreateAudioPlayer();

            for (int i = _audioPlayerPool.Count - 1; i >= 0; i--)
                ReleaseAudioPlayer(_audioPlayerPool[i]);
        }
        #endregion

        public AudioPlayer RequestAudioPlayer()
        {
            var audioPlayer = GetAudioPlayer();
            audioPlayer.Setup(this);
            return audioPlayer;
        }

        public AudioPlayer RequestAudioPlayer(AudioClipSO clipSO, float fadeInDuration, Vector3 position, float delay, Transform objectToFollow)
        {
            if (!clipSO.CanActivateNewInstance())
                return null;
            
            var audioPlayer = GetAudioPlayer();

            PlayAudioPlayer(audioPlayer, clipSO, fadeInDuration, position, delay, objectToFollow);
            return audioPlayer;
        }

        public void PlayAudioPlayer(AudioPlayer audioPlayer, AudioClipSO clipSO, float fadeInDuration, Vector3 position, float delay, Transform objectToFollow)
        {
            if (!clipSO.CanActivateNewInstance())
                return;

            AddClipInstance(audioPlayer, clipSO);
            audioPlayer.Setup(this, clipSO, position);

            if (objectToFollow != null)
                audioPlayer.transform.parent = objectToFollow;

            StartCoroutine(CheckAudioDelay(audioPlayer, fadeInDuration, delay));
        }

        public void StopAudioPlayer(AudioPlayer audioPlayer, float fadeOutDuration)
        {
            if (audioPlayer == null)
                return;
            
            audioPlayer.IsLooping = false;
            audioPlayer.HasAddedInstance = false;

            audioPlayer.StopAudio(fadeOutDuration, () => ReleaseAudioPlayer(audioPlayer));
        }

        public void PauseAudioPlayers(bool value)
        {
            foreach (AudioPlayer audioPlayer in _audioPlayerPool)
                audioPlayer.PauseAudio(value);
        }

        private IEnumerator CheckAudioDelay(AudioPlayer audioPlayer, float fadeInDuration, float delay)
        {            
            if (delay == 0f)
                audioPlayer.PlayAudio(fadeInDuration, () => ReleaseAudioPlayer(audioPlayer));
            else
            {
                yield return new WaitForSeconds(delay);

                if (audioPlayer.gameObject.activeInHierarchy)
                    audioPlayer.PlayAudio(fadeInDuration, () => ReleaseAudioPlayer(audioPlayer));
            }
        }

        private void AddClipInstance(AudioPlayer audioPlayer, AudioClipSO clipSO)
        {
            if (!audioPlayer.IsLooping && !clipSO.Loop)
            {
                clipSO.AddInstance();
                return;
            }
                
            if (!audioPlayer.HasAddedInstance)
            {
                clipSO.AddInstance();
                audioPlayer.HasAddedInstance = true;
            }
        }
    }
}