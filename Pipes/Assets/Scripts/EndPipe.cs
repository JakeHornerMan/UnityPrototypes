using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPipe : MonoBehaviour
{
    [SerializeField] GameObject source1;
    private GameObject finalPipe;
    private bool gameFinished = false;
    private float rayDist = 0.01f;

    void Update()
    {
        CheckPipes();
        if(finalPipe != null){
            gameFinished = IsFinalPipeConnected();
        }
        if(gameFinished){
            // Debug.LogWarning("GAME FIN!!!!!");
        }
    }

    public void CheckPipes(){
        finalPipe = RayCastUp();
    }

    private GameObject RayCastUp() {
        RaycastHit2D rayCastUp;
        rayCastUp = Physics2D.Raycast(source1.transform.position, source1.transform.TransformDirection(Vector2.up), rayDist);
        
        if(Physics2D.Raycast(source1.transform.position, source1.transform.TransformDirection(Vector2.up), rayDist)){
            return rayCastUp.collider.gameObject;
        }
        else{
            return null;
        }
    }

    public bool IsFinalPipeConnected(){
        if(finalPipe != null){
            return finalPipe.GetComponent<PipeLogic>().getConnected();
        }
        else{
            return false;
        }
    }
}
