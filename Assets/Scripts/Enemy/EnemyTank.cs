using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.Controls.AxisControl;
using static UnityEngine.UI.Image;

public class EnemyTank : MonoBehaviour
{
    public float Hp;
    [HideInInspector] public float BulletEnegy;
    
    public DisableEnemyTankAction DisableEnemyTankActionScript;
    
    public GameObject Body , Turret ,  Muzzle;
    

    public GameObject BigExplode;
    public Material DestroyMaterial;

    EnemyFactory _EnemyFactoryScript;
    EnemyShoot _EnemyShootScript;
    [SerializeField] GameObject _Player;

    public GameObject Player
    {
        get => _Player;
    }

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

    public float offset = 0.55f;
    public Terrain terrain ;

    
    private void Start()
    {
        _Player = GameObject.Find("PlayerTank").transform.GetChild(1).gameObject;
        _Agent = GetComponent<NavMeshAgent>();
        _Obstacle = GetComponent<NavMeshObstacle>();
        _Obstacle.enabled = false;
        _EnemyShootScript = GetComponent<EnemyShoot>();
        
        DisableEnemyTankActionScript = GetComponent<DisableEnemyTankAction>();
        _EnemyFactoryScript = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();
        rb = GetComponent<Rigidbody>();

        _State = "Patrol";
        _TargetPos = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)); ;
        _OriginalPos = transform.position;
        
        _StandbyTime = 2f;
        _ResetTime = 30f;
        _WantToMove = true;
        _NetxFrameTime = 0.5f;

        //terrain = GameObject.Find("MainTerrain").GetComponent<Terrain>();
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
        /*
        RaycastHit hit;
        if (Physics.Raycast(transform.position  , Vector3.down, out hit, 1 << 13))
        {
            //Debug.DrawLine(transform.position  , hit.point, Color.black);
            
            //Get slope angle from the raycast hit normal then calcuate new pos of the object
            Quaternion newRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime - 2f);

            terrain = GameObject.Find(hit.transform.name).GetComponent<Terrain>();

            transform.position = new Vector3(transform.position.x, terrain.SampleHeight(transform.position) + offset, transform.position.z);
        }
        */

    }
    private void LateUpdate()
    {

        if (_Player != null)
        {
            
            RaycastHit hit;
            switch (_State)
            {
                case "Patrol": // 巡邏


                    PatrolStat();
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
                        
                        if (transform.position.x == _TargetPos.x && transform.position.z == _TargetPos.z && _WantToMove == false) // 停止時刻
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
                            
                            if (hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.red);
                                _TargetPos = _Player.transform.position;
                                _LastMoveTime = Time.time;
                                _State = "EnemyFound";

                                break;
                            }
                            else if (!hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.white);
                            }


                        }
                    }
                    else if (hit.collider == null)
                    {
                        Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.white);
                    }

                    break;

                case "EnemyFound": // 發現敵人


                    LookPlayer();
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
                            
                            if (!hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.white);
                                _TargetPos = _Player.transform.position;
                                _LastMoveTime = Time.time;
                                _WantToMove = true;
                                _State = "Patrol";

                                break;
                            }
                            else if (hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.red);
                            }
                        }
                        
                    }
                    else if (hit.collider == null) // 沒有找到player
                    {
                        Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.white);
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
                            
                            if (hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.red);
                                _TargetPos = _Player.transform.position;
                                _LastMoveTime = Time.time;
                                
                                _State = "EnemyFound";

                                break;
                            }
                            else if (!hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.white);
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
                            
                            if (hit.collider.transform.CompareTag("Player")) // 找到player
                            {
                                Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.red);
                                LookPlayer();
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
                                Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.white);
                                PatrolStat();
                                _WantToMove = true;
                                
                            }
                            
                            
                        }

                    }
                    else if (hit.collider == null ) // 未找到player
                    {
                        Debug.DrawRay(transform.position, _Player.transform.position - transform.position, Color.white);

                        PatrolStat();
                        _WantToMove = true;
                        
                    }

                    

                    break;

            }
        }

        _LastUpdatePos = transform.position; // 紀錄上一幀位置
        
    }


    
    private void AttackAction()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out hit, 80f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
        {

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                _EnemyShootScript.Shooting(BulletEnegy, hit.point);
            }
        }

        _EnemyShootScript.Reloading(BulletEnegy);



    }

    void LookPlayer()
    {
        float _clampMin = -8f;
        float _clampMax = 3f;
        float _clamp;

        Vector3 v = Player.transform.position - Turret.transform.position;
        Quaternion rotation = Quaternion.LookRotation(v);

        // Turret 左右
        Turret.transform.rotation = Quaternion.Lerp(Turret.transform.rotation, Quaternion.Euler(Turret.transform.rotation.eulerAngles.x, rotation.eulerAngles.y, Turret.transform.rotation.eulerAngles.z), Time.deltaTime);

        // Muzzle 上下
        if (rotation.eulerAngles.x > 3.0f && rotation.eulerAngles.x < 180f)
        {
            _clamp = _clampMax;
        }
        else if (rotation.eulerAngles.x > 180f && rotation.eulerAngles.x < 352f)
        {
            _clamp = _clampMin;
        }
        else
        {
            _clamp = rotation.eulerAngles.x;
        }
        Muzzle.transform.rotation = Quaternion.Lerp(Muzzle.transform.rotation,
            Quaternion.Euler(_clamp, Muzzle.transform.eulerAngles.y, Muzzle.transform.eulerAngles.z), 2 * Time.deltaTime);
    }
    void PatrolStat()
    {
        Turret.transform.forward = Vector3.Lerp(Turret.transform.forward, Body.transform.forward, Time.deltaTime);
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
                
                DisableEnemyTankActionScript.enabled = false;
                _EnemyShootScript.enabled = false;
                Destroy(rb);
                Destroy(transform.GetChild(1).gameObject); // 將turret摧毀

                transform.GetChild(0).GetComponent<Renderer>().material = DestroyMaterial; // TankBody
                
                _Agent.enabled = false;
                _Obstacle.enabled = false;

                enabled = false;

            }
        }


    }


}
