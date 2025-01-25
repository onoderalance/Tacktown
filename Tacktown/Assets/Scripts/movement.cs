using System;
using System.Collections;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float minSpeed = 0f;
    public float accelerationFactor = 0.1f;
    public float decelerationFactor = 2f;
    public float maxPushForce = 20f;
    public float bounceFactor = 0.5f;

    public float airstreamForce = 8f;
    public float airstreamSlowFactor = 0.5f;

    private Vector2 velocity = Vector2.zero;
    private Vector2 pushDirection = Vector2.zero;
    private float distanceFactor = 0;

    private int decelTime = 5;

    private Rigidbody2D rb;
    private bool inAirstream = false; // Track if the player is in the airstream
    private Vector2 airstreamDirection = Vector2.zero; // Direction of the airstream

    void Start()
    {
        print("start");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 playerPos = transform.position;
        Vector2 mousePos = GetMouseWorldPosition();

        // Calculate the distance between the player and the mouse
        float distance = Vector2.Distance(playerPos, mousePos);

        // When mouse button is held
        if (Input.GetMouseButton(0))
        {
            decelTime = 5;

            // Calculate the direction (vector) between the player and the mouse
            Vector2 desiredDirection = (mousePos - playerPos).normalized;

            // Apply a force away from the mouse (opposite direction)
            Vector2 pushDirection = -desiredDirection;

            // Smooth transition for speed based on the distance (further = slower)
            float distanceFactor = Mathf.Clamp01(distance / maxSpeed);
            float currentSpeed = Mathf.Lerp(maxSpeed, minSpeed, distanceFactor);

            // Smoothly apply the push direction and the current speed to velocity
            velocity = Vector2.Lerp(velocity, pushDirection * currentSpeed, accelerationFactor);

            // Ensure that we do not exceed max speed
            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
        }
        else
        {
            if (decelTime > 0)
            {
                decelTime--;
            }
            else
            {
                float currentSpeed = Mathf.Lerp(maxSpeed, minSpeed, distanceFactor * 0.5f);

                // Smoothly apply the push direction and the current speed to velocity
                velocity = Vector2.Lerp(velocity, pushDirection * currentSpeed, decelerationFactor);

                // Ensure that we do not exceed max speed
                if (velocity.magnitude > maxSpeed)
                {
                    velocity = velocity.normalized * maxSpeed;
                }
            }
        }

        // Apply force in the direction of the airstream when inside the airstream
        if (inAirstream)
        {
            velocity += airstreamDirection * airstreamForce * Time.deltaTime; // Apply force in airstream direction
        }
        else
        {
            // Slowly slow down momentum when not in the airstream
            if (velocity.magnitude > 0)
            {
                velocity -= velocity.normalized * airstreamSlowFactor * Time.deltaTime;
            }
        }

        // Move the player based on the velocity
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }

    // Helper function to get the mouse position relative to the Camera
    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            print("COLLIDED");

            // Get collision normal (wall direction)
            Vector2 collisionNormal = collision.contacts[0].normal;

            // Reflect across the collision normal (preserve momentum)
            velocity = Vector2.Reflect(velocity, collisionNormal) * bounceFactor;
        }
    }

    // Call this method when the player enters the airstream trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("airstream"))
        {
            inAirstream = true;

            // Get the direction of the airstream based on its rotation
            airstreamDirection = other.transform.up; // Assume the airstream's forward direction is its "up" vector
        }
    }

    // Call this method when the player exits the airstream trigger zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("airstream"))
        {
            inAirstream = false;
            airstreamDirection = Vector2.zero; // Reset the airstream direction
        }
    }
}
