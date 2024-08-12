using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpot : MonoBehaviour 
{

    public InstantiateTank instantiateTank;

    void Start()
    {
        instantiateTank = GameObject.FindWithTag("EnemyFactory").GetComponent<InstantiateTank>();

        instantiateTank.BuildTank( new List<Vector3>() { transform.position });
    }


}
