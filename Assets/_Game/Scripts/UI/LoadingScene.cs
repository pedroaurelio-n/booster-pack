using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    const string PERCENTAGE_FORMAT = "{0}%";
    
    [SerializeField] Button startButton;
    [SerializeField] GameObject[] loadingObjects;
    [SerializeField] TextMeshProUGUI loadingNumber;

    int loadingProgress;

    AsyncOperation mainSceneLoad;
    AsyncOperation loadingSceneUnload;
    
    void Awake ()
    {
        if (startButton != null && startButton.gameObject.activeInHierarchy)
        {
            startButton.onClick.AddListener(StartLoading);
            
            foreach (GameObject obj in loadingObjects)
                obj.SetActive(false);
            
            return;
        }
        
        StartLoading();
    }

    void StartLoading ()
    {
        if (startButton != null)
            startButton.gameObject.SetActive(false);
        
        foreach (GameObject obj in loadingObjects)
            obj.SetActive(true);

        loadingNumber.text = string.Format(PERCENTAGE_FORMAT, loadingProgress);

        mainSceneLoad = SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
        StartCoroutine(UpdateLoadingProgress());
    }

    void CompleteLoad ()
    {
        loadingSceneUnload = SceneManager.UnloadSceneAsync("LoadingScene");
    }

    IEnumerator UpdateLoadingProgress ()
    {
        while (!mainSceneLoad.isDone)
        {
            loadingProgress = Mathf.RoundToInt(mainSceneLoad.progress * 100);
            loadingNumber.text = string.Format(PERCENTAGE_FORMAT, loadingProgress);
            yield return null;
        }

        CompleteLoad();
    }
}
