using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform target;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);        
    }
}
