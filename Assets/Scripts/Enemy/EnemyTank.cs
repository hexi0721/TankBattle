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

    EnemyShoot _EnemyShootScript;
    GameObject _Player;
    NavMeshAgent _Agent;

    [SerializeField] string _State;

    [SerializeField] Vector3 _TargetPos; // �s��e�����ؼЦ�m
    [SerializeField] Vector3 _LastUpdatePos; // �W�@�V��m
    [SerializeField] bool _EnemyFound; // �O�_��쪱�a
    [SerializeField] float _StandbyTime; // �ݩR�ɶ�


    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
        _Agent = GetComponent<NavMeshAgent>();
        _EnemyShootScript = GetComponent<EnemyShoot>();

        _State = "Patrol";
        _TargetPos = transform.position;
        _EnemyFound = false;
        _StandbyTime = 2f;
        
    }

    private void LateUpdate()
    {
        


        RaycastHit hit;

        if(_Player != null)
        {

            if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 100f, ~(1 << 8 | 1 << 9 | 1 << 11 | 1 << 12)))
            {
                if (hit.collider.CompareTag("Player"))
                {

                    _Agent.stoppingDistance = 70f;
                    // �p�G�I����|�Oplayer��m�A�S�I����h�Oplayer�̫��������m
                    _TargetPos = _Player.transform.position;
                    _State = "EnemyFound";
                }
            }
            else
            {
                _State = "Patrol"; // ����I���������Z������ �X�{BUG
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

                    break;

                case "EnemyFound":

                    // �Z���h�� �}��
                    float _Distance = Vector3.Distance(_Player.transform.position, transform.position);

                    if (_Distance <= _Agent.stoppingDistance)
                    {
                        EnemyTurretRotationScript.LookPlayer();


                        RaycastHit hit2;

                        if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out hit2, 70f, ~(1 << 8 | 1 << 9 | 1 << 11 | 1 << 12)))
                        {

                            Debug.Log(hit2.collider.name);
                            Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward * 100, Color.blue);

                            if (hit2.collider.CompareTag("Player"))
                            {
                                _EnemyShootScript.Shooting(BulletEnegy, hit2.point);
                            }

                        }

                        _EnemyShootScript.Reloading(BulletEnegy);

                    }

                    if (!hit.collider.CompareTag("Player"))
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

                    

                    break;

                case "UnderAttack":

                    _TargetPos = _Player.transform.position;
                    

                    break;

                

                
            }
            /*
            if (Physics.Raycast(transform.position, _Player.transform.position - transform.position, out hit, 100f, ~(1 << 8 | 1 << 9 | 1 << 11 | 1 << 12)))
            {

                //if(hit.collider != null) { Debug.Log(hit.collider.name); }

                if (hit.collider.CompareTag("Player"))
                {
                    
                    _Agent.stoppingDistance = 70f;
                    // �p�G�I����|�Oplayer��m�A�S�I����h�Oplayer�̫��������m
                    _TargetPos = _Player.transform.position;
                    _EnemyFound = true;



                    // �Z���h�� �}��
                    float _Distance = Vector3.Distance(_Player.transform.position, transform.position);

                    if (_Distance <= _Agent.stoppingDistance)
                    {
                        EnemyTurretRotationScript.LookPlayer();


                        RaycastHit hit2;

                        if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out hit2, 70f, ~(1 << 8 | 1 << 9 | 1 << 11 | 1 << 12)))
                        {
                            
                            Debug.Log(hit2.collider.name);
                            Debug.DrawRay(Muzzle.transform.position, Muzzle.transform.forward * 100, Color.blue) ;
                            
                            if (hit2.collider.CompareTag("Player"))
                            {
                                _EnemyShootScript.Shooting(BulletEnegy , hit2.point);
                            }

                        }

                        _EnemyShootScript.Reloading(BulletEnegy);




                    }

                    
                }
                else if (_EnemyFound == true)  // ��F���a�����a�I �p�G�S�o�{���a standby������ �i�J���ު��A
                {
                    EnemyTurretRotationScript.PatrolStat();
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
                _EnemyFound = false; // ����I���������Z������ �X�{BUG
            }
            */
            /*
            if (!_EnemyFound) // ����
            {
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

            }
            */


        }

        _LastUpdatePos = transform.position; // �����W�@�V��m

        
        _Agent.SetDestination(_TargetPos);
        


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
