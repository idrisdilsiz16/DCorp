/*
    Oyuna ilk giriþte görünecek olan yükleniyot barý olan açýlýþ sayfasý. Yükleme çok hýzlý gerçekleþtiði için fazla görünmüyor.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceenScript : MonoBehaviour
{
    public static LoadingSceenScript instance;
    public GameObject loadingBarObj;
    public Image loadingBar;
    public static int sceneId = 1;
    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        LoadScene(sceneId);
    }
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(sceneId);

        loadingBarObj.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/ 0.9f);
            loadingBar.fillAmount = progress;
            yield return null;
        }
    }
}
