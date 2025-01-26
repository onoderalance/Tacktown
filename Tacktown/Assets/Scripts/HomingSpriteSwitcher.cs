using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite sprite1; // First sprite
    public Sprite sprite2; // Second sprite

    private SpriteRenderer spriteRenderer;
    private bool isSprite1Active = true; // Tracks which sprite is active
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

            // Switch sprite
            isSprite1Active = !isSprite1Active;
            spriteRenderer.sprite = isSprite1Active ? sprite1 : sprite2;
        }
    }
}
