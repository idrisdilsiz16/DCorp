/*
    Oyun i�erisinde esc butonuna bas�ld���nda a��lan men� scripti.
    Script �al��t���nda kay�tl� ayarlar varsa onlar al�n�p, e�er ayarlar de�i�tirilirse yeni ayarlar kaydediliyor.

    Herhangi bir g�rev tamamland��� zaman a��lan Tebrikler panelindeki ��k�� butonunun �al��t�raca�� metot da en altta bulunmakta.
 */
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class inGameMenu : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider mainSlider,
                  sfxSlider,
                  musicSlider,
                  mouseSlider;

    public TMP_Text mainLevel,
                    sfxLevel,
                    musicLevel,
                    mouseLevel;

    private void Start()
    {
        SetMixer();
        SetSliders();
        SetLevelTexts();
    }
    void SetMixer()
    {
        mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("mainMusicLevel"));
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musicLevel"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("sfxLevel"));
    }
    void SetLevelTexts()
    {
        mainLevel.text = ((int)PlayerPrefs.GetFloat("mainMusicLevel") + 80).ToString();
        sfxLevel.text = ((int)PlayerPrefs.GetFloat("sfxLevel") + 80).ToString();
        musicLevel.text = ((int)PlayerPrefs.GetFloat("musicLevel") + 80).ToString();
        mouseLevel.text = ((int)PlayerPrefs.GetFloat("mouseLevel")).ToString();
    }
    void SetSliders()
    {
        mainSlider.value = PlayerPrefs.GetFloat("mainMusicLevel", 0f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxLevel", 0f);
        musicSlider.value = PlayerPrefs.GetFloat("musicLevel", 0f);
        mouseSlider.value = PlayerPrefs.GetFloat("mouseLevel", 10f);
    }
    public void UpdateMainLevel()
    {
        mixer.SetFloat("MasterVolume", mainSlider.value);
        PlayerPrefs.SetFloat("mainMusicLevel", mainSlider.value);
        SetLevelTexts();
    }
    public void UpdateMusicLevel()
    {
        mixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("musicLevel", musicSlider.value);
        SetLevelTexts();
    }
    public void UpdateSFXLevel()
    {
        mixer.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("sfxLevel", sfxSlider.value);
        SetLevelTexts();
    }
    public void UpdateMouseLevel()
    {
        PlayerPrefs.SetFloat("mouseLevel", mouseSlider.value);
        SetLevelTexts();
    }
    public void ExitButton()
    {
        TutorialScript.instance.saveActivePageNumber();
        PlayerControl.instance.SaveTransfom();
        LoadingSceenScript.sceneId = 1;
        SceneManager.LoadScene(0);
    }

    public void WellDonePanelExit() // g�rev bittikten sonra a��lan tebrikler panelinden ��karken e�er e�itim devam ediyorsa bir sonraki e�itim sayfas�na ge�iyoruz.
    {                               // e�itim devam etmiyorsa sadeece tebrikler paneli kapat�l�yor.
        if (GameManager.instance.isTutorialOn)
        {
            TutorialScript.instance.NextPage();
            GameManager.instance.isComplated = !GameManager.instance.isComplated;
        }
        else
        {
            GameManager.instance.isComplated = !GameManager.instance.isComplated;
            GameManager.instance.gameOn = true;
        }
    }
}
