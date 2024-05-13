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

    //List<Vector3> _Pos , _Rot; // 敵方坦克初始出生位置

    bool _AnyTank; // 是否還有其他坦克
    Vector3 _InitPoint; // 兵工廠生成坦克的點
    [SerializeField]float _InitCooldown;


    private void Start()
    {

        /*
        _Pos = new List<Vector3>() { new Vector3(180.74f, 40.59657f, 104.9098f) , new Vector3(108f, 40.59657f, 170.5f), new Vector3(35f, 40.59657f, 142.8f) , new Vector3(47.03f , 40.59657f , 113.69f) ,
                                    new Vector3(43.41f , 40.59657f , 80.57f) , new Vector3(43.05f , 40.59657f, 57.57f) , new Vector3(16.6f , 40.59657f , 16.6f) , new Vector3(81.06f , 40.59657f , 127.9f) ,
                                    new Vector3(103.16f, 40.59657f , 89.44f) , new Vector3(121.5f , 40.59657f , 33.75f) };
        
        _Rot = new List<Vector3>() { new Vector3(0f, 0f, 0f) , new Vector3(0f, 140.446f, 0f), new Vector3(0f, 0f, 0f) , new Vector3(0f , 270f , 0f) ,
                                    new Vector3(0f , 270f , 0f) , new Vector3(0f , 270f , 0f) , new Vector3(0f , 0f , 0f) , new Vector3(0f , 180f , 0f) ,
                                    new Vector3(0f, -135f , 0f) , new Vector3(0f , 270f , 0f) };
        */
        
        _InitPoint = new Vector3(172.07f, 40.59658f, 36.92f);
        _InitCooldown = 8f;
        //BuildTank(_Pos); // GameInit
        _AnyTank = true;


    }

    private void Update()
    {
        _AnyTank = GameObject.FindWithTag("EnemyTank") ? true : false;

        // 沒有敵方坦克時 每過幾秒生成一台
        if (!_AnyTank)
        {
            _InitCooldown -= Time.deltaTime;

            if(_InitCooldown < 0)
            {
                BuildTank(new List<Vector3>() { _InitPoint });
                _InitCooldown = 8f;
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

}
