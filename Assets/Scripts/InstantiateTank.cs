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

    List<Vector3> Pos = new List<Vector3>() { new Vector3(176.63f, 40.59657f, 104.91f) , new Vector3(108f, 40.59657f, 170.5f), new Vector3(35f, 40.59657f, 142.8f) , new Vector3(47.03f , 40.59657f , 113.69f) ,
                                              new Vector3(43.41f , 40.59657f , 80.57f) , new Vector3(43.05f , 40.59657f, 57.57f) , new Vector3(16.6f , 40.59657f , 16.6f) , new Vector3(81.06f , 40.59657f , 127.9f) , 
                                              new Vector3(103.16f, 40.59657f , 89.44f) , new Vector3(121.5f , 40.59657f , 33.75f) };
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
            Debug.Log(i);
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
