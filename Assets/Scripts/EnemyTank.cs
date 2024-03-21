using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{

    public float Hp;
    public float BulletEnegy;

    private void Start()
    {
        Hp = 4.0f;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {

            Hp -= Random.Range(1, 4);






            if (Hp < 0)
            {
                Destroy(this.gameObject);
            }
        }


    }


}
