/*
    Herbir görevin tamamlanmasý ve eðitim aktifse gerekli direktifleri vermesi için gereken script.
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
        if (GameManager.instance.isTutorialOn)          // eðer eðitim aktifse 
        {
            TutorialScript.instance.NextPage();         // arka planda devam eden eðitim script akýþýnýn devamýný saðlýyoruz.
            tutorialPages[activePage].SetActive(true);  // ve aktif görev ekranýnda sýradaki bilgilendirmeyi yapýyoruz.
        }

        matchsToBeDone = transform.GetChild(1).childCount;  // Görevde ne kadar eþleþtirme olduðu bilgisini alýyoruz.
        isDone = false;                                     // görev bitti bilgisi.
    }
    private void Update()
    {
        if(matchComplated >= matchsToBeDone && !isDone)     // Eþleþtirme sayýlarýný kontrol ederek görevin devamlýlýðý gözlemleniyor.
        {
            matchComplated = 0;
            missionScript.instance.MissionAccomplished();   // Eþleþtirmeler tamamlandýysa aktif görev objesi yok ediliyor ve görev bitince çalýþmasý gereken fonksiyon çaðrýlýyor.
            Destroy(gameObject);
            isDone = true;
        }
    }
    public void nextButton() // görev içi bilgilendirme sayfalarýnýn ve arka plandaki eðitim script akýþýnýn devamý için buton metotlarý
    {
        tutorialPages[activePage].SetActive(false);
        activePage++;
        tutorialPages[activePage].SetActive(true);
        TutorialScript.instance.NextPage();
    }
    public void doneButton() // görev içi bilgilendirme sayfalarýnýn ve arka plandaki eðitim script akýþýnýn devamý için buton metotlarý
    {
        tutorialPages[activePage].SetActive(false);
        TutorialScript.instance.doTheMission();
    }
}
