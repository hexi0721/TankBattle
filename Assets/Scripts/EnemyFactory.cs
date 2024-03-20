using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public float Hp = 5;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Hp -= 1.0f;

            if (Hp < 0)
            {
                Destroy(this.gameObject);
            }
        }

        

    }


}
