using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //Variables
    public float lifeTime = 3f;
    public float bulletDamage = 35f;
    public float bulletForce = 20f;

    //Components
    private Transform player; 
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();

        Vector3 direction = player.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletForce;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        Invoke("Destroy",lifeTime);
    }

    public void Destroy(){
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D colObj) {
        if (colObj.gameObject.CompareTag("Player")){
            colObj.gameObject.GetComponent<Health>().takeDamage(bulletDamage);
            Destroy();
        }
        else{
            Destroy();
        }
    }

}
