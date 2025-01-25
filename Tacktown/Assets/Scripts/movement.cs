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

    public float airstreamForce = 0.5f;
    public float airstreamSlowFactor = 0.5f;

    private Vector2 velocity = Vector2.zero;
    private Vector2 pushDirection = Vector2.zero;
    private float distanceFactor = 0;

    private int decelTime = 5;

    private Rigidbody2D rb;
    private bool inAirstream = false; // Track if the player is in the airstream

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

        // Apply upward momentum when in the airstream
        if (inAirstream)
        {
            velocity.y += airstreamForce * Time.deltaTime; // Apply upward force
        }
        else
        {
            // Slowly slow down upward momentum when leaving the airstream
            if (velocity.y > 0)
            {
                velocity.y -= airstreamSlowFactor * Time.deltaTime;
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

            // get collision normal (wall dir.)
            Vector2 collisionNormal = collision.contacts[0].normal;

            // reflect accross (preserve momentum)
            velocity = Vector2.Reflect(velocity, collisionNormal) * bounceFactor;
        }
    }

    // Call this method when the player enters the airstream trigger zone
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag(""))
    //    {
    //        inAirstream = true;
    //    }
    //}

    //// Call this method when the player exits the airstream trigger zone
    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag(""))
    //    {
    //        inAirstream = false;
    //    }
    //}
}
