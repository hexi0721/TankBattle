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

    [SerializeField] Vector3 _TargetPos; // �s��e�����ؼЦ�m
    [SerializeField] Vector3 _LastUpdatePos; // �W�@�V��m
    Vector3 _OriginalPos; // ��l��m
    [SerializeField] float _StandbyTime; // �ݩR�ɶ�
    [SerializeField] float _ResetTime; // ���]�ɶ�
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
                case "Patrol": // ����


                    EnemyTurretRotationScript.PatrolStat();
                    _Agent.stoppingDistance = 0f;

                    // �H�KBUG �]�����m��m
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
                        // ���A�u��
                        if (transform.position == _LastUpdatePos && _WantToMove != true) // ����ɨ�
                        {
                            
                            _Agent.enabled = false;
                            _Obstacle.enabled = true;

                            _StandbyTime -= Time.deltaTime;

                            if (_StandbyTime < 0f) // ���d�ɶ��p��s
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

                            if (Time.time > _LastMoveTime + _NetxFrameTime) // �{�b�ɶ� �j�� �p�G�Q���ʪ��ɶ� �[ �U�X�V�ɶ�
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

                case "EnemyFound": // �o�{�ĤH


                    EnemyTurretRotationScript.LookPlayer();
                    _Agent.stoppingDistance = 70f;

                    AttackAction();

                    if (transform.position == _LastUpdatePos ) // ����ɨ�
                    {

                        _Agent.enabled = false;
                        _Obstacle.enabled = true;

                    }

                    if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 70f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
                    {
                        
                        if (hit.collider != null) // �S�����player
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
                    else if (hit.collider == null) // �S�����player
                    {
                        _TargetPos = _Player.transform.position;
                        _LastMoveTime = Time.time;
                        _WantToMove = true;
                        _State = "Patrol";
                        break;
                    }

                    break;

                case "UnderAttack": // �D������

                    _TargetPos = _Player.transform.position;

                    if (_Agent.enabled) // ��Q������ obstacle �\���������A
                    {
                        _Agent.SetDestination(_TargetPos);
                    }
                    else // ��Q������ obstacle �\��}�Ҫ��A
                    {

                        _Obstacle.enabled = false;

                        if (Time.time > _LastMoveTime + _NetxFrameTime) // �{�b�ɶ� �j�� �p�G�Q���ʪ��ɶ� �[ �U�X�V�ɶ�
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

                case "FacUnderAttack": // �L�u�t�D������

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

                    if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 70f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))  // ��Mplayer
                    {
                        
                        if (hit.collider != null) 
                        {
                            Debug.DrawRay(transform.position, (_Player.transform.position - transform.position), Color.red);
                            if (hit.collider.transform.CompareTag("Player")) // ���player
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
                            else // �����player
                            {
                                EnemyTurretRotationScript.PatrolStat();
                                _WantToMove = true;
                                
                            }
                            
                            
                        }

                    }
                    else if (hit.collider == null ) // �����player
                    {
                        Debug.DrawRay(transform.position, (_Player.transform.position - transform.position), Color.white);

                        EnemyTurretRotationScript.PatrolStat();
                        _WantToMove = true;
                        
                    }

                    

                    break;

            }
        }

        _LastUpdatePos = transform.position; // �����W�@�V��m
        
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
                Instantiate(BigExplode, transform.position, Quaternion.identity); // �z��

                // ��ʸ}��DISABLE
                EnemyTurretRotationScript.enabled = false;
                DisableEnemyTankActionScript.enabled = false;
                _EnemyShootScript.enabled = false;
                Destroy(rb);
                Destroy(transform.GetChild(1).gameObject); // �Nturret�R��

                transform.GetChild(0).GetComponent<Renderer>().material = DestroyMaterial; // TankBody
                
                _Agent.enabled = false;
                _Obstacle.enabled = false;

                this.enabled = false;

            }
        }


    }


}
