using UnityEngine;

public class SpriteSwitcherThree : MonoBehaviour
{
    public Sprite sprite1; // First sprite
    public Sprite sprite2; // Second sprite
    public Sprite sprite3; // Third sprite

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0; // Tracks the active sprite (0, 1, or 2)
    private float timer = 0f; // Timer to track time
    public float stepInterval = 0.5f; // Time interval between steps (in seconds)

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite
        spriteRenderer.sprite = sprite1;
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if the interval has passed
        if (timer >= stepInterval)
        {
            // Reset the timer
            timer = 0f;

            // Cycle through sprites
            currentSpriteIndex = (currentSpriteIndex + 1) % 3;

            // Set the sprite based on the current index
            switch (currentSpriteIndex)
            {
                case 0:
                    spriteRenderer.sprite = sprite1;
                    break;
                case 1:
                    spriteRenderer.sprite = sprite2;
                    break;
                case 2:
                    spriteRenderer.sprite = sprite3;
                    break;
            }
        }
    }
}
