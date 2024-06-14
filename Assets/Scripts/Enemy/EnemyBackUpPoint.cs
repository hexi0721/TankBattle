using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackUpPoint : MonoBehaviour
{

    public GameManage gameManage;


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            gameManage.BackUpTrigger = true;
            Destroy(this.gameObject);
        }
    }


}
