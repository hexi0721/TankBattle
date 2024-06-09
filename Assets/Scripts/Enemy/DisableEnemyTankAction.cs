using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DisableEnemyTankAction : MonoBehaviour
{

    private GameManage gameManage;
    
    public Behaviour EnemyTankScript , EnemyShootScript ;
    private EnemyTank enemyTank;
    private NavMeshAgent meshAgent;
    private NavMeshObstacle obstacle;
    [SerializeField]bool  _ExitMenu;
    public bool ExitMenu
    {
        get => _ExitMenu;
        set => _ExitMenu = value;
    }

    private void Start()
    {
        gameManage = GameObject.Find("GameManage").GetComponent<GameManage>();
        enemyTank = GetComponent<EnemyTank>();
        meshAgent = GetComponent<NavMeshAgent>();
        obstacle= GetComponent<NavMeshObstacle>();
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
            EnemyShootScript.enabled = false;

            _ExitMenu = true;



        }
        else
        {


            EnemyTankScript.enabled = true;
            EnemyShootScript.enabled = true;


        }
        





    }




}
