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

    public float offset = 0.55f;
    public Terrain terrain;

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
        animator.enabled = true;
        transform.position = new Vector3(126.49f, 40.597f , 319.73f);
        //
        
    }

    private void Awake()
    {
        _instance = this;
    }
    

    void Update()
    {

        transform.position = new Vector3(transform.position.x, terrain.SampleHeight(transform.position) + offset, transform.position.z);

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
}
