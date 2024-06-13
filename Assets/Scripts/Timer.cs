using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{

    public TMP_Text Time_txt;
    [SerializeField]float _time = 300f;

    public float EnemyBackUpTime
    {
        get => _time;
    }

    GameManage _gameManage;

    private void Start()
    {
        _gameManage = GetComponent<GameManage>();
    }

    void Update()
    {

        // 計時器
        if (_gameManage.IsOpenMenu == false && PlayerSetting.Instance.animator.enabled == false)
        {
            _time -= Time.deltaTime;
            Time_txt.text = "敵方援軍抵達時間 : " + ((int)_time);
        }


    }
}
