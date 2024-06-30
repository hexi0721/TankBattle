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
    public DisableEnemyTankAction DisableEnemyTankActionScript;
    
    public GameObject Muzzle;

    public GameObject BigExplode;
    public Material DestroyMaterial;

    EnemyFactory _EnemyFactoryScript;
    EnemyShoot _EnemyShootScript;
    GameObject _Player;
    NavMeshAgent _Agent;
    NavMeshObstacle _Obstacle;
    Rigidbody rb;

    [SerializeField] string _State;

    [SerializeField] Vector3 _TargetPos; // 存放前往的目標位置
    [SerializeField] Vector3 _LastUpdatePos; // 上一幀位置
    Vector3 _OriginalPos; // 初始位置
    [SerializeField] float _StandbyTime; // 待命時間
    [SerializeField] float _ResetTime; // 重設時間
    float _LastMoveTime;
    float _NetxFrameTime;
    
    [SerializeField] bool _WantToMove;
    
    public bool WantToMove
    {
        set => _WantToMove = value;
        get => _WantToMove;
    }
    
    //RaycastHit hit;
    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        _Agent = GetComponent<NavMeshAgent>();
        _Obstacle = GetComponent<NavMeshObstacle>();
        _Obstacle.enabled = false;
        _EnemyShootScript = GetComponent<EnemyShoot>();
        _EnemyFactoryScript = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();
        rb = GetComponent<Rigidbody>();

        _State = "Patrol";
        _TargetPos = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)); ;
        _OriginalPos = transform.position;
        
        _StandbyTime = 2f;
        _ResetTime = 30f;
        _WantToMove = true;
        _NetxFrameTime = 0.2f;
        

    }

    private void Update()
    {
        

        if (_EnemyFactoryScript.Hp < _EnemyFactoryScript.GetMaxHp())
        {
            
            if( _Player != null )
            {
                _TargetPos = _Player.transform.position;
            }
            _State = "FacUnderAttack";
        }

    }
    private void LateUpdate()
    {

        if (_Player != null)
        {
            
            RaycastHit hit;
            switch (_State)
            {
                case "Patrol": // 巡邏


                    EnemyTurretRotationScript.PatrolStat();
                    _Agent.stoppingDistance = 0f;

                    // 以免BUG 因此重置位置
                    _ResetTime -= Time.deltaTime;
                    if (_ResetTime < 0f)
                    {
                        _LastMoveTime = Time.time;
                        _WantToMove = true;
                        _TargetPos = _OriginalPos;
                        _ResetTime = 30f;
                    }

                    if(DisableEnemyTankActionScript.ExitMenu)
                    {
                        if(_Agent.enabled)
                        {
                            _Agent.SetDestination(_TargetPos);
                            _WantToMove = true;
                        }
                        
                        DisableEnemyTankActionScript.ExitMenu = false;
                    }
                    else
                    {
                        // 日後再優化
                        if (transform.position == _LastUpdatePos && _WantToMove != true) // 停止時刻
                        {
                            
                            _Agent.enabled = false;
                            _Obstacle.enabled = true;

                            _StandbyTime -= Time.deltaTime;

                            if (_StandbyTime < 0f) // 當停留時間小於零
                            {

                                _LastMoveTime = Time.time;
                                _WantToMove = true;

                                
                                _TargetPos += new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

                                _ResetTime = 30f;
                            }

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
                                

                            }

                        }
                    }

                    

                    if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 70f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
                    {
                        
                        if (hit.collider != null)
                        {
                            Debug.DrawRay(transform.position, (_Player.transform.position - transform.position), Color.red);
                            if (hit.collider.transform.CompareTag("Player"))
                            {

                                _TargetPos = _Player.transform.position;
                                _LastMoveTime = Time.time;
                                _State = "EnemyFound";

                                break;
                            }


                        }
                    }
                    else if (hit.collider == null)
                    {
                        Debug.DrawRay(transform.position, (_Player.transform.position - transform.position), Color.white);
                    }

                    break;

                case "EnemyFound": // 發現敵人


                    EnemyTurretRotationScript.LookPlayer();
                    _Agent.stoppingDistance = 70f;

                    AttackAction();

                    if (transform.position == _LastUpdatePos ) // 停止時刻
                    {

                        _Agent.enabled = false;
                        _Obstacle.enabled = true;

                    }

                    if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 70f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
                    {
                        
                        if (hit.collider != null) // 沒有找到player
                        {
                            Debug.DrawRay(transform.position, (_Player.transform.position - transform.position), Color.red);
                            if (!hit.collider.transform.CompareTag("Player"))
                            {
                                _TargetPos = _Player.transform.position;
                                _LastMoveTime = Time.time;
                                _WantToMove = true;
                                _State = "Patrol";

                                break;
                            }
                        }
                        
                    }
                    else if (hit.collider == null) // 沒有找到player
                    {
                        _TargetPos = _Player.transform.position;
                        _LastMoveTime = Time.time;
                        _WantToMove = true;
                        _State = "Patrol";
                        break;
                    }

                    break;

                case "UnderAttack": // 遭受攻擊

                    _TargetPos = _Player.transform.position;

                    if (_Agent.enabled) // 當被攻擊時 obstacle 功能關閉狀態
                    {
                        _Agent.SetDestination(_TargetPos);
                    }
                    else // 當被攻擊時 obstacle 功能開啟狀態
                    {

                        _Obstacle.enabled = false;

                        if (Time.time > _LastMoveTime + _NetxFrameTime) // 現在時間 大於 如果想移動的時間 加 下幾幀時間
                        {
                            _Agent.enabled = true;
                            _Agent.SetDestination(_TargetPos);

                        }

                    }
                    

                    if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 70f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
                    {
                        if (hit.collider != null)
                        {
                            Debug.DrawRay(transform.position, (_Player.transform.position - transform.position), Color.red);
                            if (hit.collider.transform.CompareTag("Player"))
                            {

                                _TargetPos = _Player.transform.position;
                                _LastMoveTime = Time.time;
                                
                                _State = "EnemyFound";

                                break;
                            }

                        }
                    }
                    

                    break;

                case "FacUnderAttack": // 兵工廠遭受攻擊

                    _Agent.stoppingDistance = 70f;

                    if (_WantToMove)
                    { 

                        _Obstacle.enabled = false;

                        if (Time.time > _LastMoveTime + _NetxFrameTime) 
                        {
                            _Agent.enabled = true;
                            _Agent.SetDestination(_TargetPos);
                            
                        }

                    }
                    else
                    {
                        _LastMoveTime = Time.time;
                    }

                    if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 70f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))  // 找尋player
                    {
                        
                        if (hit.collider != null) 
                        {
                            Debug.DrawRay(transform.position, (_Player.transform.position - transform.position), Color.red);
                            if (hit.collider.transform.CompareTag("Player")) // 找到player
                            {

                                EnemyTurretRotationScript.LookPlayer();
                                AttackAction();

                                if (transform.position == _LastUpdatePos )
                                {

                                    _Agent.enabled = false;
                                    _Obstacle.enabled = true;
                                    _WantToMove = false;
                                }   

                            }
                            else // 未找到player
                            {
                                EnemyTurretRotationScript.PatrolStat();
                                _WantToMove = true;
                                
                            }
                            
                            
                        }

                    }
                    else if (hit.collider == null ) // 未找到player
                    {
                        Debug.DrawRay(transform.position, (_Player.transform.position - transform.position), Color.white);

                        EnemyTurretRotationScript.PatrolStat();
                        _WantToMove = true;
                        
                    }

                    

                    break;

            }
        }

        _LastUpdatePos = transform.position; // 紀錄上一幀位置
        
    }


    
    private void AttackAction()
    {
        
        RaycastHit hit2;
        if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out hit2, 80f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
        {

            Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward, Color.black);

            if (hit2.collider != null && hit2.collider.CompareTag("Player"))
            {
                _EnemyShootScript.Shooting(BulletEnegy, hit2.point);
            }
        }

        _EnemyShootScript.Reloading(BulletEnegy);



    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            _LastMoveTime = Time.time;
            _WantToMove = true;
            _State = "UnderAttack";
            
            Hp -= Random.Range(1, 4);

            if (Hp <= 0)
            {
                
                GetComponent<WhenEnemyTankDestroy>().IsDestroy = true;
                Instantiate(BigExplode, transform.position, Quaternion.identity); // 爆炸

                // 行動腳本DISABLE
                EnemyTurretRotationScript.enabled = false;
                DisableEnemyTankActionScript.enabled = false;
                _EnemyShootScript.enabled = false;
                Destroy(rb);
                Destroy(transform.GetChild(1).gameObject); // 將turret摧毀

                transform.GetChild(0).GetComponent<Renderer>().material = DestroyMaterial; // TankBody
                
                _Agent.enabled = false;
                _Obstacle.enabled = false;

                this.enabled = false;

            }
        }


    }


}
