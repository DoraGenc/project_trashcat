using UnityEngine;

public class ItemUseHandler : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>(); 
    }

    public void UseSelectedItem()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory not found!");
            return;
        }

        Item selectedItem = inventory.GetPrimarySelectedItem();

        if (selectedItem == null)
        {
            Debug.Log("No item selected to use.");
            return;
        }

        // Call the corresponding "use" action based on item name
        switch (selectedItem.itemName)
        {
            case "Healing Potion":
                UseHealingPotion();
                break;
            case "Magic Scroll":
                UseMagicScroll();
                break;
            default:
                Debug.Log($"{selectedItem.itemName} cannot be used.");
                break;
        }
    }

    private void UseHealingPotion()
    {
        Debug.Log("You used a Healing Potion and restored health.");

    }

    private void UseMagicScroll()
    {
        Debug.Log("You used a Magic Scroll and cast a powerful spell.");

    }
}
