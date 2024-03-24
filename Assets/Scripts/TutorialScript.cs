/*
    Eðitim sayfalarý yönetim scripti
 */
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public TMP_Text nameText;
    public static TutorialScript instance;
    public int activePageNumber;
    private bool isTutorialStarted = false;
    public GameObject[] pages;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
        activePageNumber = PlayerPrefs.GetInt("activePageNumber", 0);
        nameText.text = "Merhaba, " + PlayerPrefs.GetString("playerName"); // Giriþ sayfasýnda girilen oyuncu ismini karþýlama sayfasýnda kullanmak için text'e atýyoruz.
   
        if (GameManager.instance.isTutorialOn && !isTutorialStarted) // Oyun baþlangýcýnda eðer Eðitim aktifse 1 sn içerisinde bilgilendirmeyi baþlatacak metotu çaðýrýyoruz.
            Invoke("StartTutorial", 1f);
    }
    public void StartTutorial()
    {
        GameManager.instance.gameOn = false; // Eðitim baþladýðýnda hareketi durdurmak ve imleci aktif etmek için gereken deðiþkeni ayarlýyoruz.
        pages[activePageNumber].SetActive(true); // gerekli sayfayý aktif ediyoruz.
    }

    #region // Gerekli buton atamalarý
    public void NextPage()
    {
        GameManager.instance.gameOn = false;
        pages[activePageNumber].SetActive(false);
        activePageNumber++;
        pages[activePageNumber].SetActive(true);
    }
    public void QuitedNextButton()
    {
        pages[activePageNumber].SetActive(false);
        GameManager.instance.gameOn = true;
    }
    public void startFirstMission()
    {
        pages[activePageNumber].SetActive(false);
        GameManager.instance.gameOn = true;
        GameManager.instance.isActiveMission = true;
    }

    public void doTheMission()
    {
        pages[activePageNumber].SetActive(false);
    }
    
    public void finishTutorial()
    {
        pages[activePageNumber].SetActive(false);
        GameManager.instance.isTutorialOn = false;
        GameManager.instance.gameOn = true;
    }

    #endregion

    public void saveActivePageNumber()
    {
        PlayerPrefs.SetInt("activePageNumber", activePageNumber);
    }
}
