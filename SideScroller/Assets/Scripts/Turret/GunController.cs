using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint;

    public enum State { pistol, machinegun, disabled }
    public State gunState = State.pistol;

    public GameObject standardBullet;
    public float standardBulletForce = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    public void Shoot(){
        if(gunState == State.pistol){
            StandardBullet();
        }
    }

    public void StandardBullet(){
        GameObject bullet = Instantiate(standardBullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(firePoint.right * standardBulletForce, ForceMode2D.Impulse);
    }

}
