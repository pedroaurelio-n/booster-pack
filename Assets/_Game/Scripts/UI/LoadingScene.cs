using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    const string PERCENTAGE_FORMAT = "{0}%";
    const float LOADING_SPEED = 0.5f;
    const float FADE_DURATION = 0.8f;
    
    [SerializeField] GameObject[] loadingSceneObjects;
    [SerializeField] GameObject[] loadingUIObjects;
    [SerializeField] CanvasGroup fadeToBlackObject;
    [SerializeField] Button startButton;
    [SerializeField] TextMeshProUGUI loadingNumber;
    [SerializeField] Image loadingFillBar;

    float _loadingProgress;
    float _targetProgress;

    AsyncOperation _mainSceneLoad;
    AsyncOperation _loadingSceneUnload;
    
    void Awake ()
    {
        if (startButton != null && startButton.gameObject.activeInHierarchy)
        {
            startButton.onClick.AddListener(StartLoading);
            
            foreach (GameObject obj in loadingUIObjects)
                obj.SetActive(false);
            
            return;
        }
        
        StartLoading();
    }

    void StartLoading ()
    {
        if (startButton != null)
            startButton.gameObject.SetActive(false);
        
        foreach (GameObject obj in loadingUIObjects)
            obj.SetActive(true);

        UpdateLoadingUI();

        _mainSceneLoad = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        StartCoroutine(UpdateLoadingProgress(_mainSceneLoad, null));
    }

    void CompleteLoad ()
    {
        foreach (GameObject obj in loadingSceneObjects)
            obj.SetActive(false);
        
        FadeOut();
    }

    IEnumerator UpdateLoadingProgress (AsyncOperation loadOperation, AsyncOperation unloadOperation)
    {
        loadOperation.allowSceneActivation = false;
        
        while (_loadingProgress < 100)
        {
            float loadProgress = loadOperation.progress < 0.9f ? loadOperation.progress : 1f;
            float unloadProgress = unloadOperation?.progress ?? 1f;
            float unloadWeight = unloadOperation != null ? 0.5f : 0f;
            float loadWeight = 1f - unloadWeight;
            
            _targetProgress = Mathf.RoundToInt((loadProgress * loadWeight + unloadProgress * unloadWeight) * 100);

            while (_loadingProgress < _targetProgress && _targetProgress != 0)
            {
                _loadingProgress += LOADING_SPEED;
                UpdateLoadingUI();
                yield return null;
            }

            UpdateLoadingUI();
            yield return null;
        }
        
        _loadingProgress = 100;
        UpdateLoadingUI();

        FadeIn().OnComplete(() => loadOperation.allowSceneActivation = true);

        while (!loadOperation.isDone)
            yield return null;
        
        CompleteLoad();
    }

    void UpdateLoadingUI ()
    {
        loadingNumber.text = string.Format(PERCENTAGE_FORMAT, Mathf.RoundToInt(_loadingProgress));
        loadingFillBar.fillAmount = _loadingProgress / 100;
    }
    
    //TODO pedro: isolate fade to it's own class
    Tween FadeIn ()
    {
        return DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 1, FADE_DURATION);
    }
    
    Tween FadeOut ()
    {
        return DOTween.To(() => fadeToBlackObject.alpha, x => fadeToBlackObject.alpha = x, 0, FADE_DURATION);
    }
}
