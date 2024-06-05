using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DisableEnemyTankAction : MonoBehaviour
{

    private GameManage gameManage;
    
    public Behaviour EnemyTankScript , EnemyShootScript ;
    private NavMeshAgent meshAgent;
    private NavMeshObstacle obstacle;
    

    private void Start()
    {
        gameManage = GameObject.Find("GameManage").GetComponent<GameManage>();
        meshAgent = GetComponent<NavMeshAgent>();
        obstacle= GetComponent<NavMeshObstacle>();
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

            
            

        }
        else
        {
            


            EnemyTankScript.enabled = true;
            EnemyShootScript.enabled = true;

            
        }
        





    }




}
