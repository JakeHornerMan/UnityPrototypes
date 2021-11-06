using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    public float lifeTime = 3f;

    public float bulletDamage = 35f;

    void Start()
    {
        Invoke("Destroy",lifeTime);
    }

    public void Destroy(){
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D colObj) {
        if (colObj.gameObject.tag == "Enemy"){
            colObj.gameObject.GetComponent<Health>().takeDamage(bulletDamage);
            Destroy();
        }
        else{
            Destroy();
        }
    }
}
