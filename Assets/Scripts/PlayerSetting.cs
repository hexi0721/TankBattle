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

    public float Hp; // 玩家生命 讓GameController判斷0時結束遊戲
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


        HpImage.fillAmount = Hp / 100;
        BulletEnegyImage.fillAmount = BulletEnegy / 2.5f;
    }
}
