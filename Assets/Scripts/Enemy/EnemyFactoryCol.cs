using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFactoryCol : MonoBehaviour
{
    
    public EnemyFactory EnemyFactoryScript;

    private void Start()
    {
        EnemyFactoryScript = GameObject.FindWithTag("EnemyFactory").GetComponent<EnemyFactory>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {

            EnemyFactoryScript.Hp -= Random.Range(1, 5);
            EnemyFactoryScript.FacHp.transform.parent.gameObject.SetActive(true);
            EnemyFactoryScript.HpShowTime = 4f;

            if (EnemyFactoryScript.Hp < 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }



    }
}
