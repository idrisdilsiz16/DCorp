/*
    Aktif edilen odanýn kapýsýnda bulunan alarm lambalarýnýn davranýþlarýný kontrol eden script.
 */
using UnityEngine;

public class alarmLamps : MonoBehaviour
{
    private string myName;
    private bool isThisRoomActive = false;
    private GameObject myLights;
    Renderer myRenderer;

    private AudioSource alarmSound;

    private float rotSpeed = 200.0f;

    private void Start()
    {
        alarmSound = GetComponent<AudioSource>();
        myRenderer = GetComponent<Renderer>();
        myLights = transform.GetChild(0).gameObject;
        myName = transform.root.gameObject.name;
    }
    void Update()
    {
        if (GameManager.instance.isActiveMission)
        {
            if (myName == GameManager.instance.activeRoom)
            {
                isThisRoomActive = true;
                myRenderer.material.SetColor("_EmissionColor", new Color(1f, 0f, 0.02f, 1f));
            }
            else
            {
                isThisRoomActive = false;
                myRenderer.material.SetColor("_EmissionColor", new Color(1f, 0.65f, 0f, 1f));
            }
        }
        else
        {
            isThisRoomActive = false;
            myRenderer.material.SetColor("_EmissionColor", new Color(0.15f, 1f, 0f, 1f));
        }
        
        myLights.SetActive(isThisRoomActive);

        if (GameManager.instance.gameOn)
        {
            if (isThisRoomActive && !alarmSound.isPlaying)
                alarmSound.Play();
            else if (!isThisRoomActive && alarmSound.isPlaying)
                alarmSound.Stop();
        }
        else
            alarmSound.Stop();

        Rotate();
    }
    private void Rotate()
    {
        if(isThisRoomActive)
            transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
    }
}
