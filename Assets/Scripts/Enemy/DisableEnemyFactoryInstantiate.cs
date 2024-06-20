using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnemyFactoryInstantiate : MonoBehaviour
{
    private GameManage gameManage;
    Behaviour InstantiateTankScript , EnemyFactoryScript;

    private void Start()
    {
        gameManage = GameObject.Find("GameManage").GetComponent<GameManage>();
        InstantiateTankScript = GetComponent<InstantiateTank>();
        EnemyFactoryScript = GetComponent<EnemyFactory>();
    }

    private void Update()
    {

        if (gameManage.IsOpenMenu)
        {
            InstantiateTankScript.enabled = false;
            EnemyFactoryScript.enabled = false;
        }
        else
        {
            InstantiateTankScript.enabled = true;
            EnemyFactoryScript.enabled = true;
        }
    }


}
