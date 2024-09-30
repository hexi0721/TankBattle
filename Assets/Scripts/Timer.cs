using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class Timer : MonoBehaviour
{

    TMP_Text Timer_txt;
    float _time;

    public float EnemyBackUpTime
    {
        get => _time;
    }

    GameManage _gameManage;
    [SerializeField]EnemyFactory EnemyFactoryScript;

    private void Start()
    {
        _gameManage = GetComponent<GameManage>();
        Timer_txt = GameObject.FindWithTag("Timer").GetComponent<TMP_Text>();
        _time = 120;
        EnemyFactoryScript = GameObject.Find("EnemyFactory").GetComponent<EnemyFactory>();
    }

    void Update()
    {


        // 當兵工廠被攻擊 敵方援軍抵達時間 變為0
        if (EnemyFactoryScript.Hp < EnemyFactoryScript.MaxHp || _gameManage.BackUpTrigger == true)
        {
            _time = 0f;
            Timer_txt.enabled = false;
            return;

        }

        // 計時器
        if (_gameManage.IsOpenMenu == false && PlayerSetting.Instance.animator.enabled == false && _time >= 0f)
        {
            _time -= Time.deltaTime;
            Timer_txt.text = "<color=#FF0000>敵方援軍抵達時間 : " + ((int)_time) + "</color>";
        }

    }
}
