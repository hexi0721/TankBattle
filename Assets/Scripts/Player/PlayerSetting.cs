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

    private float maxHp;
    public float Hp; // ���a�ͩR ����� ��GameController�P�_0�ɵ����C��
    public float Hp2; // ������
    public float BulletEnegy;
    bool _underAttack; // �Q����
    float CounterTime; // �p���������ɰʧ@
    float _FeedBackTime;

    public Image HpImage;
    public Image HpImage2;
    public Image BulletEnegyImage;
    
    [SerializeField] Image ShootFeedBack;
    public bool _Ishit; // �����ĤH

    public Animator animator;

    public GameObject turret , shell;

    private void Start()
    {
        maxHp = 100f;
        Hp = maxHp;
        Hp2 = maxHp;
        _underAttack = false;
        _Ishit = false;
        CounterTime = 1.5f;

        _FeedBackTime = 0f;
        
        ShootFeedBack.color = new Color(ShootFeedBack.color.r, ShootFeedBack.color.g, ShootFeedBack.color.b, 0f);

    }

    private void Awake()
    {
        _instance = this;
    }
    

    void Update()
    {
        
        BulletEnegyImage.fillAmount = BulletEnegy / 2.5f;
        HpImage.fillAmount = Hp / 100f;

        float healAmount = 0.1f * Time.deltaTime; // �C���ɦ媺�q
        Heal(healAmount);

        if (Hp <= 0)
        {
            Destroy(GetComponent<Rigidbody>());
            Destroy(turret);
            Destroy(shell);

            GetComponent<Move>().enabled = false;
            GetComponent<PlayerRotation>().enabled = false;
            GetComponent<Shoot>().enabled = false;
            GetComponent<DisableTankActionScript>().enabled = false;
            enabled = false;
        }
        else
        {
            turret.transform.position = shell.transform.position + shell.transform.up * 1.1f;
        }

        UnderAttack();

        if (_Ishit)
        {
            _FeedBackTime -= Time.deltaTime;
            if (_FeedBackTime <= 0f)
            {
                ShootFeedBack.color = new Color(ShootFeedBack.color.r, ShootFeedBack.color.g, ShootFeedBack.color.b, 0f);
                _Ishit = false;
            }
        }


    }

    void UnderAttack()
    {
        if (_underAttack)
        {
            CounterTime -= Time.deltaTime;

            if (Hp2 <= Hp)
            {
                Hp2 = Hp;
                _underAttack = false;
            }
            else if (CounterTime <= 0)
            {
                float decreaseSpeed = 4f;
                Hp2 -= decreaseSpeed * Time.deltaTime;
                HpImage2.fillAmount = Hp2 / 100;
            }

        }
        else
        {
            Hp2 = Hp;
        }
    }


    public void Ishit()
    {
        _Ishit = true;
        _FeedBackTime = 0.1f;
        ShootFeedBack.color = new Color(ShootFeedBack.color.r, ShootFeedBack.color.g, ShootFeedBack.color.b, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {

            Hp -= Random.Range(1, 5);
            _underAttack = true;
            CounterTime = 1.5f;

        }
    }

    private void Heal(float healAmount)
    {
        int hpMax = 100;

        if(Hp >= hpMax)
        {
            Hp = 100f;
        }
        else
        {
            Hp += healAmount;
        }

            
    }

}
