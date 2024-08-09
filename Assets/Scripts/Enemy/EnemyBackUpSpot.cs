using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackUpSpot : MonoBehaviour
{

    public InstantiateTank instantiateTank;
    GameManage gameManage;

    [SerializeField] float _appearTime;

    private void Start()
    {
        instantiateTank = GameObject.Find("EnemyFactory").GetComponent<InstantiateTank>();
        gameManage = GameObject.Find("GameManage").GetComponent<GameManage>();
        
        _appearTime = 0f;
        
    }

    private void Update()
    {
        if (gameManage.IsOpenMenu == false)
        {
            if (gameManage.BackUpTrigger == true && _appearTime <= 0f)
            {
                instantiateTank.BuildTank(new List<Vector3>() { transform.position });
                _appearTime = 30f;

            }
            else if (_appearTime > 0f)
            {
                _appearTime -= Time.deltaTime;
            }
        }

    }

}
