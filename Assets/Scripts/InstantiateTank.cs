using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstantiateTank : MonoBehaviour
{
    public GameObject EnemyTankPrefab;
    public GameObject EnemyPoingPrefab;

    // 與 RadarPoint 配合
    public Camera MapCamera; // 地圖鏡頭
    public RectTransform Map; // 地圖 canvas
    public RectTransform MapBG; // 地圖 BG

    bool _FactoryTank; // 是否還有其他坦克
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
        
        // 當無生產坦克 還有 生產時間到 則生產坦克
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

    public void BuildTank(List<Vector3> Pos) // 生成坦克 與 雷達
    {
        for (int i = 0; i < Pos.Count; i++)
        {

            GameObject Tgo = Instantiate(EnemyTankPrefab, Pos[i], Quaternion.identity) as GameObject; // 敵方坦克
            

            GameObject EPgo = Instantiate(EnemyPoingPrefab) as GameObject; // 敵方坦克雷達


            EPgo.transform.SetParent(Map.transform);
            EPgo.GetComponent<RadarPoing>().MapCamera = MapCamera;
            EPgo.GetComponent<RadarPoing>().Obj = Tgo;
            EPgo.GetComponent<RadarPoing>().Map = Map;
            EPgo.GetComponent<RadarPoing>().MapBG = MapBG;
            
            EPgo.transform.localScale = Vector3.one;
            EPgo.transform.localEulerAngles = Vector3.zero;
            
        }
    }

    public void BuildTank(List<Vector3> Pos , string s) // 生成坦克 與 雷達
    {
        for (int i = 0; i < Pos.Count; i++)
        {

            GameObject Tgo = Instantiate(EnemyTankPrefab, Pos[i], Quaternion.identity) as GameObject; // 敵方坦克
            Tgo.tag = s;


            GameObject EPgo = Instantiate(EnemyPoingPrefab) as GameObject; // 敵方坦克雷達

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
