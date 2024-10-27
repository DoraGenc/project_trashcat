using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventureScript : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;
    public LayerMask clickableLayer;

    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        Debug.Log("Inventory found: " + (inventory != null));
    }

    void Update()
    {
        if (inventory != null && inventory.InventoryWindow.activeSelf)
        {
            Debug.Log("Inventory is open, blocking raycast.");
            return; // Block raycast if inventory is open
        }

        // Mouse click logic
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero, Mathf.Infinity, clickableLayer);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.tag);

                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    player.GetComponent<PlayerControls>().SetTargetPosition(hit.point);
                }

                if (hit.collider.gameObject.CompareTag("Item"))
                {
                    // Set the item as the current item to collect
                    player.GetComponent<PlayerControls>().SetCurrentItem(hit.collider.gameObject);
                    player.GetComponent<PlayerControls>().SetTargetPosition(hit.collider.gameObject.transform.position);
                }

                if (hit.collider.gameObject.CompareTag("Door"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}