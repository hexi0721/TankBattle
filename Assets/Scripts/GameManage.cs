using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject MapCam; // ¶aπœ√Ë¿Y
    public RectTransform Map;

    bool _visible;
    bool _showMap;

    private void Start()
    {
        _visible = false;
        _showMap = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {

            _showMap = true;
            _visible = true;

        }

        if(Input.GetKeyUp(KeyCode.Tab))
        { 
            _visible = false; 
        }

        if(_showMap)
        {
            ShowOrHideMap();
        }
        
    }

    private void ShowOrHideMap()
    {
        float speed = 5f;
        if (_visible)
        {


            Map.localPosition = Vector3.Lerp(Map.localPosition , new Vector3(0 , Map.localPosition.y , Map.localPosition.z), speed * Time.deltaTime);



        }
        else
        {
            Map.localPosition = Vector3.Lerp(Map.localPosition, new Vector3(-450, Map.localPosition.y, Map.localPosition.z), speed * Time.deltaTime);

            
        }

        if (Map.localPosition.x == 0)
        {
            
            _showMap = false;
        }

        if (Map.localPosition.x == -450)
        {
            
            _showMap = false;
        }

        
    }
}
