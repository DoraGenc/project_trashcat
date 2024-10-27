using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image itemImage; // Referenz zur Image-Komponente des UI-Elements
    public Inventory inventory; // Referenz zum Inventar
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;
    private Transform dropTarget;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        canvasGroup.alpha = 0.6f; // Sichtbarkeit reduzieren, wenn gezogen wird
        canvasGroup.blocksRaycasts = false; // Verhindert, dass das Element andere Raycasts blockiert
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor; // Die Position des Elements anpassen
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; // Sichtbarkeit wiederherstellen
        canvasGroup.blocksRaycasts = true;

        // Wenn das Element nicht mehr am ursprünglichen Ort ist
        if (transform.parent != originalParent)
        {
            if (dropTarget != null)
            {
                // Überprüfen, ob das Drop-Ziel ein Slot ist
                if (dropTarget.CompareTag("ItemSlot"))
                {
                    // Hier rufen wir eine Methode in einem Skript auf, das für das Drop-Ziel verantwortlich ist
                    ItemDropHandler dropHandler = dropTarget.GetComponent<ItemDropHandler>();
                    if (dropHandler != null)
                    {
                        dropHandler.OnItemDropped(this);
                    }
                }
                else
                {
                    // Wieder zum ursprünglichen Ort zurückkehren, wenn das Ziel kein Slot ist
                    transform.SetParent(originalParent);
                    rectTransform.anchoredPosition = Vector2.zero;
                }
            }
            else
            {
                // Wieder zum ursprünglichen Ort zurückkehren, wenn kein Ziel vorhanden ist
                transform.SetParent(originalParent);
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        dropTarget = eventData.pointerEnter?.transform; // Speichere das Drop-Ziel, falls vorhanden
    }
}
