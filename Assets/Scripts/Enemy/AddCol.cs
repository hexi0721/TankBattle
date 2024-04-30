using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCol : MonoBehaviour
{
    
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<EnemyFactoryCol>();
            transform.GetChild(i).gameObject.GetComponent<EnemyFactoryCol>().EnemyFactoryScript = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();
        }
    }


}
