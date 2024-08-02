using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetting : MonoBehaviour
{
    private static PlayerSetting _instance;

    public static PlayerSetting Instance 
    {
        get => _instance;

    }

    public float Hp; // ���a�ͩR ����� ��GameController�P�_0�ɵ����C��
    public float Hp2; // ������
    public float BulletEnegy;
    bool _UnderAttack;
    float Counter; // �p���������ɰʧ@
    [SerializeField] float _FeedBackTime;

    public Image HpImage;
    public Image HpImage2;

    public Image BulletEnegyImage;
    Image ShootFeedBack;

    public bool _Ishit;

    public Animator animator; // ���թZ�J�\��i�Hdisable

    public GameObject turret, muzzle, shell, track_1, track_2, wheels;

    private void Start()
    {
        _UnderAttack = false;
        Counter = 1.5f;

        _FeedBackTime = 0f;
        ShootFeedBack = GameObject.FindWithTag("ShootFeedBack").GetComponent<Image>();
        ShootFeedBack.color = new Color(ShootFeedBack.color.r, ShootFeedBack.color.g, ShootFeedBack.color.b, 0f);
        _Ishit = false;

        animator = GetComponent<Animator>();

        
        // ���թZ�J�\��i�Hdisable
        //animator.enabled = true;
        //transform.position = new Vector3(126.49f, 40.597f , 319.73f);
        //
        
    }

    private void Awake()
    {
        _instance = this;
    }
    

    void Update()
    {
        Link();
        

        BulletEnegyImage.fillAmount = BulletEnegy / 2.5f;

        HpImage.fillAmount = Hp / 100;
        if (Hp <= 0)
        {
            Destroy(this.gameObject);
        }

        if(_UnderAttack)
        {

            if (Hp2 == Hp)
            {
                _UnderAttack = false;
            }

            Counter -= Time.deltaTime;

            if (Counter <= 0 && Hp2 > Hp)
            {
                Hp2 -= 1;
                HpImage2.fillAmount = Hp2 / 100;
            }
            
        }

        if (_Ishit)
        {
            _FeedBackTime = 0.1f;
            ShootFeedBack.color = new Color(ShootFeedBack.color.r, ShootFeedBack.color.g, ShootFeedBack.color.b, 1f);
            _Ishit = false;

            
            
        }
        else
        {

            if (_FeedBackTime <= 0f)
            {

                ShootFeedBack.color = new Color(ShootFeedBack.color.r, ShootFeedBack.color.g, ShootFeedBack.color.b, 0f);
            }
            else
            {
                _FeedBackTime -= Time.deltaTime;
            }

        }

        


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {

            Hp -= Random.Range(5, 10);
            _UnderAttack = true;
            Counter = 1.5f;

        }
    }


    // �s�W�ɦ�\��

    void Link()
    {
        Vector3 _currentVelocity = Vector3.zero;

        // �Hshell ������I
        // turret
        turret.transform.position = Vector3.SmoothDamp(turret.transform.position , shell.transform.position + shell.transform.up * 1.1f ,ref _currentVelocity, 0.01f);
        turret.transform.eulerAngles = new Vector3(shell.transform.eulerAngles.x, turret.transform.eulerAngles.y, shell.transform.eulerAngles.z);

        // muzzle
        //muzzle.transform.position = turret.transform.position + new Vector3(turret.transform.forward.x, turret.transform.forward.y, turret.transform.forward.z * 1.2f);
        //muzzle.transform.eulerAngles = new Vector3(muzzle.transform.eulerAngles.x , turret.transform.eulerAngles.y , turret.transform.eulerAngles.z);

        // track & wheels
        track_1.transform.position = Vector3.SmoothDamp(track_1.transform.position, shell.transform.position, ref _currentVelocity, 0.01f);
        track_2.transform.position = Vector3.SmoothDamp(track_2.transform.position, shell.transform.position, ref _currentVelocity, 0.01f);
        wheels.transform.position = Vector3.SmoothDamp(wheels.transform.position, shell.transform.position, ref _currentVelocity, 0.01f);
        track_1.transform.eulerAngles = shell.transform.eulerAngles;
        track_2.transform.eulerAngles = shell.transform.eulerAngles;
        wheels.transform.eulerAngles = shell.transform.eulerAngles;


    }
}
