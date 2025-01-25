using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float minSpeed = 0f;
    public float accelerationFactor = 0.1f;
    public float decelerationFactor = 2f;
    public float maxPushForce = 20f;

    private Vector2 velocity = Vector2.zero;
    private Vector2 pushDirection = Vector2.zero;
    private float distanceFactor = 0;

    private int decelTime = 5;


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
