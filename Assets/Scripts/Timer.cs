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


        // ��L�u�t�Q���� �Ĥ贩�x��F�ɶ� �ܬ�0
        if (EnemyFactoryScript.Hp < EnemyFactoryScript.GetMaxHp())
        {
            _time = -1f;

        }

        // �p�ɾ�
        if (_gameManage.IsOpenMenu == false && PlayerSetting.Instance.animator.enabled == false && _time >= 0f)
        {
            _time -= Time.deltaTime;
            Time_txt.text = "<color=#FF0000>�Ĥ贩�x��F�ɶ� : " + ((int)_time) + "</color>";
        }
        else
        {
            Time_txt.text = "";
        }


        


    }
}
