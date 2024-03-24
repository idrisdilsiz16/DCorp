/*
    Oyun i�erisindeki a��l�r kapan�r kap�lar� kontrol eden script.
 */

using UnityEngine;

public class doorAnimation : MonoBehaviour
{
    public static doorAnimation instance;
    public AudioSource audioSource;
    public Animator myAnim;
    private string myName;
    private bool isThisDoorActive = false;
    public bool isFirstMission = false;

    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource =GetComponent<AudioSource>();
        myName = transform.root.gameObject.name; // objenin bulundu�u parent objenin ismini al�yoruz.
        myAnim = transform.GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if(GameManager.instance.isActiveMission)            // e�er aktif g�rev varsa
        {
            if (myName == GameManager.instance.activeRoom)  // ve GameManager taraf�ndan atanan oda bu kap�n�n oldu�u oda ise bu kap� aktif edilecek
                isThisDoorActive = true;
            else
                isThisDoorActive = false;
        }
        else                                // aksi durumlarda bu kap� pasif olacak.
            isThisDoorActive = true;
    }

    private void OnTriggerEnter(Collider other) // e�er bu kap� aktif ve Oyuncu ile etkile�imi girdiyse kap� a��lacak
    {
        if (other.gameObject.CompareTag("Player") && isThisDoorActive)
        {
            myAnim.SetBool("isDoorOpen", true);
            audioSource.Play();
        }
    }
    
    private void OnTriggerExit(Collider other) // oyuncu kap� alan�ndan ��kt���nda kap� kapanacak
    {
        myAnim.SetBool("isDoorOpen", false);
    }
}
