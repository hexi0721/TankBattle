using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyTank : MonoBehaviour
{
    public float Hp;
    [HideInInspector] public float BulletEnegy;
    public EnemyTurretRotation EnemyTurretRotationScript;
    public GameObject Muzzle;

    EnemyFactory _EnemyFactoryScript;
    EnemyShoot _EnemyShootScript;
    GameObject _Player;
    NavMeshAgent _Agent;
    NavMeshObstacle _Obstacle;

    [SerializeField] string _State;

    
    [SerializeField] float _Distance;
    [SerializeField] Vector3 _TargetPos; // 存放前往的目標位置
    [SerializeField] Vector3 _LastUpdatePos; // 上一幀位置
    [SerializeField] Vector3 _OriginalPos; // 初始位置
    [SerializeField] float _StandbyTime; // 待命時間
    [SerializeField] float _ResetTime; // 重設時間
    [SerializeField] float _LastMoveTime;
    [SerializeField] float _NetxFrameTime;
    [SerializeField] bool _WantToMove;

    RaycastHit hit;
    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        _Agent = GetComponent<NavMeshAgent>();
        _Obstacle = GetComponent<NavMeshObstacle>();
        _Obstacle.enabled = false;
        _EnemyShootScript = GetComponent<EnemyShoot>();
        _EnemyFactoryScript = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();

        _State = "Patrol";
        _TargetPos = transform.position;
        _OriginalPos = transform.position;
        //_LastUpdatePos = transform.position;
        //_LastMoveTime = Time.time;
        _StandbyTime = 2f;
        _ResetTime = 30f;
        _WantToMove = true;
        _NetxFrameTime = 0.2f;

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
                if (Physics.Raycast(transform.position , _Player.transform.position - transform.position, out hit, 100f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
                {
                    //Debug.Log(_Player.transform.name);
                    if (hit.collider != null)
                    {
                        Debug.DrawRay(transform.position, (_Player.transform.position - transform.position) , Color.red);
                        if (hit.collider.transform.CompareTag("Player"))
                        {
                            _Agent.stoppingDistance = 70f;
                            // 如果碰撞到會是player位置，沒碰撞到則是player最後消失的位置
                            _TargetPos = _Player.transform.position;
                            _State = "EnemyFound";
                        }
                        
                        
                    }
                }
            }

            switch (_State)
            {
                case "Patrol": // 巡邏
                    
                    EnemyTurretRotationScript.PatrolStat();
                    _Agent.stoppingDistance = 0f;
                    
                    if (transform.position == _LastUpdatePos && _WantToMove != true) // 停止時刻
                    {
                        
                        _Agent.enabled = false;
                        _Obstacle.enabled = true;
                        
                        _StandbyTime -= Time.deltaTime;

                        if (_StandbyTime < 0f) // 當停留時間小於零
                        {

                            _LastMoveTime = Time.time;
                            _WantToMove = true;

                            Vector3 _MVRAND = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                            _TargetPos += _MVRAND;
                        }

                    }

                    

                    // 以免BUG 因此重置位置
                    _ResetTime -= Time.deltaTime;
                    if (_ResetTime < 0f)
                    {
                        _WantToMove = true;
                        _TargetPos = _OriginalPos;
                    }
                    
                    if (_WantToMove)
                    {
                        _Obstacle.enabled = false;

                        if (Time.time > _LastMoveTime + _NetxFrameTime) // 現在時間 大於 如果想移動的時間 加 下幾幀時間
                        {
                            _Agent.enabled = true;
                            _Agent.SetDestination(_TargetPos);
                            _WantToMove = false;

                            _StandbyTime = 3f;
                            _ResetTime = 30f;
                        }

                    }

                    break;

                case "EnemyFound": // 發現敵人
                    
                    AttackAction();

                    break;

                case "UnderAttack": // 遭受攻擊
                    
                    _TargetPos = _Player.transform.position;

                    break;

                case "FacUnderAttack": // 兵工廠遭受攻擊
                    
                    _Agent.stoppingDistance = 70f;
                    _TargetPos = _Player.transform.position;

                    AttackAction();
                    


                    break;

            }
        }

        _LastUpdatePos = transform.position; // 紀錄上一幀位置
        
        if(_State != "Patrol")
        {
            _Agent.SetDestination(_TargetPos);
        }
        
    }



    private void AttackAction()
    {
        // 距離多近 開火
        _Distance = Vector3.Distance(_Player.transform.position, transform.position);

        if (_Distance <= _Agent.stoppingDistance)
        {
            EnemyTurretRotationScript.LookPlayer();

            RaycastHit hit2;

            if (Physics.Raycast(Muzzle.transform.position , Muzzle.transform.forward, out hit2, 80f, 1 << 3 | 1 << 7 | 1 << 10  | 1 << 13 ))
            {
                
                Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward , Color.black);

                if (hit2.collider != null && hit2.collider.CompareTag("Player"))
                {
                    // Debug.Log("Shoot");
                    _EnemyShootScript.Shooting(BulletEnegy, hit2.point);
                }
            }

            _EnemyShootScript.Reloading(BulletEnegy);

        }

        if (hit.collider != null && !hit.collider.CompareTag("Player") && _State != "FacUnderAttack")
        {
            EnemyTurretRotationScript.PatrolStat();
            _Agent.stoppingDistance = 0f;

            if (_LastUpdatePos == transform.position)
            {
                _StandbyTime -= Time.deltaTime;

                if (_StandbyTime < 0f)
                {
                    _StandbyTime = 3f;
                    _State = "Patrol";
                }
            }
            else if (Vector3.Distance(_TargetPos, _LastUpdatePos) != 0)
            {
                _ResetTime -= Time.deltaTime;
                if (_ResetTime < 0f)
                {
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
