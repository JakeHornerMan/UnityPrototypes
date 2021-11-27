using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private Camera cam;
    private DistanceJoint2D dj;
    private LineRenderer lr;
    private Rigidbody2D rb;
    private PlayerController pc;

    private Transform playerPos;

    [SerializeField] private LayerMask platformLayerMask;
    private Vector3 mousePos;
    private Vector3 targetPos;
    private Vector3 pointPos;
    public bool isGrappling;
    private bool isGrappleable;

    public float range = 20f;
    private bool detatchable;
    public float detatchJump = 15f;

    void Start()
    {
        cam = Camera.main;
        dj = this.GetComponent<DistanceJoint2D>();
        lr = this.GetComponent<LineRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        pc = this.GetComponent<PlayerController>();
        playerPos = this.GetComponent<Transform>(); 
        
        dj.enabled = false;
        isGrappling = false;
        lr.positionCount = 0;
    }

    void Update()
    { 
        MousePosition();
        IsGrappleable();    
        Grapple();
        Detatch();
    }

    public void MousePosition(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Grapple(){
        
        if(Input.GetButtonDown("Fire2") && isGrappling == false && isGrappleable == true){
            dj.enabled = true;
            dj.connectedAnchor = mousePos;
            isGrappling = true;
            lr.positionCount = 2;
            pointPos = targetPos;
            pc.DisableMove();
            detatchable = false;
            Invoke("EnableDetatch",0.5f);
        }
        else if (Input.GetButtonDown("Fire2") && isGrappling == true && isGrappleable == true){
            dj.enabled = false;
            isGrappling = false;
            lr.positionCount = 0;
            rb.velocity = Vector2.up * detatchJump;
            
            dj.enabled = true;
            dj.connectedAnchor = mousePos;
            isGrappling = true;
            lr.positionCount = 2;
            pointPos = targetPos;
            pc.DisableMove();
            detatchable = false;
            Invoke("EnableDetatch",0.5f);
        }
        DrawLine();
    }

    public void Detatch(){
        if(Input.GetKey(KeyCode.W) && isGrappling == true && detatchable == true){
            dj.enabled = false;
            isGrappling = false;
            lr.positionCount = 0;
            pc.EnableMove();
            rb.velocity = Vector2.up * detatchJump;
        }
    }

    public void DrawLine(){
        if(lr.positionCount<=0) return;
        lr.SetPosition(0,transform.position);
        lr.SetPosition(1,pointPos);
    }

    public void IsGrappleable(){
    
        Vector3 addDir1 = new Vector3(0f,0.5f,0f);
        Vector3 addDir2 = new Vector3(0f,1f,0f);

        Vector3 direction = mousePos - playerPos.position;
        RaycastHit2D hit = Physics2D.Raycast(playerPos.position, direction, range,platformLayerMask);
        RaycastHit2D hit1 = Physics2D.Raycast(playerPos.position, direction + addDir1 , range,platformLayerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(playerPos.position, direction + addDir2 , range,platformLayerMask);
        RaycastHit2D hit3 = Physics2D.Raycast(playerPos.position, direction - addDir1 , range,platformLayerMask);
        RaycastHit2D hit4 = Physics2D.Raycast(playerPos.position, direction - addDir2 , range,platformLayerMask);
        if (hit){
            isGrappleable = true;
            Debug.DrawRay(playerPos.position, direction, Color.green);
            targetPos = hit.point;
        }
        else if (hit1){
            isGrappleable = true;
            Debug.DrawRay(playerPos.position, direction + addDir1 , Color.green);
            targetPos = hit1.point;
        }
        else if (hit2){
            isGrappleable = true;
            Debug.DrawRay(playerPos.position, direction + addDir2, Color.green);
            targetPos = hit2.point;
        }
        else if (hit3){
            isGrappleable = true;
            Debug.DrawRay(playerPos.position, direction - addDir1 , Color.green);
            targetPos = hit3.point;
        }
        else if (hit4){
            isGrappleable = true;
            Debug.DrawRay(playerPos.position, direction - addDir2, Color.green);
            targetPos = hit4.point;
        }
        else{
            isGrappleable = false;
        }
    }

    public void EnableDetatch(){
        detatchable = true;
    }  
}