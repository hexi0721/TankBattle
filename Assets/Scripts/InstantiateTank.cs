using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstantiateTank : MonoBehaviour
{
    public GameObject EnemyTankPrefab;
    public GameObject EnemyPoingPrefab;

    // �P RadarPoint �t�X
    public Camera MapCamera; // �a�����Y
    public RectTransform Map; // �a�� canvas
    public RectTransform MapBG; // �a�� BG

    bool _FactoryTank; // �O�_�٦���L�Z�J
    public Transform _InitPoint;
    [SerializeField]float _InitCooldown;


    private void Start()
    {

        _InitCooldown = 8f;
        _FactoryTank = true;

    }

    private void Update()
    {
        _FactoryTank = GameObject.FindWithTag("FactoryTank") ? true : false;
        
        // ��L�Ͳ��Z�J �٦� �Ͳ��ɶ��� �h�Ͳ��Z�J
        if (_FactoryTank == false)
        {
            _InitCooldown -= Time.deltaTime;

            if(_InitCooldown < 0)
            {
                BuildTank(new List<Vector3>() { _InitPoint.position  } , "FactoryTank");
                _InitCooldown = 15f;
            }

        }

    }

    public void BuildTank(List<Vector3> Pos) // �ͦ��Z�J �P �p�F
    {
        for (int i = 0; i < Pos.Count; i++)
        {

            GameObject Tgo = Instantiate(EnemyTankPrefab, Pos[i], Quaternion.identity) as GameObject; // �Ĥ�Z�J
            

            GameObject EPgo = Instantiate(EnemyPoingPrefab) as GameObject; // �Ĥ�Z�J�p�F


            EPgo.transform.SetParent(Map.transform);
            EPgo.GetComponent<RadarPoing>().MapCamera = MapCamera;
            EPgo.GetComponent<RadarPoing>().Obj = Tgo;
            EPgo.GetComponent<RadarPoing>().Map = Map;
            EPgo.GetComponent<RadarPoing>().MapBG = MapBG;
            
            EPgo.transform.localScale = Vector3.one;
            EPgo.transform.localEulerAngles = Vector3.zero;
            
        }
    }

    public void BuildTank(List<Vector3> Pos , string s) // �ͦ��Z�J �P �p�F
    {
        for (int i = 0; i < Pos.Count; i++)
        {

            GameObject Tgo = Instantiate(EnemyTankPrefab, Pos[i], Quaternion.identity) as GameObject; // �Ĥ�Z�J
            Tgo.tag = s;


            GameObject EPgo = Instantiate(EnemyPoingPrefab) as GameObject; // �Ĥ�Z�J�p�F

            EPgo.transform.SetParent(Map.transform);
            EPgo.GetComponent<RadarPoing>().MapCamera = MapCamera;
            EPgo.GetComponent<RadarPoing>().Obj = Tgo;
            EPgo.GetComponent<RadarPoing>().Map = Map;
            EPgo.GetComponent<RadarPoing>().MapBG = MapBG;

            EPgo.transform.localScale = Vector3.one;
            EPgo.transform.localEulerAngles = Vector3.zero;

        }
    }

}
