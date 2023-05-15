using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100f;

    // Update is called once per frame
    public void takeDamage(float inputDamage)
    {
        Debug.Log("Object "+ this.name +", hurt for : "+ inputDamage);
        health -= inputDamage;
        if (health <= 0) //when health hits 0
        {
            Death();  
        }
    }

    public void Death(){
        Debug.Log("Death of: "+ gameObject.name);
        Destroy(gameObject);
    }
}
