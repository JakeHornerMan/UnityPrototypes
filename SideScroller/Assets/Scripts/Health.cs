using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void takeDamage(float inputDamage)
    {
        health -= inputDamage;
        if (health <= 0) //when health hits 0
        {
            Death();  
        }
    }

    public void Death(){
        Destroy(gameObject);
    }
}
