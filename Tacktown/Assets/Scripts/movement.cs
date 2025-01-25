using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float minSpeed = 0f;
    public float accelerationFactor = 0.1f;
    public float decelerationFactor = 0.005f;
    public float maxPushForce = 20f;
    private Vector2 velocity = Vector2.zero;
    private Vector2 pushDirection = Vector2.zero;


    void Update()
    {
        Vector2 playerPos = transform.position;
        Vector2 mousePos = GetMouseWorldPosition();

        // Calculate the distance between the player and the mouse
        float distance = Vector2.Distance(playerPos, mousePos);

        // When mouse button is held
        if (Input.GetMouseButton(0))
        {
            // Calculate the direction (vector) between the player and the mouse
            Vector2 desiredDirection = (mousePos - playerPos).normalized;

            // Apply a force away from the mouse (opposite direction)
            Vector2 pushDirection = -desiredDirection;

            // Smooth transition for speed based on the distance (further = slower)
            float distanceFactor = Mathf.Clamp01(distance / maxSpeed);  // Scales as distance increases
            float currentSpeed = Mathf.Lerp(maxSpeed, minSpeed, distanceFactor); // Smooth speed transition

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
            float distanceFactor = Mathf.Clamp01(distance / maxSpeed);  // Scales as distance increases
            float currentSpeed = Mathf.Lerp(maxSpeed, minSpeed, distanceFactor); // Smooth speed transition

            // Smoothly apply the push direction and the current speed to velocity
            velocity = Vector2.Lerp(velocity, pushDirection * currentSpeed, accelerationFactor);

            // Ensure that we do not exceed max speed
            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
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
}
