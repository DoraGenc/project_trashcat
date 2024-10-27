using UnityEngine;

public class CharacterSpriteController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // Define sprites for each action
    public Sprite s_demo_ability;
    public Sprite s_demo_act;
    public Sprite s_demo_craft;
    public Sprite s_demo_walkDown;
    public Sprite s_demo_walkLeft;
    public Sprite s_demo_walkRight;
    public Sprite s_demo_walkUp;
    public Sprite s_demo_idle;
    public Sprite s_demo_pick;
    public Sprite s_demo_special;

    // Enum to track current action
    private enum Action { None, Ability, Act, Craft, WalkDown, WalkLeft, WalkRight, WalkUp, Pick, Special }
    private Action currentAction = Action.None;

    void Update()
    {
        CheckActionInput();
        UpdateSprite();
    }

    private void CheckActionInput()
    {
        // Check for key down and key up to set the current action
        if (Input.GetKey(KeyCode.Keypad1)) currentAction = Action.Ability;
        else if (Input.GetKey(KeyCode.Keypad2)) currentAction = Action.Act;
        else if (Input.GetKey(KeyCode.Keypad3)) currentAction = Action.Craft;
        else if (Input.GetKey(KeyCode.Keypad4)) currentAction = Action.WalkDown;
        else if (Input.GetKey(KeyCode.Keypad5)) currentAction = Action.WalkLeft;
        else if (Input.GetKey(KeyCode.Keypad6)) currentAction = Action.WalkRight;
        else if (Input.GetKey(KeyCode.Keypad7)) currentAction = Action.WalkUp;
        else if (Input.GetKey(KeyCode.Keypad8)) currentAction = Action.Special;
        else if (Input.GetKey(KeyCode.Keypad9)) currentAction = Action.Pick;
        
        else currentAction = Action.None; // Default to None if no key is pressed
    }

    private void UpdateSprite()
    {
        // Set sprite based on the current action
        switch (currentAction)
        {
            case Action.Ability:
                spriteRenderer.sprite = s_demo_ability;
                break;
            case Action.Act:
                spriteRenderer.sprite = s_demo_act;
                break;
            case Action.Craft:
                spriteRenderer.sprite = s_demo_craft;
                break;
            case Action.WalkDown:
                spriteRenderer.sprite = s_demo_walkDown;
                break;
            case Action.WalkLeft:
                spriteRenderer.sprite = s_demo_walkLeft;
                break;
            case Action.WalkRight:
                spriteRenderer.sprite = s_demo_walkRight;
                break;
            case Action.WalkUp:
                spriteRenderer.sprite = s_demo_walkUp;
                break;
            case Action.Pick:
                spriteRenderer.sprite = s_demo_pick;
                break;
            case Action.Special:
                spriteRenderer.sprite = s_demo_special;
                break;
            default:
                spriteRenderer.sprite = s_demo_idle;
                break;
        }
    }
}
