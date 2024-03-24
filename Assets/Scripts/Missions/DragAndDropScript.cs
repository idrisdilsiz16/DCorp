/*
    G�revlerde kullan�lacak itemlere s�r�kle b�rak �zelli�i sa�layan script
 */

using UnityEngine;
using UnityEngine.EventSystems;
public class DragAndDropScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public static DragAndDropScript instance;
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public static Vector2 originalPosition;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData eventData) // S�r�kleme ba�lad���nda, s�r�klenen objenin g�r�n�rl���n� hafif saydam yap�p, objenin alt�nda ne oldu�unu g�rebilmek i�in Raycast g�r�n�rl���n� kapatt�k.
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) // s�r�kleme s�ras�nda fare imleci ile objenin bire bir ayn� gitmesi fare hareketini canvasa oranlayarak obje pozisyonuna ekledik.
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        GameObject mouseReleasedObj = eventData.pointerCurrentRaycast.gameObject; // fare butonu b�rak�ld��� s�rada objenin hangi obje �zerinde oldu�u bilgisini ald�k.
        
        if (mouseReleasedObj.transform.tag != "Slots") // obje e�er bo� bir alana b�rak�ld�ysa yanl�� yer sesi �al�nd� ve obje orjinal pozisyonuna g�nderildi.
        {
            ItemSlotScript.instance.playSound(1);
            this.gameObject.GetComponent<RectTransform>().position = originalPosition;
        }
        
        canvasGroup.alpha = 1.0f;           // s�r�kleme bitirildi�inde obje g�r�n�rl��� ve raycast g�r�n�rl��� eski haline getirildi.
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData) // ilk t�klamada t�klan�lan objenin pozisyonu orjinal pozisyon g�ndermeleri i�in al�nd�.
    {
        originalPosition = this.gameObject.GetComponent<RectTransform>().position;
    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}
