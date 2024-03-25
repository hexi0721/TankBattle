using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyTank : MonoBehaviour
{

    public float Hp;
    public float BulletEnegy;

    GameObject _Player;
    NavMeshAgent _Agent;

    Vector3 _TargetPos; // 存放前往的目標位置


    private void Start()
    {
        Hp = 4.0f;

        _Player = GameObject.FindWithTag("Player");
        _Agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {

        //DistanceCheck();


        RaycastHit hit;

        if(Physics.Raycast(transform.position , _Player.transform.position - transform.position , out hit , 50 , ~(1 << 11  | 1 << 12)) )
        {

            if (hit.collider.CompareTag("Player"))
            {
                _Agent.enabled = true;
                _Agent.stoppingDistance = 50f;
                // 如果碰撞到會是player位置，沒碰撞到則是player最後消失的位置
                _TargetPos = _Player.transform.position; 
            }
            else
            {
                Debug.Log(2);
                _Agent.stoppingDistance = 0f;
            }
        }


        if (_Agent.isActiveAndEnabled)
        {
            _Agent.SetDestination(_TargetPos);
        }

    }

    private void DistanceCheck()
    {
        float _Distance = Vector3.Distance(_Player.transform.position, transform.position);
        //Debug.Log(_Distance);

        if (_Distance > 80f)
        {
            _Agent.enabled = false;
        }
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
