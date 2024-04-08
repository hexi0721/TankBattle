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
    public EnemyTurretRotation EnemyTurretRotationScript;
    public GameObject Muzzle;

    EnemyFactory _EnemyFactoryScript;
    EnemyShoot _EnemyShootScript;
    GameObject _Player;
    NavMeshAgent _Agent;

    [SerializeField] string _State;

    [SerializeField] Vector3 _TargetPos; // 存放前往的目標位置
    [SerializeField] Vector3 _LastUpdatePos; // 上一幀位置
    [SerializeField] float _StandbyTime; // 待命時間

    RaycastHit hit;
    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        _Agent = GetComponent<NavMeshAgent>();
        _EnemyShootScript = GetComponent<EnemyShoot>();
        _EnemyFactoryScript = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();

        _State = "Patrol";
        _TargetPos = transform.position;
        _StandbyTime = 2f;
        
    }
    private void Update()
    {
        if (_EnemyFactoryScript.Hp < _EnemyFactoryScript.GetMaxHp())
        {
            _State = "FacUnderAttack";
        }
    }
    private void LateUpdate()
    {

        if (_Player != null)
        {
            if (_State != "FacUnderAttack")
            {
                if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 100f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
                {
                    if (hit.collider != null && hit.collider.CompareTag("Player"))
                    {
                        _Agent.stoppingDistance = 70f;
                        // 如果碰撞到會是player位置，沒碰撞到則是player最後消失的位置
                        _TargetPos = _Player.transform.position;
                        _State = "EnemyFound";
                    }
                }
            }

            switch (_State)
            {
                case "Patrol":

                    EnemyTurretRotationScript.PatrolStat();
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
                    else // 修改停頓機制
                    {

                    }

                    break;

                case "EnemyFound":

                    AttackAction();

                    break;

                case "UnderAttack":
                    _TargetPos = _Player.transform.position;

                    break;

                case "FacUnderAttack":
                    _Agent.stoppingDistance = 70f;
                    _TargetPos = _Player.transform.position;

                    AttackAction();
                    


                    break;

            }
        }

        _LastUpdatePos = transform.position; // 紀錄上一幀位置

        _Agent.SetDestination(_TargetPos);
        
    }

    private void AttackAction()
    {
        // 距離多近 開火
        float _Distance = Vector3.Distance(_Player.transform.position, transform.position);

        if (_Distance <= _Agent.stoppingDistance)
        {
            EnemyTurretRotationScript.LookPlayer();

            RaycastHit hit2;

            if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out hit2, 70f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
            {
                //Debug.Log(hit2.collider.name);
                Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward * 100, Color.blue);

                if (hit2.collider != null && hit2.collider.CompareTag("Player"))
                {
                    _EnemyShootScript.Shooting(BulletEnegy, hit2.point);
                }
            }

            _EnemyShootScript.Reloading(BulletEnegy);

        }

        if (hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            EnemyTurretRotationScript.PatrolStat();
            _Agent.stoppingDistance = 0f;

            if (_LastUpdatePos == transform.position)
            {
                _StandbyTime -= Time.deltaTime;

                if (_StandbyTime < 0f)
                {
                    _StandbyTime = 2f;
                    _State = "Patrol";
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            _State = "UnderAttack";
            Hp -= Random.Range(1, 4);

            if (Hp < 0)
            {
                Destroy(this.gameObject);
            }
        }


    }


}
