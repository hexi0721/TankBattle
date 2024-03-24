using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public float Hp;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            
            Hp -= Random.Range(1, 5);
            


            if (Hp < 0)
            {
                Destroy(this.gameObject);
            }
        }

        

    }


}
