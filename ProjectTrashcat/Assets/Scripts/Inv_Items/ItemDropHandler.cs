using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public void OnItemDropped(ItemDragDropHandler draggedItem)
    {
        // Beispiel: Füge Item zum Slot hinzu
        Debug.Log("Item dropped on: " + gameObject.name);

        // Setze das Item in den aktuellen Slot
        draggedItem.transform.SetParent(transform);
        draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // Optional: Füge Logik zum Inventar hinzu, um das Item in den Slot einzufügen
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Verarbeite den Drop hier, falls erforderlich
    }
}
