/*
    Oyunun ilk açýlýþýnda karþýmýza çýkan menü ekraný scripti.    
    
    Eðer kayýtlý oyuncu varsa devam butonu ile devam ediyoruz. yoksa yeni oyun butonu ile oyuna baþlýyoruz.
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

        if(PlayerPrefs.HasKey("playerName"))    // eðer kayýtlý bir oyuncu varsa Yeni Oyun butonu deaktif edilip devam butonu aktif hale getiriliyor
        {
            newGameButton.interactable = false;
            continueButton.interactable = true;
        }
        else                                    // eðer kayýtlý oyuncu yoksa yeni oyun butonu aktif hale getirilip devam butonu deaktif ediliyor.
        {
            newGameButton.interactable = true;
            continueButton.interactable = false;
        }
    }
    public void newGame() // yeni oyun butonuna basýlýnca oyuncunun adýnýn girileceði panel açýlýyor.
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
