using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public Inventory inventory; // Reference to the Inventory script

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item")) // Check if the collided object is tagged as "Item"
        {
            Item item = other.GetComponent<ItemComponent>().item; // Get the Item component
            inventory.AddItemToInventory(item); // Add item to inventory
            Destroy(other.gameObject); // Destroy the item in the scene
        }
    }
}
