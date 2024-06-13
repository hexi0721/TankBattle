using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackUpSpot : MonoBehaviour
{

    public Timer timer;

    public InstantiateTank instantiateTank;

    public static bool _backupStart;


    private void Start()
    {
        instantiateTank = GameObject.Find("EnemyFactory").GetComponent<InstantiateTank>();
        _backupStart = false;
    }

    private void Update()
    {
        

        if(timer.EnemyBackUpTime <= 0 && _backupStart == true)
        {
            instantiateTank.BuildTank(new List<Vector3>() { transform.position });
            _backupStart = false;
        }

        

        

    }





}
