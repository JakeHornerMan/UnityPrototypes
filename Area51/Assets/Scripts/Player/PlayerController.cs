using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //Components
    private Rigidbody2D rb;

    //Flags
    private bool facingRight = true;

    //Movement Variables
    public float testVar = 1f;
    public float runSpeed = 10;
    public float jumpForce = 16;
    public float fallMultiplier = 5.0f;
    public float lowJumpMultiplier = 5.0f;

    //Layers
    [SerializeField] private LayerMask platformLayerMask;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMove();
        // Debug.Log("Is touching floor: "+ IsGrounded());
    }

    public void PlayerMove(){
        Move();
        Jump();
    }

    private void Move(){
        if (Input.GetKey(KeyCode.A)){
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            facingRight = false;  
        }
        else if (Input.GetKey(KeyCode.D)){
            rb.velocity = new Vector2(+runSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            facingRight = true;
        }
    }
    
    private void Jump(){
        if (Input.GetKey(KeyCode.W) && IsGrounded()){
            rb.velocity = Vector2.up * jumpForce;
        }
        //using gravity for hold jump and to fall faster
        if(rb.velocity.y<0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier-1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W)){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier-1) * Time.deltaTime;
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(new Vector2(transform.position.x,transform.position.y-1f),0.2f,platformLayerMask);
    }

}
