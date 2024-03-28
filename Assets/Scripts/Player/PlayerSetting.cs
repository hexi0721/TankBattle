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

    public float Hp; // ���a�ͩR ��GameController�P�_0�ɵ����C��
    public float BulletEnegy;

    //public float MaxHp;
    public Image HpImage;

    //public float MaxBulletEnegy;
    public Image BulletEnegyImage;


    private void Awake()
    {
        _instance = this;
    }
    

    void Update()
    {

        BulletEnegyImage.fillAmount = BulletEnegy / 2.5f;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {

            Hp -= Random.Range(5, 10);
            HpImage.fillAmount = Hp / 100;


            if (Hp < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // �ݷs�W�ɦ�\��

}
