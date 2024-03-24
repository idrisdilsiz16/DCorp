/*
    Görevlerde kullanýlacak itemlere sürükle býrak özelliði saðlayan script
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
    
    public void OnBeginDrag(PointerEventData eventData) // Sürükleme baþladýðýnda, sürüklenen objenin görünürlüðünü hafif saydam yapýp, objenin altýnda ne olduðunu görebilmek için Raycast görünürlüðünü kapattýk.
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) // sürükleme sýrasýnda fare imleci ile objenin bire bir ayný gitmesi fare hareketini canvasa oranlayarak obje pozisyonuna ekledik.
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        GameObject mouseReleasedObj = eventData.pointerCurrentRaycast.gameObject; // fare butonu býrakýldýðý sýrada objenin hangi obje üzerinde olduðu bilgisini aldýk.
        
        if (mouseReleasedObj.transform.tag != "Slots") // obje eðer boþ bir alana býrakýldýysa yanlýþ yer sesi çalýndý ve obje orjinal pozisyonuna gönderildi.
        {
            ItemSlotScript.instance.playSound(1);
            this.gameObject.GetComponent<RectTransform>().position = originalPosition;
        }
        
        canvasGroup.alpha = 1.0f;           // sürükleme bitirildiðinde obje görünürlüðü ve raycast görünürlüðü eski haline getirildi.
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData) // ilk týklamada týklanýlan objenin pozisyonu orjinal pozisyon göndermeleri için alýndý.
    {
        originalPosition = this.gameObject.GetComponent<RectTransform>().position;
    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}
