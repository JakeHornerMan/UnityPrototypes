using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public Rigidbody2D rb;
    public CapsuleCollider2D cc;
    public Animator anim;
    
    public float speed = 10;
    public float jumpForce = 10;
    private float jumpTimeCounter; 
    public float jumpTime;
    private bool isJumping;
    private bool facingRight;
    private enum State{idle,run,jump,fall}
    private State action;

    public void Start()
    {
        rb= this.GetComponent<Rigidbody2D>();
        anim= this.GetComponent<Animator>();

        facingRight = true;
    }

    void FixedUpdate()
    {
        PlayerMove();
        SetAnim();
    }

    public void SetAnim(){
        anim.SetInteger("State", (int)action);
        if (rb.velocity.y < .1f && IsGrounded() == false){
            action = State.fall;
        }
        else if (rb.velocity.y > .1f && IsGrounded() == false){
            action = State.jump;
        }
        else if(IsGrounded() == true){
            action = State.idle;
        }
    }

    public void PlayerMove(){
        Move();
        Jump();
    }

    public void Move(){
        if (Input.GetKey(KeyCode.A)){
            if(IsGrounded()){
                action = State.run;
            }
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            facingRight = false;
        }
        else if (Input.GetKey(KeyCode.D)){
            if(IsGrounded()){
                action = State.run;
            }
            rb.velocity = new Vector2(+speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            facingRight = true;
        }
        else {
            if(IsGrounded()){
                action = State.idle;
            }
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void Jump(){
        if (Input.GetKey(KeyCode.W) && IsGrounded()){
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetKeyDown(KeyCode.W) && isJumping ==true){
            if(jumpTimeCounter > 0){
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else{
                isJumping = false;
            }
        }
        
        if(Input.GetKeyUp(KeyCode.W)){
            isJumping = false;
        }
    }

    private bool IsGrounded() {

        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = 
            Physics2D.BoxCast(cc.bounds.center, cc.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
            Color rayColor;

        if (raycastHit.collider != null)
        {
            action = State.idle;
            rayColor = Color.green;
            //Debug.Log("true");
            isJumping = false;
            return true;
        }
        else {
            rayColor = Color.green;
            //Debug.Log("false");
            return false;
        }
    }
}
