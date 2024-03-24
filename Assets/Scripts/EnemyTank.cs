using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyTank : MonoBehaviour
{

    public float Hp;
    public float BulletEnegy;

    GameObject _Player;
    NavMeshAgent _Agent; 



    private void Start()
    {
        Hp = 4.0f;

        _Player = GameObject.FindWithTag("Player");
        _Agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {

        _Agent.SetDestination(_Player.transform.position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {

            Hp -= Random.Range(1, 4);



            if (Hp < 0)
            {
                Destroy(this.gameObject);
            }
        }


    }


}
