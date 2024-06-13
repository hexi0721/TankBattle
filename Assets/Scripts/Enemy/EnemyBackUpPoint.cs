using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackUpPoint : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            EnemyBackUpSpot._backupStart = true;
            Destroy(this.gameObject);
        }
    }


}
