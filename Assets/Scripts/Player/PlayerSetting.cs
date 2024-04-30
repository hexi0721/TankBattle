using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetting : MonoBehaviour
{
    private static PlayerSetting _instance;

    public static PlayerSetting Instance 
    {
        get { return _instance; }

    }

    public float Hp; // ���a�ͩR ����� ��GameController�P�_0�ɵ����C��
    public float Hp2; // ������
    public float BulletEnegy;
    bool _UnderAttack;
    float Counter; // �p���������ɰʧ@

    public Image HpImage;
    public Image HpImage2;

    public Image BulletEnegyImage;

    private void Start()
    {
        _UnderAttack = false;
        Counter = 1.5f;
    }

    private void Awake()
    {
        _instance = this;
    }
    

    void Update()
    {

        BulletEnegyImage.fillAmount = BulletEnegy / 2.5f;

        HpImage.fillAmount = Hp / 100;
        if (Hp < 0)
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
