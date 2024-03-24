/*
    Oyuncunun hareketlerinin, animasyonlarýnýn, ayak seslerinin ve tetiklemelerin yapýldýðý script.
 */

using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector] public static PlayerControl instance;

    [HideInInspector] public float moveSpeed = 1.0f;
    [HideInInspector] public float mouseSensivity;

    [HideInInspector] public bool isEscPressed = false;
    [HideInInspector] public bool inPanelArea = false;
    [HideInInspector] public bool isWalk = false;

    [HideInInspector] public float posX, posY, posZ, rotX, rotY, rotZ;

    [HideInInspector] private const float footstepRate = 0.5f;
    [HideInInspector] public float lastFootstepTime;

    private Animator myAnim;
    private Rigidbody rb;
    public AudioClip[] footstepsClips;
    public AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetTransform(); // Kaydedilmiþ ya da varsayýlan transform deðerlerini oyuncuya atýyoruz.
        myAnim = transform.GetChild(1).GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Rotate();

        if (GameManager.instance.gameOn)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) // Klavye tuþlarýna göre oyuncuya animasyon tetiklemeleri yapýyoruz.
            {
                myAnim.SetBool("isWalking", true);
                isWalk = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                myAnim.SetBool("isWalkBack", true);
                isWalk = true;
            }
            else
            {
                myAnim.SetBool("isWalking", false);
                myAnim.SetBool("isWalkBack", false);
                isWalk = false;
            }
            if (Input.GetKeyDown(KeyCode.E) && inPanelArea) // eðer oyuncu görev alanýndaysa e tuþuna basýlarak göreve baþlýyoruz.
            {
                myAnim.SetBool("isPushButton", true);
                GameManager.instance.missionSet = true;
                GameManager.instance.gameOn = false;
            }
            else
                myAnim.SetBool("isPushButton", false);  // animasyon tetiklemeleri sonu.
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isEscPressed) // ESC tuþuna basýldýðýnda oyunu durdurup tekrar basýldýðýnda devam ettiriyoruz.
        {
            GameManager.instance.gameOn = false;
            GameManager.instance.inGameMenuSet = true;
            isEscPressed = !isEscPressed;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isEscPressed)
        {
            GameManager.instance.gameOn = true;
            GameManager.instance.inGameMenuSet = false;
            isEscPressed = !isEscPressed;   // ESC sonu.
        }
    }
    private void FixedUpdate()
    {
        mouseSensivity = PlayerPrefs.GetFloat("mouseLevel", 30f);

        if (isWalk) // Ayak sesi efekti
        {
            if (Time.time - lastFootstepTime > footstepRate)
            {
                lastFootstepTime = Time.time;
                audioSource.PlayOneShot(footstepsClips[Random.Range(0, footstepsClips.Length)]);
            }
        }
    }

    void Move()
    {
        if(GameManager.instance.gameOn) // Klavye tuþlarýyla oyuncu hareketi saðlanýyor
        {
            float moveX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
            float moveY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
            transform.Translate(new Vector3(moveX, 0, moveY));
        }
    }
    void Rotate() // Fare hareketiyle oyuncuyu döndürüyoruz.
    {
        if (GameManager.instance.gameOn)
        {
            float rotateObj = Input.GetAxis("Mouse X") * mouseSensivity;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotateObj, 0);
        }
    }

    void GetTransform() // PlayerPrefte kayýtlý transform deðerleri varsa oyun baþlangýcýnda o deðerleri oyuncuya atýyoruz. Yoksa varsayýlan deðerleri atýyoruz. 
    {
        posX = PlayerPrefs.GetFloat("positionX", -10);
        posY = PlayerPrefs.GetFloat("positionY", 1);
        posZ = PlayerPrefs.GetFloat("positionZ", -10);
        Vector3 posVector = new Vector3(posX, posY, posZ);
        transform.position = posVector;

        rotX = PlayerPrefs.GetFloat("rotationX", 0);
        rotY = PlayerPrefs.GetFloat("rotationY", 270);
        rotZ = PlayerPrefs.GetFloat("rotationZ", 0);
        transform.rotation = Quaternion.Euler(rotX,rotY,rotZ);
    }
    public void SaveTransfom() // PlayerPrefe oyuncunun transform deðerlerini kaydediyoruz.
    {
        PlayerPrefs.SetFloat("positionX", transform.position.x);
        PlayerPrefs.SetFloat("positionY", transform.position.y);
        PlayerPrefs.SetFloat("positionZ", transform.position.z);

        PlayerPrefs.SetFloat("rotationX", transform.rotation.x);
        PlayerPrefs.SetFloat("rotationY", transform.rotation.y);
        PlayerPrefs.SetFloat("rotationZ", transform.rotation.z);
    }

    private void OnTriggerEnter(Collider other) // görev tamamlandýktan sonra ofise geri dönüldüðünde yeni görev aktif ediliyor.
    {
        if(other.gameObject.name == "officeTrigger" && !GameManager.instance.isTutorialOn)
        {
            GameManager.instance.isActiveMission = true;
            GameManager.instance.isSpawned = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.parent.gameObject.name == "TutorialColliders" && GameManager.instance.isTutorialOn) // eðitim aktifse gösterilecek bilgilendirme panellerinin
        {                                                                                                      // doðru yerlerde gelmesini saðlayan trigger metotu
            TutorialScript.instance.NextPage();

            other.transform.parent.GetChild(1).gameObject.SetActive(true); // bir sonraki trigger aktif hale getirilip aktif trigger yok ediliyor.
            Destroy(other.gameObject);

        }
    }
    private void FootStepSound() // Oyuncu harek halindeyken ayak sesi çalýnmasý 
    {
        if (Time.time - lastFootstepTime > footstepRate)
        {
            lastFootstepTime = Time.time;
            audioSource.PlayOneShot(footstepsClips[Random.Range(0, footstepsClips.Length)]);
        }

    }

}
