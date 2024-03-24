using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void TaskOnClick()
    {
        LoadingSceenScript.sceneId = 1;
        SceneManager.LoadScene(0);
    }
}