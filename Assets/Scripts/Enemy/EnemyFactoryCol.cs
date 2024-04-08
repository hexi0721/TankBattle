using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryCol : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {

            EnemyFactory.Hp -= Random.Range(1, 5);



            if (EnemyFactory.Hp < 0)
            {
                Destroy(this.gameObject.transform.parent.gameObject);
                Destroy(GameObject.Find("EnemyFactory").gameObject);
            }
        }



    }
}
