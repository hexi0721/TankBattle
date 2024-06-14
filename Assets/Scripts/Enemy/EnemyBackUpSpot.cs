using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackUpSpot : MonoBehaviour
{

    public Timer timer;

    public InstantiateTank instantiateTank;
    GameManage gameManage;

    [SerializeField] float _appearTime;

    private void Start()
    {
        instantiateTank = GameObject.Find("EnemyFactory").GetComponent<InstantiateTank>();
        timer = GameObject.Find("GameManage").GetComponent<Timer>();
        gameManage = GameObject.Find("GameManage").GetComponent<GameManage>();
        
        _appearTime = 0f;
        
    }

    private void Update()
    {
        
        if(_appearTime > 0f)
        {
            _appearTime -= Time.deltaTime;
        }


        if (timer.EnemyBackUpTime <= 0f && gameManage.BackUpTrigger && _appearTime <= 0f)
        {
            instantiateTank.BuildTank(new List<Vector3>() { transform.position });
            _appearTime = 30f;
            
        }

        

        

    }





}
