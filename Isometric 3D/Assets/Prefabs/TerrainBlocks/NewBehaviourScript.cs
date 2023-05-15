using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    void OnMouseEnter(){
        this.gameObject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
    }
}
