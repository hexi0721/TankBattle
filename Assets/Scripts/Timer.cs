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


        // ��L�u�t�Q���� �Ĥ贩�x��F�ɶ� �ܬ�0
        if (EnemyFactoryScript.Hp < EnemyFactoryScript.MaxHp || _gameManage.BackUpTrigger == true)
        {
            _time = 0f;
            Timer_txt.enabled = false;
            return;

        }

        // �p�ɾ�
        if (_gameManage.IsOpenMenu == false && PlayerSetting.Instance.animator.enabled == false && _time >= 0f)
        {
            _time -= Time.deltaTime;
            Timer_txt.text = "<color=#FF0000>�Ĥ贩�x��F�ɶ� : " + ((int)_time) + "</color>";
        }

    }
}
