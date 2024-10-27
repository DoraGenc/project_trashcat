using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f;
    public PolygonCollider2D boundaryCollider;
    private Rigidbody2D rb;
    private Inventory inventory;
    private Vector2 targetPos; // Target position for movement
    private bool isMoving;
    private GameObject currentItem; // Store the current item to collect

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPos, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, targetPos) < 0.1f)
        {
            isMoving = false;
            // Check for item to collect once reached
            CollectItem();
        }
    }

    public void SetTargetPosition(Vector2 target)
    {
        targetPos = target;
        isMoving = true;
    }

    public void CollectItem()
    {
        if (currentItem != null)
        {
            // Check if the player is close enough to collect the item
            if (Vector2.Distance(rb.position, currentItem.transform.position) < 1f)
            {
                ItemComponent itemComponent = currentItem.GetComponent<ItemComponent>();
                if (itemComponent != null && itemComponent.item != null)
                {
                    inventory.AddItemToInventory(itemComponent.item);
                    currentItem.SetActive(false); // Disable the item object
                    currentItem = null; // Reset current item
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            SetCurrentItem(other.gameObject); // Set the current item when colliding
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            // Reset currentItem if exiting the trigger
            if (currentItem == other.gameObject)
            {
                currentItem = null;
            }
        }
    }

    public void SetCurrentItem(GameObject item)
    {
        currentItem = item;
    }
}
