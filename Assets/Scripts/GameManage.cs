using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject MapCam; // 地圖鏡頭
    public RectTransform Map; // 地圖bg

    bool _visible;
    bool _showMap;
    bool _Fade;
    float _speed = 5f;
    private void Start()
    {
        _visible = false;
        _showMap = false;
        _Fade = false;
        Map.localPosition = new Vector3(-Map.transform.parent.GetComponent<RectTransform>().rect.width / 2 , 0, 0);
    }

    private void Update()
    {
        //Debug.Log(Screen.width + " " + Screen.height);
        if (Input.GetKey(KeyCode.Tab))
        {
            _showMap = true;
            if(_showMap)
            {
                Map.localPosition = Vector3.Lerp(Map.localPosition, new Vector3(0, Map.localPosition.y, Map.localPosition.z), _speed * Time.deltaTime);
            }

        }

        if(Input.GetKeyUp(KeyCode.Tab))
        {
            _showMap = false;
        }
        if(!_showMap)
        {
            Map.localPosition = Vector3.Lerp(Map.localPosition, new Vector3(-Map.transform.parent.GetComponent<RectTransform>().rect.width / 2, Map.localPosition.y, Map.localPosition.z), _speed * Time.deltaTime);
        }




        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // menu
        }

        
        
    }



    void FadeIn()
    {
        _Fade = true;
    }

    void FadeOut()
    {
        _Fade = false;
    }
}
