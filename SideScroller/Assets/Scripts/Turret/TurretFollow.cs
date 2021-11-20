using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFollow : MonoBehaviour
{
    public float speed;
    public Transform target;
    public bool moveup = true;
    private Vector2 newPosition;
    private IEnumerator coroutine;
    
    public GameObject gun;


    void Start()
    {
        MoveTimer();
        
    }

    void FixedUpdate()
    {
        MoveTargetPosition();
        MoveTowardTarget();
    }

    public void MoveTowardTarget(){
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void faceDirection(bool facingRight){
        if(facingRight == true){
            transform.localScale = new Vector2(1, 1);
            gun.transform.localScale = new Vector2(1, 1);
        }
        else if(facingRight == false){
            transform.localScale = new Vector2(-1, 1);
            gun.transform.localScale = new Vector2(-1, -1);
        }
    }

    public void MoveTimer(){
        coroutine = NewPos(.4f);
        StartCoroutine(coroutine);
    }

    public void MoveTargetPosition(){
        target.transform.position = Vector2.MoveTowards(target.transform.position, newPosition, Time.deltaTime * .2f);
    }

    IEnumerator NewPos(float _waitTime){

        yield return new WaitForSeconds(_waitTime);

        if(moveup == true){
            newPosition = new Vector2(target.transform.position.x,target.transform.position.y + 1f);
            moveup = false;
        }
        else if (moveup == false){
            newPosition = new Vector2(target.transform.position.x,target.transform.position.y - 1f);
            moveup = true;
        }

        MoveTimer();
    }
}
