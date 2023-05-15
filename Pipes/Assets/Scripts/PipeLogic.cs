using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeLogic : MonoBehaviour
{
    [SerializeField] GameObject source1;
    [SerializeField] GameObject source2;
    GameObject pipe1 = null;
    GameObject pipe2 = null;
    
    private bool connectedPipe = false;
    private float rayDist = 0.01f;
    [SerializeField] bool turnPipe  = false;
    void Start()
    {
        ShootRay();
    }

    // Update is called once per frame
    void Update()
    {
        if(turnPipe){
            Debug.DrawRay(source1.transform.position, source1.transform.TransformDirection(Vector2.left) * rayDist, Color.red);
        }
        else{
            Debug.DrawRay(source1.transform.position, source1.transform.TransformDirection(Vector2.up) * rayDist, Color.red);
        }
        Debug.DrawRay(source2.transform.position, source2.transform.TransformDirection(Vector2.down) * rayDist, Color.red);
        
        
    }

    public void ShootRay(){
        if(turnPipe){
            pipe1 = RayCastLeft();
        }
        else{
            pipe1 = RayCastUp();
        }
        pipe2 = RayCastDown();

        if(pipe1 || pipe2 != null){
            nexPipeConnection();
        }
        
        if(connectedPipe){
            Debug.Log("Connected: " + transform.position.x + ", " + transform.position.y);
        }
        else{
            Debug.Log("Not Connected");
        }
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

    private GameObject RayCastLeft() {
        RaycastHit2D rayCastLeft;
        rayCastLeft = Physics2D.Raycast(source1.transform.position, source1.transform.TransformDirection(Vector2.left), rayDist);
        
        if(Physics2D.Raycast(source1.transform.position, source1.transform.TransformDirection(Vector2.left), rayDist)){
            return rayCastLeft.collider.gameObject;
        }
        else{
            return null;
        }
    }

    private GameObject RayCastDown(){
        RaycastHit2D rayCastDown;
        rayCastDown = Physics2D.Raycast(source2.transform.position, source2.transform.TransformDirection(Vector2.down), rayDist);

        if(Physics2D.Raycast(source2.transform.position, source2.transform.TransformDirection(Vector2.down), rayDist)){
            return rayCastDown.collider.gameObject;
        }
        else{
            return null;
        }
    }

    // void setConnected(){
    //     connectedPipe  = getConnected();
    // }

    // public bool getConnected(){
    //     if(IsPipe1Connected() || IsPipe2Connected()){
    //         return true;
    //     }
    //     else{
    //         return false;
    //     }
    // }

    public void nexPipeConnection(){
        ShootRay();
        if(pipe1 != null || pipe1.name != "PipeStart"){
            if(!pipe1.GetComponent<PipeLogic>().getConnected()){
                pipe1.GetComponent<PipeLogic>().setConnected(connectedPipe);
                pipe1.GetComponent<PipeLogic>().ShootRay();
            }
        }
        if(pipe2 != null || pipe2.name != "PipeStart"){
            if(!pipe2.GetComponent<PipeLogic>().getConnected()){
                pipe2.GetComponent<PipeLogic>().setConnected(connectedPipe);
                pipe2.GetComponent<PipeLogic>().ShootRay();
            }
        }
    }

    public void setConnected(bool connected){
        connectedPipe = connected;
        nexPipeConnection();
    }

    public bool getConnected(){
        return connectedPipe;
    }

    // public bool IsPipe1Connected(){
    //     if(pipe1 != null){
    //         if(pipe1.name == "PipeStart"){
    //             return true;
    //         }
    //         else{
    //             return pipe1.GetComponent<PipeLogic>().getConnected();
    //         }
    //     }
    //     else{
    //         return false;
    //     }
    // }

    // public bool IsPipe2Connected(){
    //     if(pipe2 != null){
    //         if(pipe2.name == "PipeStart"){
    //             return true;
    //         }
    //         else{
    //             return pipe2.GetComponent<PipeLogic>().getConnected();
    //         }
    //     }
    //     else{
    //         return false;
    //     }
    // }
}
