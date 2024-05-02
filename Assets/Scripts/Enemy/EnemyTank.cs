using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyTank : MonoBehaviour
{
    [HideInInspector] public float Hp;
    [HideInInspector] public float BulletEnegy;
    public EnemyTurretRotation EnemyTurretRotationScript;
    public GameObject Muzzle;

    EnemyFactory _EnemyFactoryScript;
    EnemyShoot _EnemyShootScript;
    GameObject _Player;
    NavMeshAgent _Agent;
    //NavMeshObstacle _Obstacle;

    [SerializeField] string _State;

    
    [SerializeField] float _Distance;
    [SerializeField] Vector3 _TargetPos; // �s��e�����ؼЦ�m
    [SerializeField] Vector3 _LastUpdatePos; // �W�@�V��m
    [SerializeField] Vector3 _OriginalPos; // ��l��m
    [SerializeField] float _StandbyTime; // �ݩR�ɶ�
    [SerializeField] float _ResetTime; // ���]�ɶ�

    RaycastHit hit;
    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        _Agent = GetComponent<NavMeshAgent>();
        //_Obstacle = GetComponent<NavMeshObstacle>();
        _EnemyShootScript = GetComponent<EnemyShoot>();
        _EnemyFactoryScript = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();

        _State = "Patrol";
        _TargetPos = transform.position;
        _OriginalPos = transform.position;
        _StandbyTime = 2f;
        _ResetTime = 8f;


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
                            // �p�G�I����|�Oplayer��m�A�S�I����h�Oplayer�̫��������m
                            _TargetPos = _Player.transform.position;
                            _State = "EnemyFound";
                        }
                        
                        
                    }
                }
            }

            switch (_State)
            {
                case "Patrol": // ����
                    Debug.Log("Patrol");
                    EnemyTurretRotationScript.PatrolStat();
                    _Agent.stoppingDistance = 0f;
                    
                    if (transform.position == _LastUpdatePos)
                    {
                        /*
                        _Agent.enabled = false;
                        _Obstacle.enabled = true;
                        */
                        _StandbyTime -= Time.deltaTime;

                        if (_StandbyTime < 0f)
                        {
                            //_Obstacle.enabled = false;
                            
                            _StandbyTime = 3f;
                            _ResetTime = 10f;
                            Vector3 _MVRAND = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

                            _TargetPos += _MVRAND;
                        }
                    }
                    else if (Vector3.Distance(_TargetPos , _LastUpdatePos) != 0) // �^���l��m
                    {
                        _ResetTime -= Time.deltaTime;
                        if (_ResetTime < 0f)
                        {
                            _TargetPos = _OriginalPos;
                        }
                    }

                    break;

                case "EnemyFound": // �o�{�ĤH
                    Debug.Log("EnemyFound");
                    AttackAction();

                    break;

                case "UnderAttack": // �D������
                    Debug.Log("UnderAttack");
                    _TargetPos = _Player.transform.position;

                    break;

                case "FacUnderAttack": // �L�u�t�D������
                    Debug.Log("FacUnderAttack");
                    _Agent.stoppingDistance = 70f;
                    _TargetPos = _Player.transform.position;

                    AttackAction();
                    


                    break;

            }
        }

        _LastUpdatePos = transform.position; // �����W�@�V��m
        /*
        if (!_Obstacle.enabled)
        {
            _Agent.enabled = true;
            _Agent.SetDestination(_TargetPos);
        }
        */
        _Agent.SetDestination(_TargetPos);
    }

    

    private void AttackAction()
    {
        // �Z���h�� �}��
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
