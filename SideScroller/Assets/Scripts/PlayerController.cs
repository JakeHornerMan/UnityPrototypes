using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private Animator anim;

    private PlayerCombat pc;
    
    public float speed = 10;
    public float jumpForce = 16;
    public float fallMultiplier = 5.0f;
    public float lowJumpMultiplier = 5.0f;
    private bool facingRight = true;
    private enum State{idle,run,jump,fall}
    private State action;


    public void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        cc = this.GetComponent<CapsuleCollider2D>();
        pc = this.GetComponent<PlayerCombat>();
    }

    void Update()
    {
        PlayerMove();
        PlayerAttack();
        SetAnim();
    }

    public void SetAnim(){
        anim.SetInteger("State", (int)action);
    }

    public void PlayerMove(){
        Move();
        Jump(); 
    }

    public void PlayerAttack(){
        if(Input.GetKeyDown(KeyCode.Space)){
            pc.BasicHit();
        }
    }

    public void Move(){
        if (Input.GetKey(KeyCode.A)){
            if(IsGrounded()){
                action = State.run;
            }
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            facingRight = false;
            FindObjectOfType<TurretFollow>().faceDirection(facingRight);
        }
        else if (Input.GetKey(KeyCode.D)){
            if(IsGrounded()){
                action = State.run;
            }
            rb.velocity = new Vector2(+speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            facingRight = true;
            FindObjectOfType<TurretFollow>().faceDirection(facingRight);
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

        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = 
            Physics2D.BoxCast(cc.bounds.center, cc.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);
            Color rayColor;

        if (raycastHit.collider != null)
        {
            action = State.idle;
            rayColor = Color.green;
            //Debug.Log("true");
            return true;
        }
        else {
            if (rb.velocity.y < 0) {
                action = State.fall;
            }
            else if (rb.velocity.y > 0) {
                action = State.jump;

            }
            rayColor = Color.green;
            return false;
        }
    }

    public bool isTouchingWall(){
        //Physics2D.Raycast
        return false;
    }
}
