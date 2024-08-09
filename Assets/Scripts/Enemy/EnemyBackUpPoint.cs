using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackUpPoint : MonoBehaviour
{

    GameManage gameManage;

    private void Start()
    {
        gameManage = GameObject.FindWithTag("GameManage").GetComponent<GameManage>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            gameManage.BackUpTrigger = true;
            Destroy(gameObject);
        }
    }


}
