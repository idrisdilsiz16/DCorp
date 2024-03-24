/*
    E�le�tirme g�revlerinde slotlarda kullan�lan script. 
 */


using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IDropHandler
{
    public static ItemSlotScript instance;
    public int childNumber;
    public AudioClip[] sounds;
    private AudioSource audioSource;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        childNumber = transform.childCount; // Slot objesinin alt objelerinin say�s�n� al�yoruz.
    }
    public void OnDrop(PointerEventData eventData) // Slot �zerindeyken fare t�klamas� b�rak�ld���nda neler olaca��n� belirliyoruz.
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.name == transform.name) // T�k b�rak�ld���nda herhangi bir obje var m�, varsa obje ismi ile slot ismi ayn� m� diye kontrol ediyoruz.
        {
            playSound(0);
            Mission001Script.instance.matchComplated++;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; // B�rak�lan objeyi slot objesine ortal�yoruz.

            for (int x = 0; x < childNumber; x++)
            {
                transform.GetChild(x).gameObject.SetActive(true); // Slot objesi i�erisinde ne kadar alt obje varsa hepsini aktif hale getiriyoruz.
            }
        }
        else
        {
            playSound(1);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = DragAndDropScript.originalPosition; // B�rak�lan objeyi orjinal pozisyonuna geri yolluyoruz.
        }
        
    }
    public void playSound(int sel) // Do�ru ya da yanl�� e�le�tirme seslerini d��ar�dan eri�erek �alabilmek i�in bir methot yazd�k.
    {
        audioSource.PlayOneShot(sounds[sel]);
    }
}
