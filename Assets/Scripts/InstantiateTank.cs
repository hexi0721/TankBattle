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

    //List<Vector3> _Pos , _Rot; // �Ĥ�Z�J��l�X�ͦ�m

    bool _AnyTank; // �O�_�٦���L�Z�J
    Vector3 _InitPoint; // �L�u�t�ͦ��Z�J���I
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

        // �S���Ĥ�Z�J�� �C�L�X��ͦ��@�x
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

}
