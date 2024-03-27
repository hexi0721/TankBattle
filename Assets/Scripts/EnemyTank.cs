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

    public int tmp;

    [SerializeField] Vector3 _TargetPos; // 存放前往的目標位置
    [SerializeField] Vector3 _LastUpdatePos;
    [SerializeField] bool _EnemyFound;
    [SerializeField] float _StandbyTime;

    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        _Agent = GetComponent<NavMeshAgent>();

        _TargetPos = transform.position;
        _EnemyFound = false;
        _StandbyTime = 2f;
    }

    private void Update()
    {
        
        

        RaycastHit hit;

        if(_Player != null)
        {
            if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 50, ~(1 << 11 | 1 << 12)))
            {

                //if(hit.collider != null) { Debug.Log(hit.collider.name); }

                if (hit.collider.CompareTag("Player"))
                {
                    //_Agent.enabled = true;
                    _Agent.stoppingDistance = 50f;
                    // 如果碰撞到會是player位置，沒碰撞到則是player最後消失的位置
                    _TargetPos = _Player.transform.position;
                    _EnemyFound = true;
                    
                    if (true)
                    {
                        // 距離多近 開火
                    }
                }
                else if (_EnemyFound == true)  // 到達玩家消失地點 如果沒發現玩家 standby結束完 進入巡邏狀態
                {
                    
                    _Agent.stoppingDistance = 0f;

                    if (_LastUpdatePos == transform.position)
                    {
                        _StandbyTime -= Time.deltaTime;

                        if (_StandbyTime < 0f)
                        {
                            _StandbyTime = 2f;
                            _EnemyFound = false;
                        }
                    }

                }

            }
            else
            {
                _EnemyFound = false; // 防止碰撞出現BUG
            }
            
            if (!_EnemyFound) // 巡邏
            {
                _Agent.stoppingDistance = 0f;

                if (_LastUpdatePos == transform.position)
                {
                    _StandbyTime -= Time.deltaTime;

                    if (_StandbyTime < 0f)
                    {
                        _StandbyTime = 2f;
                        Vector3 _MVRAND = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

                        _TargetPos += _MVRAND;
                    }
                }

            }



        }

        _LastUpdatePos = transform.position; // 紀錄上一幀位置

        
        _Agent.SetDestination(_TargetPos);
        
       

        

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
