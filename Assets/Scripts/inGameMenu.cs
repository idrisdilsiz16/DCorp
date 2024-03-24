/*
    Oyun içerisinde esc butonuna basýldýðýnda açýlan menü scripti.
    Script çalýþtýðýnda kayýtlý ayarlar varsa onlar alýnýp, eðer ayarlar deðiþtirilirse yeni ayarlar kaydediliyor.

    Herhangi bir görev tamamlandýðý zaman açýlan Tebrikler panelindeki çýkýþ butonunun çalýþtýracaðý metot da en altta bulunmakta.
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

    public void WellDonePanelExit() // görev bittikten sonra açýlan tebrikler panelinden çýkarken eðer eðitim devam ediyorsa bir sonraki eðitim sayfasýna geçiyoruz.
    {                               // eðitim devam etmiyorsa sadeece tebrikler paneli kapatýlýyor.
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
