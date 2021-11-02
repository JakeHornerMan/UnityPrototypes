using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBulletBehaviour : MonoBehaviour
{
    public float BulletSpeed = 10f;
    public float lifeTime = 5f;

    private Rigidbody2D rb;

    Vector3 targetPosition;

    public GameObject destroyEffect;
    
    public void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        Invoke("Destroy",lifeTime);
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        //transform.position += (targetPosition - transform.position).normalized * BulletSpeed * Time.deltaTime;
        //transform.Translate(transform.up * BulletSpeed * Time.deltaTime);
        rb.velocity = new Vector2((Time.deltaTime * 1000)* BulletSpeed, rb.velocity.y);
    }

    public void Destroy(){
        //Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
