using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private IEnumerator coroutine;
    private Animator anim;

    public Transform attackPoint;
    public float basicHitRange = 1f;
    public float basicHitStrength = 25f;
    public float basicHitTime = 0.33f;
    public LayerMask enemyLayers;

    void Start()
    {
       anim = this.GetComponent<Animator>();
    }

    public void BasicHit(){
       anim.SetTrigger("Attack1");
       Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position,basicHitRange,enemyLayers);
       foreach(Collider2D enemy in hitEnemys){
           Debug.Log("Attack1 hit : " + enemy.name);
           waitandAttack(basicHitTime);
           enemy.GetComponent<Health>().takeDamage(basicHitStrength);
       }
    }    
    IEnumerator waitandAttack(float _waitTime){
        yield return new WaitForSeconds(_waitTime);
    }

    /*public void BasicBullet(){
        GameObject bullet = Instantiate(standardBullet);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.Euler(0,0,lookAngle);

        bullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * standardBulletSpeed;
    }*/
}
