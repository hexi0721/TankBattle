using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{

    public TMP_Text Time_txt;
    [SerializeField]float _time;

    public float EnemyBackUpTime
    {
        get => _time;
    }

    GameManage _gameManage;
    public EnemyFactory EnemyFactoryScript;

    private void Start()
    {
        _gameManage = GetComponent<GameManage>();
    }

    void Update()
    {


        // 當兵工廠被攻擊 敵方援軍抵達時間 變為0
        if (EnemyFactoryScript.Hp < EnemyFactoryScript.GetMaxHp())
        {
            _time = -1f;

        }

        // 計時器
        if (_gameManage.IsOpenMenu == false && PlayerSetting.Instance.animator.enabled == false && _time >= 0f)
        {
            _time -= Time.deltaTime;
            Time_txt.text = "<color=#FF0000>敵方援軍抵達時間 : " + ((int)_time) + "</color>";
        }
        else
        {
            Time_txt.text = "";
        }


        


    }
}
