using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls playerControls; // Reference to PlayerControls
    private Inventory inventory; // Reference to the Inventory

    private void Start()
    {
        playerControls = GetComponent<PlayerControls>();
        inventory = FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        // Handle input based on whether inventory is open
        if (inventory.blockPlayerActions)
        {
            HandleInventoryInput();
        }
        else
        {
            HandleMovementInput();
            HandleItemCollection();
        }
    }

    private void HandleMovementInput()
    {
        // Mouse input for movement
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerControls.SetTargetPosition(mousePos);
        }

        // Keyboard input for movement (WASD)
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection.magnitude > 0)
        {
            playerControls.SetTargetPosition(playerControls.transform.position + (Vector3)moveDirection * playerControls.speed * Time.deltaTime);
        }
    }

    private void HandleItemCollection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerControls.CollectItem();
        }
    }

    private void HandleInventoryInput()
    {
        // Handle inventory navigation with W (up) and S (down)
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Navigate up in inventory (implement inventory selection logic)
            Debug.Log("Navigate up in inventory");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            // Navigate down in inventory (implement inventory selection logic)
            Debug.Log("Navigate down in inventory");
        }

        // Add other inventory-related controls here
    }
}
