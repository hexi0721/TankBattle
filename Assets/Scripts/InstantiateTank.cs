using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstantiateTank : MonoBehaviour
{
    public GameObject EnemyTankPrefab;
    public GameObject EnemyPoingPrefab;

    /*
    // �P RadarPoint �t�X
    public Camera MapCamera; // �a�����Y
    public RectTransform Map; // �a�� canvas
    public RectTransform MapBG; // �a�� BG
    */

    public Transform _InstantiatePoint;
    float _InstantiateCooldown;


    private void Start()
    {

        _InstantiateCooldown = 8f;

    }

    private void Update()
    {
        
        // ��L�Ͳ��Z�J �٦� �Ͳ��ɶ��� �h�Ͳ��Z�J �]���K�[����s 
        if (GameObject.FindWithTag("FactoryTank") == false)
        {
            _InstantiateCooldown -= Time.deltaTime;

            if(_InstantiateCooldown < 0)
            {
                BuildTank(new List<Vector3>() { _InstantiatePoint.position  } , "FactoryTank");
                _InstantiateCooldown = 12f;
            }

        }

    }

    public void BuildTank(List<Vector3> Pos) // �ͦ��Z�J �P �p�F
    {
        for (int i = 0; i < Pos.Count; i++)
        {
            Instantiate(EnemyTankPrefab, Pos[i], Quaternion.identity); // �Ĥ�Z�J
            // GameObject Tgo = Instantiate(EnemyTankPrefab, Pos[i], Quaternion.identity) as GameObject; // �Ĥ�Z�J
            

            // GameObject EPgo = Instantiate(EnemyPoingPrefab) as GameObject; // �Ĥ�Z�J�p�F


            // EPgo.transform.SetParent(Map.transform);
            // EPgo.GetComponent<RadarPoing>().MapCamera = MapCamera;
            // EPgo.GetComponent<RadarPoing>().Obj = Tgo;
            // EPgo.GetComponent<RadarPoing>().Map = Map;
            // EPgo.GetComponent<RadarPoing>().MapBG = MapBG;
            
            // EPgo.transform.localScale = Vector3.one;
            // EPgo.transform.localEulerAngles = Vector3.zero;
            
        }
    }

    public void BuildTank(List<Vector3> Pos , string s) // �ͦ��Z�J �P �p�F
    {
        for (int i = 0; i < Pos.Count; i++)
        {

            
            
            GameObject Tgo = Instantiate(EnemyTankPrefab, Pos[i], Quaternion.identity) as GameObject; // �Ĥ�Z�J
            Tgo.tag = s;
            /*

            GameObject EPgo = Instantiate(EnemyPoingPrefab) as GameObject; // �Ĥ�Z�J�p�F

            EPgo.transform.SetParent(Map.transform);
            EPgo.GetComponent<RadarPoing>().MapCamera = MapCamera;
            EPgo.GetComponent<RadarPoing>().Obj = Tgo;
            EPgo.GetComponent<RadarPoing>().Map = Map;
            EPgo.GetComponent<RadarPoing>().MapBG = MapBG;

            EPgo.transform.localScale = Vector3.one;
            EPgo.transform.localEulerAngles = Vector3.zero;
            */
        }
    }

}
