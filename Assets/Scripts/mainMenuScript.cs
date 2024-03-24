/*
    Oyunun ilk a��l���nda kar��m�za ��kan men� ekran� scripti.    
    
    E�er kay�tl� oyuncu varsa devam butonu ile devam ediyoruz. yoksa yeni oyun butonu ile oyuna ba�l�yoruz.
 */

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuScript : MonoBehaviour
{
    public GameObject menuPanel,
                      playerNamePanel;
    public Button newGameButton,
                  continueButton;

    public TMP_InputField playerNameInput;


    void Start()
    {
        playerNamePanel.SetActive(false);

        if(PlayerPrefs.HasKey("playerName"))    // e�er kay�tl� bir oyuncu varsa Yeni Oyun butonu deaktif edilip devam butonu aktif hale getiriliyor
        {
            newGameButton.interactable = false;
            continueButton.interactable = true;
        }
        else                                    // e�er kay�tl� oyuncu yoksa yeni oyun butonu aktif hale getirilip devam butonu deaktif ediliyor.
        {
            newGameButton.interactable = true;
            continueButton.interactable = false;
        }
    }
    public void newGame() // yeni oyun butonuna bas�l�nca oyuncunun ad�n�n girilece�i panel a��l�yor.
    {
        playerNamePanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    public void continueGame()
    {
        LoadingSceenScript.sceneId = 2;
        SceneManager.LoadScene(0);
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void startGame(TMP_InputField playerNameInput)
    {
        PlayerPrefs.SetString("playerName", playerNameInput.text);
        continueGame();
    }
    
}
