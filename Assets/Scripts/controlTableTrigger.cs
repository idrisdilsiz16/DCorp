/*
    G�rev odalar�ndaki kontrol masalar�n�n davran��lar�n� kontrol eden script.
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
        if (GameManager.instance.isActiveMission) // e�er aktif g�rev varsa ve bu kontrol masas� GameManager taraf�ndan atanan masaysa bu masay� aktif ediyoruz ve 
        {                                                       // masa �zerindeki aktif g�rev lambas�n�n rengini ayarl�yoruz.
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

    private void OnTriggerEnter(Collider other) // oyuncu masan�n ba��ndaysa hangi butona basmas� gerekti�ini g�steren objeyi aktif ediyoruz.
    {
        if (other.gameObject.CompareTag("Player") && isThisTableActive)
        {
            buttonObj.SetActive(true);
            PlayerControl.instance.inPanelArea = true; // g�reve ba�layabilmesi i�in gerekli olan de�i�keni at�yoruz.
        }
    }
    private void OnTriggerExit(Collider other)
    {
        buttonObj.SetActive(false);
        PlayerControl.instance.inPanelArea = false;
    }
}
