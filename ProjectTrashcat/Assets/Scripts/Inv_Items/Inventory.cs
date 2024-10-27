using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public ItemDatabase itemDatabase;
    public GameObject InventoryWindow;
    public CanvasGroup inventoryCanvasGroup;
    public float itemWidth = 50f;
    public float itemHeight = 50f;
    public bool blockPlayerActions; // New variable to block player actions

    // Variables for selected items
    private Item primarySelectedItem; // The currently selected item
    private Item secondarySelectedItem; // The previously selected item

    // List of slot images for the items in the UI (should be assigned in the Inspector)
    public List<Image> slotImages; // The UI slots to show item icons

    // Adds an item to the inventory
    public void AddItemToInventory(Item item)
    {
        if (item != null)
        {
            // Check for available slots before adding the item
            if (items.Count < slotImages.Count) // Ensure there is space
            {
                items.Add(item);
                Debug.Log(item.itemName + " wurde dem Inventar hinzugefügt.");
                UpdateInventoryUI(); // Update the UI to show the collected item
            }
            else
            {
                Debug.LogWarning("Das Inventar ist voll.");
            }
        }
        else
        {
            Debug.LogWarning("Das Item ist null.");
        }
    }

    // Updates the inventory UI to reflect the collected items
    public void UpdateInventoryUI()
    {
        if (InventoryWindow.activeSelf)
        {
            for (int i = 0; i < slotImages.Count; i++)
            {
                // Get the parent (InventorySlot/Button) and the child (ItemIcon)
                GameObject slotObject = slotImages[i].gameObject; // The Button or InventorySlot
                Transform itemIconTransform = slotObject.transform.Find("ItemIcon"); // Child ItemIcon object

                // Make sure the slot icon (Button) is always visible
                slotImages[i].enabled = true;

                // Handle the display of the item in the child ItemIcon
                if (i < items.Count) // If there's an item for this slot
                {
                    Image itemImage = itemIconTransform.GetComponent<Image>();
                    itemImage.sprite = items[i].itemIcon; // Set item icon in the child
                    itemImage.enabled = true; // Make sure the image is visible

                    // Adjust the size if necessary to fit within the child
                    RectTransform itemRect = itemIconTransform.GetComponent<RectTransform>();
                    itemRect.sizeDelta = new Vector2(itemWidth, itemHeight); // Adjust to fit slot

                    // Add a Button component to handle item selection
                    Button button = slotObject.GetComponent<Button>();
                    if (button == null)
                    {
                        button = slotObject.AddComponent<Button>();
                    }
                    int index = i; // Capture the current index
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => SelectItem(index)); // Call SelectItem on click
                }
                else
                {
                    // No item, so hide the child image (ItemIcon)
                    Image itemImage = itemIconTransform.GetComponent<Image>();
                    itemImage.enabled = false; // Hide the item image
                }
            }
        }
    }

    public void ToggleInventoryState()
    {
        bool isActive = !InventoryWindow.activeSelf;
        InventoryWindow.SetActive(isActive);
        inventoryCanvasGroup.blocksRaycasts = isActive;
        blockPlayerActions = isActive; // Toggle blocking player actions based on inventory state
        UpdateInventoryUI();
    }

    public void SelectItem(int index)
    {
        if (index < items.Count)
        {
            // Update the selected items
            secondarySelectedItem = primarySelectedItem;
            primarySelectedItem = items[index];

            Debug.Log($"Selected Item: {primarySelectedItem.itemName}");
        }
    }

    public void InspectSelectedItem()
    {
        if (primarySelectedItem != null)
        {
            Debug.Log($"Inspecting Item: {primarySelectedItem.itemName}");
            Debug.Log($"Description: {primarySelectedItem.itemDescription}"); // Assuming 'itemDescription' is a property of your Item class
        }
        else
        {
            Debug.Log("No item selected to inspect.");
        }
    }

    public void CombineSelectedItems()
    {
        if (primarySelectedItem == null || secondarySelectedItem == null)
        {
            //not 2 Items Selected
            Debug.Log("Es müssen zwei Items ausgewählt sein, um sie zu kombinieren.");
            return;
        }

        // Prüfen, ob die beiden Items kombinierbar sind
        bool primaryCombinesWithSecondary = System.Array.Exists(primarySelectedItem.itemCombinableWith, item => item == secondarySelectedItem);
        bool secondaryCombinesWithPrimary = System.Array.Exists(secondarySelectedItem.itemCombinableWith, item => item == primarySelectedItem);

        if (primaryCombinesWithSecondary && secondaryCombinesWithPrimary)
        {
            //Items Valid
            Debug.Log($"{primarySelectedItem.itemName} kann mit {secondarySelectedItem.itemName} kombiniert werden!");

            PerformItemCombination();

        }
        else
        {
            //Items not Valid
            Debug.Log($"{primarySelectedItem.itemName} kann nicht mit {secondarySelectedItem.itemName} kombiniert werden.");
        }
    }

    private void PerformItemCombination()
    {
        // Double Check
        if (primarySelectedItem.itemCombinationResult == null)
        {
            Debug.Log("Kein Kombinationsergebnis für die ausgewählten Items vorhanden.");
            return;
        }

        // Entferne die ausgewählten Items aus dem Inventar
        items.Remove(primarySelectedItem);
        items.Remove(secondarySelectedItem);
        Debug.Log($"{primarySelectedItem.itemName} und {secondarySelectedItem.itemName} wurden aus dem Inventar entfernt.");

        // Füge das kombinierte Item hinzu
        AddItemToInventory(primarySelectedItem.itemCombinationResult);
        Debug.Log($"Das kombinierte Item {primarySelectedItem.itemCombinationResult.itemName} wurde dem Inventar hinzugefügt.");

        primarySelectedItem = null;
        secondarySelectedItem = null;

        UpdateInventoryUI();
    }

    public Item GetPrimarySelectedItem()
    {
        return primarySelectedItem;
    }


}