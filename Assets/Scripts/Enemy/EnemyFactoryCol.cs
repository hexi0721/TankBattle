using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryCol : MonoBehaviour
{

    public EnemyFactory EnemyFactoryScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {

            EnemyFactoryScript.Hp -= Random.Range(1, 5);

            if (EnemyFactoryScript.Hp < 0)
            {
                Destroy(this.gameObject.transform.parent.gameObject);
                Destroy(GameObject.Find("EnemyFactory").gameObject);
            }
        }



    }
}
