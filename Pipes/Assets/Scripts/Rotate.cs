using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public int rotationDirection = -1; 
    // -1 for clockwise, 1 for anti-clockwise
    public int rotationStep = 90; 
    private Vector3 currentRotation, targetRotation;
    private PipeLogic pipeLogic;
    private bool activePipe;
    [SerializeField] GameObject highlight;

    void Start()
    {
        pipeLogic = this.GetComponent<PipeLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && activePipe){
            rotateObject();
            pipeLogic.ShootRay();
        }
    }


    private void rotateObject()
    {
        currentRotation = this.gameObject.transform.eulerAngles;
        targetRotation.z = (currentRotation.z + (90 * rotationDirection));
        currentRotation.z += (rotationStep * rotationDirection);
        gameObject.transform.eulerAngles = currentRotation;
        // source1.transform.eulerAngles = currentRotation;
    }
    
    void OnMouseEnter(){
        activePipe = true;
        highlight.SetActive(true);
    }

    void OnMouseExit(){
        activePipe = false;
        highlight.SetActive(false);
    }
}
