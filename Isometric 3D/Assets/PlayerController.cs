using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider cc;


    [SerializeField]
    LayerMask platformLayerMask;
    float moveSpeed = 4f;
    float jumpForce = 5f;
    public float fallMultiplier = 5.0f;
    public float lowJumpMultiplier = 5.0f;
    Vector3 foreward;
    Vector3 right;

    float distToGround;




    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        cc = this.GetComponent<CapsuleCollider>();

        foreward = Camera.main.transform.forward;
        foreward.y=0;
        foreward = Vector3.Normalize(foreward);
        right = Quaternion.Euler(new Vector3(0,90,0)) * foreward;

        distToGround = cc.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            Move();
            Jump();
        }
    }

    void Move(){
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 forewardMovement = foreward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + forewardMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += forewardMovement;
    }

    public void Jump(){
        if (Input.GetKey(KeyCode.Space) && IsGrounded()){
            rb.velocity = Vector2.up * jumpForce;
        }

        //using gravity for hold jump and to fall faster
        /*if(rb.velocity.y<0){
            rb.velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplier-1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.W)){
            rb.velocity += Vector3.up * Physics2D.gravity.y * (lowJumpMultiplier-1) * Time.deltaTime;
        }*/
    }

    /*private bool IsGrounded() {

        float extraHeightText = 0.1f;
        RaycastHit raycastHit = 
            Physics.BoxCast(cc.bounds.center, cc.bounds.size, 0f, Vector3.down, extraHeightText, platformLayerMask);
            Color rayColor;

        if (raycastHit.collider != null)
        {
            return true;
        }
        else {
            return false;
        }
    }*/
    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
