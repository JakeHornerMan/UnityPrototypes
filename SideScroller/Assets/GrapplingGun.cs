using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private Camera cam;
    private DistanceJoint2D dj;
    private LineRenderer lr;
    private Rigidbody2D rb;

    [SerializeField] private LayerMask platformLayerMask;
    private Vector2 mousePos;
    private Vector2 tempPos;
    public bool isGrappling;

    void Start()
    {
        cam = Camera.main;
        dj = GetComponent<DistanceJoint2D>();
        lr = GetComponent<LineRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        
        dj.enabled = false;
        isGrappling = false;
        lr.positionCount = 0;
    }

    void Update()
    { 
        MousePosition();    
        Grapple();
    }

    public void MousePosition(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Grapple(){
        RaycastHit2D hit = Physics2D.Raycast(cam.transform.position, mousePos, Mathf.Infinity, platformLayerMask);
        if(Input.GetButtonDown("Fire2") && isGrappling == false && hit == true){
            dj.enabled = true;
            dj.connectedAnchor = mousePos;
            isGrappling = true;
            lr.positionCount = 2;
            tempPos = mousePos;
        }
        else if(Input.GetButtonDown("Fire2") && isGrappling == true){
            dj.enabled = false;
            isGrappling = false;
            lr.positionCount = 0;
        }
        DrawLine();
    }

    public void DrawLine(){
        if(lr.positionCount<=0) return;
        lr.SetPosition(0,transform.position);
        lr.SetPosition(1,tempPos);
    }
    

}