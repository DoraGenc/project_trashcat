using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public void OnItemDropped(ItemDragDropHandler draggedItem)
    {
        // Beispiel: F�ge Item zum Slot hinzu
        Debug.Log("Item dropped on: " + gameObject.name);

        // Setze das Item in den aktuellen Slot
        draggedItem.transform.SetParent(transform);
        draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // Optional: F�ge Logik zum Inventar hinzu, um das Item in den Slot einzuf�gen
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Verarbeite den Drop hier, falls erforderlich
    }
}
