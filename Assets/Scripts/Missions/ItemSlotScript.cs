/*
    Eþleþtirme görevlerinde slotlarda kullanýlan script. 
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
        childNumber = transform.childCount; // Slot objesinin alt objelerinin sayýsýný alýyoruz.
    }
    public void OnDrop(PointerEventData eventData) // Slot üzerindeyken fare týklamasý býrakýldýðýnda neler olacaðýný belirliyoruz.
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.name == transform.name) // Týk býrakýldýðýnda herhangi bir obje var mý, varsa obje ismi ile slot ismi ayný mý diye kontrol ediyoruz.
        {
            playSound(0);
            Mission001Script.instance.matchComplated++;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; // Býrakýlan objeyi slot objesine ortalýyoruz.

            for (int x = 0; x < childNumber; x++)
            {
                transform.GetChild(x).gameObject.SetActive(true); // Slot objesi içerisinde ne kadar alt obje varsa hepsini aktif hale getiriyoruz.
            }
        }
        else
        {
            playSound(1);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = DragAndDropScript.originalPosition; // Býrakýlan objeyi orjinal pozisyonuna geri yolluyoruz.
        }
        
    }
    public void playSound(int sel) // Doðru ya da yanlýþ eþleþtirme seslerini dýþarýdan eriþerek çalabilmek için bir methot yazdýk.
    {
        audioSource.PlayOneShot(sounds[sel]);
    }
}
