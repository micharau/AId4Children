using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoveForward : MonoBehaviour
{
    public float speed = 1.0f; // Speed factor for the movement
    public float moveDistance = 5.0f; // The distance the actor can walk

    private bool isMovingForward = true; // Movement direction flag.
    private Vector3 startingPosition; // To keep track of the starting position.
    private bool canMove = true; // To control movement based on collision.

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position; // Record the starting position.
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveAndTurnBehavior();
        }
    }

    IEnumerator TurnAroundWithDelay()
    {
        canMove = false; // Optional: stop movement during the wait time.
        yield return new WaitForSeconds(3); // Wait for 3 seconds.
        TurnAround(); // Now perform the turnaround.
        canMove = true; // Resume movement.
    }

    void MoveAndTurnBehavior()
    {
        // Calculate the distance moved from the starting position.
        float movedDistance = Vector3.Distance(startingPosition, transform.position);

        // If the object has moved the specified distance or more, reverse direction.
        if (movedDistance >= moveDistance)
        {
            //TurnAround();
            StartCoroutine(TurnAroundWithDelay());
            startingPosition = transform.position; //update starting position
        }

        // Move the object forward in the current direction.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void TurnAround()
    {
        // Rotate the object by 180 degrees around the Y axis.
        
        transform.Rotate(0, 180, 0);
        isMovingForward = !isMovingForward; // Update the movement direction flag.
    }

    // Called when this collider/rigidbody has begun touching another rigidbody/collider.
    private void OnCollisionEnter(Collision collision)
    {
        // Optional: Check if the collision is with a specific object or tag.
        // if(collision.gameObject.tag == "YourSpecificTagHere")

        canMove = false; // Stop movement.
    }

    // Called when the collider/rigidbody has stopped touching another rigidbody/collider.
    private void OnCollisionExit(Collision collision)
    {
        // Resume movement after collision ends if you want it to pause and then continue.
        // Remove or comment out this method if you want movement to permanently stop on collision.
        canMove = true; // Allow movement again.
    }
}