using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTank : MonoBehaviour
{
    public GameObject EnemyTankPrefab;
    public GameObject EnemyPoingPrefab;

    public Camera MapCamera; // 地圖鏡頭
    public RectTransform Map; // 地圖 canva

    List<Vector3> Pos = new List<Vector3>() { new Vector3(176.63f, 40.59657f, 104.91f),  new Vector3(108f, 40.59657f, 170.5f) };
    List<Vector3> Rot = new List<Vector3>();

    private void Start()
    {

        GameStartInit();

        
    }

    private void GameStartInit()
    {
        // init 待修
        for (int i = 0;i < Pos.Count;i++)
        {
            GameObject Tgo = Instantiate(EnemyTankPrefab, Pos[i], Quaternion.identity) as GameObject;

            GameObject EPgo = Instantiate(EnemyPoingPrefab) as GameObject;


            EPgo.transform.SetParent(Map.transform);
            EPgo.GetComponent<RadarPoing>().MapCamera = MapCamera;
            EPgo.GetComponent<RadarPoing>().Obj = Tgo;
            EPgo.GetComponent<RadarPoing>().Map = Map;

            EPgo.transform.localScale = new Vector3(1, 1, 1);
        }


        
        


       


    }
}
