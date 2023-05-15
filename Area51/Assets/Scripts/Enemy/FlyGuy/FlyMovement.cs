using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    //Components
    private Transform player; // Reference to the player's transform
    private Rigidbody2D rb;
    private FlyCombat combat;

    //Variables
    public float movementSpeed = 5f; // Speed at which the enemy moves towards the player
    public float rotationSpeed = 200f; // Speed at which the enemy rotates around the player
    public float circleRadius = 5f; // Radius of the circular path around the player
    private Vector2 circleCenter;
    public float detectionDistance = 8f; // Distance at which the enemy detects the player

    //Flags
    public bool disableMOvement = false;

    private void Start()
    {
        // Find the player GameObject using a tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        combat = this.GetComponent<FlyCombat>();
    }

    private void Update()
    {
        if(!disableMOvement){
            Movement();
        }
    }

    public void Movement(){
        if(!IsPlayerClose()){
            MoveTowards();
        }
        else{
            if(!combat.CanIShoot()){
                CircleEnemy();
            }
            else{
                MoveUpDown();
            }
        }
    }

    public float verticalSpeed = 1f;  // Speed of the vertical movement
    public float verticalAmplitude = 0.5f;  // Amplitude of the vertical movement

    void MoveUpDown()
    {
        // Apply an upward force to the enemy
        rb.AddForce(Vector2.up * verticalSpeed);

        // Apply a downward force to counteract the upward force
        rb.AddForce(Vector2.down * verticalSpeed);
    }

    public void MoveTowards()
    {
        // Calculate the direction from the enemy to the player
        Vector2 direction = player.position - transform.position;

        // Normalize the direction vector to get a unit vector
        direction.Normalize();

        // Move the enemy towards the player
        rb.velocity = direction * movementSpeed;
    }

    public void CircleEnemy(){
        // Calculate the direction from the enemy to the circle center
        Vector2 directionToCenter = circleCenter - (Vector2)transform.position;
        directionToCenter.Normalize();

        // Calculate the perpendicular direction for circling
        Vector2 perpendicularDirection = new Vector2(-directionToCenter.y, directionToCenter.x);

        // Calculate the target position on the circular path
        Vector2 targetPosition = circleCenter + (perpendicularDirection * circleRadius);

        // Calculate the direction from the enemy to the target position
        Vector2 directionToTarget = targetPosition - (Vector2)transform.position;
        directionToTarget.Normalize();

        // Move the enemy towards the target position using Rigidbody2D velocity
        rb.velocity = directionToTarget * movementSpeed;

        // Rotate the enemy towards the target position using Rigidbody2D rotation
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);

        // Update the circle center to move it along with the player
        circleCenter = player.position;
    }

    public bool IsPlayerClose(){
        Vector2 direction = player.position - transform.position;
        // Check if the player is within the detection distance
        if (direction.magnitude <= detectionDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
