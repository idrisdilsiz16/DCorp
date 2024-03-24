/*
    Oyun içerisindeki açýlýr kapanýr kapýlarý kontrol eden script.
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
        myName = transform.root.gameObject.name; // objenin bulunduðu parent objenin ismini alýyoruz.
        myAnim = transform.GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if(GameManager.instance.isActiveMission)            // eðer aktif görev varsa
        {
            if (myName == GameManager.instance.activeRoom)  // ve GameManager tarafýndan atanan oda bu kapýnýn olduðu oda ise bu kapý aktif edilecek
                isThisDoorActive = true;
            else
                isThisDoorActive = false;
        }
        else                                // aksi durumlarda bu kapý pasif olacak.
            isThisDoorActive = true;
    }

    private void OnTriggerEnter(Collider other) // eðer bu kapý aktif ve Oyuncu ile etkileþimi girdiyse kapý açýlacak
    {
        if (other.gameObject.CompareTag("Player") && isThisDoorActive)
        {
            myAnim.SetBool("isDoorOpen", true);
            audioSource.Play();
        }
    }
    
    private void OnTriggerExit(Collider other) // oyuncu kapý alanýndan çýktýðýnda kapý kapanacak
    {
        myAnim.SetBool("isDoorOpen", false);
    }
}
