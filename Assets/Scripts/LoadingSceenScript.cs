/*
    Oyuna ilk giri�te g�r�necek olan y�kleniyot bar� olan a��l�� sayfas�. Y�kleme �ok h�zl� ger�ekle�ti�i i�in fazla g�r�nm�yor.
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
