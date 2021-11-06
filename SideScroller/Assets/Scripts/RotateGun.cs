using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public Camera cam;
    private Rigidbody2D rb;
    private Vector2 mousePos;

    private float offset = 90f;

    public void Start(){
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition); 
    }

    public void FixedUpdate(){
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg - offset;
        rb.rotation = -angle;
    }
}
