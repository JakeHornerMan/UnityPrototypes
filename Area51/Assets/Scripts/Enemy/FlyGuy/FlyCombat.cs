using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCombat : MonoBehaviour
{
    // Flags
    private bool disableShooting = false;
    private Vector2 directionToPlayer;
    
    // Variables
    public float laserWidth = 0.001f; // Width of the laser beam
    public float shootingDistance = 15f; // Distance at which the enemy detects the player
    
    //Components
    private Transform player; 
    private LineRenderer laserRenderer; // Reference to the LineRenderer component
    public GameObject bullet;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        laserRenderer = this.GetComponent<LineRenderer>();

        randomTimeShoot();
    }

    void FixedUpdate()
    {
        directionToPlayer = player.position - transform.position;
        if(!IsPlayerClose()){
            laserRenderer.enabled = false;
            disableShooting = true;
        }
        else{
            laserRenderer.enabled = true;
            disableShooting = false;
            CanIShoot();
            LaserToPlayer();
        }
    }

    public void Shoot(){
        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
    }

    public void LaserToPlayer(){
        laserRenderer.startWidth = laserWidth;
        laserRenderer.endWidth = laserWidth;
        laserRenderer.startColor = Color.red;
        laserRenderer.endColor = Color.red;
        laserRenderer.SetPosition(0, transform.position);
        laserRenderer.SetPosition(1, player.position);
    }

    IEnumerator WaitAndShoot(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(!disableShooting){
            Shoot();
        }
        randomTimeShoot();
    }

    public void randomTimeShoot(){
        float randoTime = Random.Range(2f, 4f);
        IEnumerator coroutine = WaitAndShoot(randoTime);
        StartCoroutine(coroutine);
    }

    public bool IsPlayerClose(){
        if (directionToPlayer.magnitude <= shootingDistance)
        {
            return true;
        }
        return false;
    }

    public bool CanIShoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, Mathf.Infinity);
        if (hit.collider.gameObject.name == "Platform")
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            return false;
        }
        return true;
    }
}
