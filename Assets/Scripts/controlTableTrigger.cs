/*
    Görev odalarýndaki kontrol masalarýnýn davranýþlarýný kontrol eden script.
 */
using UnityEngine;

public class controlTableTrigger : MonoBehaviour
{
    public static controlTableTrigger instance;
    private GameObject buttonObj;
    private ButtonLamp statLamp;
    private bool isThisTableActive = false;
    private string myName;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        myName = transform.parent.gameObject.name;
        statLamp = transform.parent.GetChild(0).GetComponent<ButtonLamp>();
        buttonObj = transform.GetChild(0).gameObject;
        buttonObj.SetActive(false);
    }
    private void Update()
    {
        if (GameManager.instance.isActiveMission) // eðer aktif görev varsa ve bu kontrol masasý GameManager tarafýndan atanan masaysa bu masayý aktif ediyoruz ve 
        {                                                       // masa üzerindeki aktif görev lambasýnýn rengini ayarlýyoruz.
            if (myName == GameManager.instance.activeTable)
                isThisTableActive = true;
            else
                isThisTableActive = false;

            if (isThisTableActive)
                statLamp.lightColor = ButtonLamp.lampColors.Red;
            else
                statLamp.lightColor = ButtonLamp.lampColors.Green;
        }
        else
        {
            isThisTableActive = false;
            statLamp.lightColor = ButtonLamp.lampColors.Green;
        }
    }

    private void OnTriggerEnter(Collider other) // oyuncu masanýn baþýndaysa hangi butona basmasý gerektiðini gösteren objeyi aktif ediyoruz.
    {
        if (other.gameObject.CompareTag("Player") && isThisTableActive)
        {
            buttonObj.SetActive(true);
            PlayerControl.instance.inPanelArea = true; // göreve baþlayabilmesi için gerekli olan deðiþkeni atýyoruz.
        }
    }
    private void OnTriggerExit(Collider other)
    {
        buttonObj.SetActive(false);
        PlayerControl.instance.inPanelArea = false;
    }
}
