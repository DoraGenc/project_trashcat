using UnityEngine;

public class GlobalItemDatabaseManager : MonoBehaviour
{
    public ItemDatabase itemDatabase; // Drag your ItemDatabase ScriptableObject here

    // Singleton pattern to access this globally
    public static GlobalItemDatabaseManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Utility function to find items by name globally
    public Item GetItemByName(string itemName)
    {
        return itemDatabase.GetItemByName(itemName);
    }

    public void SearchForItemInDatabase(string itemToSearchFor)
    {
        // Retrieve the Sword item from the global ItemDatabaseManager
        Item item = GlobalItemDatabaseManager.instance.GetItemByName(itemToSearchFor);

        if (item != null)
        {
            Debug.Log("Found item: " + item.itemName);
            // You can now use this item, e.g., display in UI or add to inventory
        }
        else
        {
            Debug.LogWarning("Item not found in the database.");
        }
    }
}
