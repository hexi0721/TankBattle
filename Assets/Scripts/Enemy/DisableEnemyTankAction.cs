using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DisableEnemyTankAction : MonoBehaviour
{

    GameManage gameManage;
    
    Behaviour EnemyTankScript , WhenEnemyTankDestroyScript;
    NavMeshAgent meshAgent;
    
    bool  _ExitMenu;

    public bool ExitMenu
    {
        get => _ExitMenu;
        set => _ExitMenu = value;
    }

    private void Start()
    {
        gameManage = GameObject.Find("GameManage").GetComponent<GameManage>();
        EnemyTankScript = GetComponent<EnemyTank>();
        WhenEnemyTankDestroyScript = GetComponent<WhenEnemyTankDestroy>();
        meshAgent = GetComponent<NavMeshAgent>();

        _ExitMenu = false;
    }

    private void Update()
    {
        if (gameManage.IsOpenMenu)
        {
            if(meshAgent.enabled)
            {
                meshAgent.SetDestination(transform.position);
            }
            
            EnemyTankScript.enabled = false;
            WhenEnemyTankDestroyScript.enabled = false;
            _ExitMenu = true;

        }
        else
        {
            EnemyTankScript.enabled = true;
            WhenEnemyTankDestroyScript.enabled = true;
        }


    }
}
