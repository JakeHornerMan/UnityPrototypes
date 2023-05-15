using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMovement : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    private Rigidbody2D rb;

    public float speed = 5f; // Speed at which the enemy moves towards the player

    private void Start()
    {
        // Find the player GameObject using a tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveTowards();
    }

    public void MoveTowards()
    {
        // Calculate the direction from the enemy to the player
        Vector2 direction = player.position - transform.position;

        // Normalize the direction vector to get a unit vector
        direction.Normalize();

        // Move the enemy towards the player
        rb.velocity = direction * speed;
    }
}
