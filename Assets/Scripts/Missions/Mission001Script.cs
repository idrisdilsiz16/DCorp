/*
    Herbir g�revin tamamlanmas� ve e�itim aktifse gerekli direktifleri vermesi i�in gereken script.
 */


using UnityEngine;

public class Mission001Script : MonoBehaviour
{
    public static Mission001Script instance;
    public int matchComplated;
    public int matchsToBeDone;
    public bool isDone;
    public GameObject[] tutorialPages;
    public int activePage;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (GameManager.instance.isTutorialOn)          // e�er e�itim aktifse 
        {
            TutorialScript.instance.NextPage();         // arka planda devam eden e�itim script ak���n�n devam�n� sa�l�yoruz.
            tutorialPages[activePage].SetActive(true);  // ve aktif g�rev ekran�nda s�radaki bilgilendirmeyi yap�yoruz.
        }

        matchsToBeDone = transform.GetChild(1).childCount;  // G�revde ne kadar e�le�tirme oldu�u bilgisini al�yoruz.
        isDone = false;                                     // g�rev bitti bilgisi.
    }
    private void Update()
    {
        if(matchComplated >= matchsToBeDone && !isDone)     // E�le�tirme say�lar�n� kontrol ederek g�revin devaml�l��� g�zlemleniyor.
        {
            matchComplated = 0;
            missionScript.instance.MissionAccomplished();   // E�le�tirmeler tamamland�ysa aktif g�rev objesi yok ediliyor ve g�rev bitince �al��mas� gereken fonksiyon �a�r�l�yor.
            Destroy(gameObject);
            isDone = true;
        }
    }
    public void nextButton() // g�rev i�i bilgilendirme sayfalar�n�n ve arka plandaki e�itim script ak���n�n devam� i�in buton metotlar�
    {
        tutorialPages[activePage].SetActive(false);
        activePage++;
        tutorialPages[activePage].SetActive(true);
        TutorialScript.instance.NextPage();
    }
    public void doneButton() // g�rev i�i bilgilendirme sayfalar�n�n ve arka plandaki e�itim script ak���n�n devam� i�in buton metotlar�
    {
        tutorialPages[activePage].SetActive(false);
        TutorialScript.instance.doTheMission();
    }
}
