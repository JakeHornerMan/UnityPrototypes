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

    private bool disableMove;

    private enum State{idle,run,jump,fall,wallSlide}
    private State action;
    public float speed = 10;

    //Jumping
    public Transform groundCheck;
    public float checkRadius;
    public float jumpForce = 16;
    public float fallMultiplier = 5.0f;
    public float lowJumpMultiplier = 5.0f;
    private bool facingRight = true;
    

    //WallSlide 
    public Transform wallCheck;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float xWallJumpForce = 10f;
    public float yWallJumpForce = 10f;
    public float walljumpTime;

    //LedgeClimb
    /*
    public Transform ledgeCheck;
    public float xledgeOffset1 = 0.5f;
    public float yledgeOffset1 = 0.3f;
    public float xledgeOffset2 = 0f;
    public float yledgeOffset2 = 0.5f;
    public Vector2 ledgePos1;
    public Vector2 ledgePos2;
    */


    public void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        cc = this.GetComponent<CapsuleCollider2D>();
        pc = this.GetComponent<PlayerCombat>();
        EnableMove();
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
        

        if(disableMove==false){
            if( IsTouchingWall() != true|| IsTouchingWall() != true && IsGrounded() != true || IsGrounded() == true ){
                Move();
            }
        }
        
        Jump();

        WallSlide();
        
        WallJump();

        //LedgeClimb();
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
            if(IsTouchingWall() == true && facingRight == false){
                return;
            }else{
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            facingRight = false;
            wallCheckDistance = -0.7f;
            FindObjectOfType<TurretFollow>().faceDirection(facingRight);
            }
        }
        else if (Input.GetKey(KeyCode.D)){
            if(IsGrounded()){
                action = State.run;
            }
            if(IsTouchingWall() == true && facingRight == true){
                return;
            }else{
                rb.velocity = new Vector2(+speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
                facingRight = true;
                wallCheckDistance = 0.7f;
                FindObjectOfType<TurretFollow>().faceDirection(facingRight);
            }
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

    public void WallSlide(){
        
        if(IsTouchingWall() ==true && IsGrounded() == false){
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            action = State.fall;
        }
    }

    public void WallJump(){
        
        if(IsWallJumping() == true && Input.GetKey(KeyCode.D) && facingRight == false){
            rb.velocity = new Vector2(xWallJumpForce,yWallJumpForce);
            DisableMove(0.15f);
        }
        if(IsWallJumping() == true && Input.GetKey(KeyCode.A) && facingRight == true){
            rb.velocity = new Vector2(-xWallJumpForce,yWallJumpForce);
            DisableMove(0.15f);
        }
        
    }

    private bool IsGrounded() {

        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,platformLayerMask);

        if (isGrounded == true)
        {
            action = State.idle;
            //Debug.Log("true");
            return true;
        }
        else {
            //Debug.Log("false");
            if (rb.velocity.y < 0) {
                action = State.fall;
            }
            else if (rb.velocity.y > 0) {
                action = State.jump;

            }
        }
        return isGrounded;
    }

    public bool IsTouchingWall(){
        bool touchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, platformLayerMask);

        return touchingWall;
    }

    public bool IsWallJumping(){
        bool walljumping = false; 
        if(Input.GetKey(KeyCode.W) && IsTouchingWall()){
            walljumping = true;
        }
        else {
            walljumping = false;
        }
        return walljumping;
    }

    public bool IsGrappling(){
        bool isGrappling = this.GetComponent<GrapplingGun>().isGrappling;
        return isGrappling;
    }

    public void DisableMove(float time){
        disableMove = true;
        Invoke("EnableMove", time);
    }

    public void DisableMove(){
        disableMove = true;
    }

    public void EnableMove(){
        disableMove = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(cc.bounds.center,new Vector2(cc.bounds.center.x,cc.bounds.center.y-0.8f));
        //Gizmos.DrawLine(cc.bounds.center,new Vector2(cc.bounds.center.x+0.8f,cc.bounds.center.y));
        Gizmos.DrawLine(wallCheck.position,new Vector2(wallCheck.position.x + wallCheckDistance,wallCheck.position.y));
    }

    /*
    public void LedgeClimb(){
        if(IsTouchingWall()== true && IsTouchingLedge() == false){
            Vector2 ledgePosBot = wallCheck.position;
            if(facingRight == true){
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - xledgeOffset1 , Mathf.Floor(ledgePosBot.y) + yledgeOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + xledgeOffset2 , Mathf.Floor(ledgePosBot.y) + yledgeOffset2);
            }
            else if (facingRight == false){
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + xledgeOffset1, Mathf.Floor(ledgePosBot.y) + yledgeOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - xledgeOffset1, Mathf.Floor(ledgePosBot.y) + yledgeOffset1);
            }
            DisableMove(0.3f);
            anim.SetBool("ClimbLedge", true);
            FinishLedge();
        }
    }
    public void FinishLedge(){
        transform.position = ledgePos2;
        anim.SetBool("ClimbLedge", false);
    }
    public bool IsTouchingLedge(){
        bool ledge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, platformLayerMask);
        return ledge;
    }
    */

}
