using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPipe : MonoBehaviour
{
    private GameObject startPipe;
    [SerializeField] GameObject source;
    private float rayDist = 0.01f;

    void Start(){
        ShootRay();
        setConnected();
    }

    public void RefindPath(){
        ShootRay();
        setConnected();
    }

    void ShootRay()
    {
        RaycastHit2D rayCast;
        rayCast = Physics2D.Raycast(source.transform.position, source.transform.TransformDirection(Vector2.down), rayDist);
        if(Physics2D.Raycast(source.transform.position, source.transform.TransformDirection(Vector2.down), rayDist)){
            startPipe = rayCast.collider.gameObject;
        }
    }

    void setConnected(){
        startPipe.GetComponent<PipeLogic>().setConnected(true);
        startPipe.GetComponent<PipeLogic>().ShootRay();
    }
}