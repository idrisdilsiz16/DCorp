/*
    Oyuncunun hareketlerinin, animasyonlar�n�n, ayak seslerinin ve tetiklemelerin yap�ld��� script.
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
        GetTransform(); // Kaydedilmi� ya da varsay�lan transform de�erlerini oyuncuya at�yoruz.
        myAnim = transform.GetChild(1).GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Rotate();

        if (GameManager.instance.gameOn)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) // Klavye tu�lar�na g�re oyuncuya animasyon tetiklemeleri yap�yoruz.
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
            if (Input.GetKeyDown(KeyCode.E) && inPanelArea) // e�er oyuncu g�rev alan�ndaysa e tu�una bas�larak g�reve ba�l�yoruz.
            {
                myAnim.SetBool("isPushButton", true);
                GameManager.instance.missionSet = true;
                GameManager.instance.gameOn = false;
            }
            else
                myAnim.SetBool("isPushButton", false);  // animasyon tetiklemeleri sonu.
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isEscPressed) // ESC tu�una bas�ld���nda oyunu durdurup tekrar bas�ld���nda devam ettiriyoruz.
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
        if(GameManager.instance.gameOn) // Klavye tu�lar�yla oyuncu hareketi sa�lan�yor
        {
            float moveX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
            float moveY = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;
            transform.Translate(new Vector3(moveX, 0, moveY));
        }
    }
    void Rotate() // Fare hareketiyle oyuncuyu d�nd�r�yoruz.
    {
        if (GameManager.instance.gameOn)
        {
            float rotateObj = Input.GetAxis("Mouse X") * mouseSensivity;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotateObj, 0);
        }
    }

    void GetTransform() // PlayerPrefte kay�tl� transform de�erleri varsa oyun ba�lang�c�nda o de�erleri oyuncuya at�yoruz. Yoksa varsay�lan de�erleri at�yoruz. 
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
    public void SaveTransfom() // PlayerPrefe oyuncunun transform de�erlerini kaydediyoruz.
    {
        PlayerPrefs.SetFloat("positionX", transform.position.x);
        PlayerPrefs.SetFloat("positionY", transform.position.y);
        PlayerPrefs.SetFloat("positionZ", transform.position.z);

        PlayerPrefs.SetFloat("rotationX", transform.rotation.x);
        PlayerPrefs.SetFloat("rotationY", transform.rotation.y);
        PlayerPrefs.SetFloat("rotationZ", transform.rotation.z);
    }

    private void OnTriggerEnter(Collider other) // g�rev tamamland�ktan sonra ofise geri d�n�ld���nde yeni g�rev aktif ediliyor.
    {
        if(other.gameObject.name == "officeTrigger" && !GameManager.instance.isTutorialOn)
        {
            GameManager.instance.isActiveMission = true;
            GameManager.instance.isSpawned = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.parent.gameObject.name == "TutorialColliders" && GameManager.instance.isTutorialOn) // e�itim aktifse g�sterilecek bilgilendirme panellerinin
        {                                                                                                      // do�ru yerlerde gelmesini sa�layan trigger metotu
            TutorialScript.instance.NextPage();

            other.transform.parent.GetChild(1).gameObject.SetActive(true); // bir sonraki trigger aktif hale getirilip aktif trigger yok ediliyor.
            Destroy(other.gameObject);

        }
    }
    private void FootStepSound() // Oyuncu harek halindeyken ayak sesi �al�nmas� 
    {
        if (Time.time - lastFootstepTime > footstepRate)
        {
            lastFootstepTime = Time.time;
            audioSource.PlayOneShot(footstepsClips[Random.Range(0, footstepsClips.Length)]);
        }

    }

}
