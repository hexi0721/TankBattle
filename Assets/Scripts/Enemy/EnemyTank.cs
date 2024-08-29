using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.Controls.AxisControl;
using static UnityEngine.UI.Image;

public class EnemyTank : MonoBehaviour
{
    public float Hp;
    [HideInInspector] public float BulletEnegy;
    
    public GameObject EnemyBullet; // �l�u����
    float _ReloadTime;

    DisableEnemyTankAction DisableEnemyTankActionScript;
    EnemyFactory _EnemyFactoryScript;

    public GameObject Body , Turret ,  Muzzle;
    public GameObject BigExplode;
    public Material DestroyMaterial;

    [SerializeField] GameObject _Player;

    public GameObject Player
    {
        get => _Player;
    }

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


    private void Start()
    {
        _Player = GameObject.Find("PlayerTank").transform.GetChild(1).gameObject;
        _Agent = GetComponent<NavMeshAgent>();
        _Obstacle = GetComponent<NavMeshObstacle>();
        _Obstacle.enabled = false;
        
        
        DisableEnemyTankActionScript = GetComponent<DisableEnemyTankAction>();
        _EnemyFactoryScript = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();
        rb = GetComponent<Rigidbody>();

        _State = "Patrol";
        _TargetPos = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)); ;
        _OriginalPos = transform.position;
        
        _StandbyTime = 2f;
        _ReloadTime = 5f;
        _ResetTime = 30f;
        
        _WantToMove = true;
        _NetxFrameTime = 0.5f;

        
    }

    private void Update()
    {
        FacUnderAttack();

    }

    private void FacUnderAttack()
    {
        if (_EnemyFactoryScript.Hp < _EnemyFactoryScript.MaxHp)
        {

            if (_Player != null)
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


                    PatrolStat();
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
                        
                        if (transform.position.x >= _TargetPos.x - 1 && transform.position.x <= _TargetPos.x+1 && transform.position.z >= _TargetPos.z - 1 && transform.position.z <= _TargetPos.z + 1 && _WantToMove == false) // ����ɨ�
                        {
                            
                            _Agent.enabled = false;
                            _Obstacle.enabled = true;

                            _StandbyTime -= Time.deltaTime;

                            if (_StandbyTime < 0f) // ���d�ɶ��p��s
                            {

                                _LastMoveTime = Time.time;
                                _WantToMove = true;
                                _TargetPos += new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                                _ResetTime = 15f;
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

                    

                    if (Physics.Raycast(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, out hit, 100f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
                    {
                        
                        if (hit.collider != null)
                        {
                            
                            if (hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.red);
                                _TargetPos = _Player.transform.position;
                                _LastMoveTime = Time.time;
                                _State = "EnemyFound";

                                break;
                            }
                            else if (!hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.white);
                            }


                        }
                    }
                    else if (hit.collider == null)
                    {
                        Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.white);
                    }

                    break;

                case "EnemyFound": // �o�{�ĤH

                    _Agent.stoppingDistance = 70f;
                    LookPlayer();
                    AttackAction();

                    if (transform.position == _LastUpdatePos ) // ����ɨ�
                    {
                        _Agent.enabled = false;
                        _Obstacle.enabled = true;
                    }

                    if (Physics.Raycast(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, out hit, 100f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
                    {
                        
                        if (hit.collider != null) // �S�����player
                        {
                            
                            if (!hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.white);
                                _TargetPos = _Player.transform.position;
                                _LastMoveTime = Time.time;
                                _WantToMove = true;
                                _State = "Patrol";

                                break;
                            }
                            else if (hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.red);
                            }
                        }
                        
                    }
                    else if (hit.collider == null) // �S�����player
                    {
                        Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.white);
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
                    

                    if (Physics.Raycast(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, out hit, 100f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
                    {
                        if (hit.collider != null)
                        {
                            
                            if (hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.red);
                                _TargetPos = _Player.transform.position;
                                _LastMoveTime = Time.time;
                                
                                _State = "EnemyFound";

                                break;
                            }
                            else if (!hit.collider.transform.CompareTag("Player"))
                            {
                                Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.white);
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

                    if (Physics.Raycast(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, out hit, 100f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))  // ��Mplayer
                    {
                        
                        if (hit.collider != null) 
                        {
                            
                            if (hit.collider.transform.CompareTag("Player")) // ���player
                            {
                                Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.red);
                                LookPlayer();
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
                                Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.white);
                                PatrolStat();
                                _WantToMove = true;
                                
                            }
                            
                            
                        }

                    }
                    else if (hit.collider == null ) // �����player
                    {
                        Debug.DrawRay(Muzzle.transform.position, _Player.transform.position - Muzzle.transform.position, Color.white);

                        PatrolStat();
                        _WantToMove = true;
                        
                    }

                    

                    break;

            }
        }

        _LastUpdatePos = transform.position; // �����W�@�V��m
        
    }


    
    private void AttackAction()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out hit, 120f, 1 << 3 | 1 << 7 | 1 << 10 | 1 << 13))
        {

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Shooting(hit.point);
            }
        }

        Reloading();



    }

    void LookPlayer()
    {
        float _clampMin = -8f;
        float _clampMax = 3f;
        float _clamp;

        Vector3 v = Player.transform.position - Turret.transform.position;
        Quaternion rotation = Quaternion.LookRotation(v);

        // Turret ���k
        Turret.transform.rotation = Quaternion.Lerp(Turret.transform.rotation, Quaternion.Euler(Turret.transform.rotation.eulerAngles.x, rotation.eulerAngles.y, Turret.transform.rotation.eulerAngles.z), Time.deltaTime);

        // Muzzle �W�U
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

    void Shooting(Vector3 TargetPoint)
    {
        if (BulletEnegy >= _ReloadTime)
        {

            // �R���ǽT�׭ץ�  x : 3 ~ -3  | y : 2 ~ -2 

            float x = Random.Range(-3f, 4f);
            float y = Random.Range(-2f, 3f);
            TargetPoint = new Vector3(TargetPoint.x + x , TargetPoint.y + y , TargetPoint.z);

            GameObject go = Instantiate(EnemyBullet,
                Muzzle.transform.position + Muzzle.transform.forward * 10,
                Quaternion.Euler(Muzzle.transform.eulerAngles.x, Muzzle.transform.eulerAngles.y, Muzzle.transform.eulerAngles.z)) as GameObject;
            go.transform.LookAt(TargetPoint);

            BulletEnegy = 0; // �˶�

        }
    }

    void Reloading()
    {

        if (BulletEnegy < _ReloadTime)
        {
            BulletEnegy += Time.deltaTime;
        }

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
                
                DisableEnemyTankActionScript.enabled = false;
                
                Destroy(rb);
                Destroy(Turret); // �Nturret�R��

                Body.GetComponent<Renderer>().material = DestroyMaterial; // TankBody
                
                _Agent.enabled = false;
                _Obstacle.enabled = false;

                enabled = false;

            }
        }


    }


}
