using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float offset = 0;
    public enum State { pistol, machinegun, disabled }
    public State gunState = State.pistol;
    public GameObject standardBullet;
    public GameObject firePoint;

    void Update()
    {
        GunRotation();
        Shoot();
    }

    public void GunRotation(){
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.position;
        float rotZ = Mathf.Atan2(difference.x, difference.y) * -Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }

    public void Shoot(){
        if (Input.GetMouseButtonDown(0)){
            if(gunState == State.pistol){
                PistolBullet();
            }
        }
    }

    public void PistolBullet(){
        GameObject bullet = Instantiate(standardBullet,firePoint.transform.position, transform.rotation);
        bullet.transform.position = firePoint.transform.position;
    

        //bullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * standardBulletSpeed;
    }
}
