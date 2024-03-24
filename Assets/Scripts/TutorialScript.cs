/*
    E�itim sayfalar� y�netim scripti
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
        nameText.text = "Merhaba, " + PlayerPrefs.GetString("playerName"); // Giri� sayfas�nda girilen oyuncu ismini kar��lama sayfas�nda kullanmak i�in text'e at�yoruz.
   
        if (GameManager.instance.isTutorialOn && !isTutorialStarted) // Oyun ba�lang�c�nda e�er E�itim aktifse 1 sn i�erisinde bilgilendirmeyi ba�latacak metotu �a��r�yoruz.
            Invoke("StartTutorial", 1f);
    }
    public void StartTutorial()
    {
        GameManager.instance.gameOn = false; // E�itim ba�lad���nda hareketi durdurmak ve imleci aktif etmek i�in gereken de�i�keni ayarl�yoruz.
        pages[activePageNumber].SetActive(true); // gerekli sayfay� aktif ediyoruz.
    }

    #region // Gerekli buton atamalar�
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
