using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject MapCam; // �a�����Y
    public RectTransform MapBG , MapCanvas; // �a��bg , MapCanvas
    
    bool _showMap ;
    public bool IsOpenMenu; // �O�_�}�ҵ��
    float _speed = 5f;

    GameObject _PlayerTank;
    private void Start()
    {
        
        _showMap = false;
        IsOpenMenu = true;

        MapCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        MapBG.localPosition = new Vector3(-MapCanvas.rect.width / 2 , 0, 0);

        _PlayerTank = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        TabKeyCode();

        EscKeyCode();

        if(_PlayerTank)
        {
            // �ק������T�k�s
            Debug.Log(true);
        }

    }

    private void EscKeyCode()
    {


        // Esc �}�����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsOpenMenu = !IsOpenMenu;
            if (IsOpenMenu)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }
    }

    private void TabKeyCode()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            _showMap = true;

            if (_showMap)
            {
                MapBG.localPosition = Vector3.Lerp(MapBG.localPosition, new Vector3(0, MapBG.localPosition.y, MapBG.localPosition.z), _speed * Time.deltaTime);

                MapCanvas.GetComponent<CanvasGroup>().alpha += _speed * Time.deltaTime;

            }

        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _showMap = false;

        }
        if (!_showMap)
        {
            MapBG.localPosition = Vector3.Lerp(MapBG.localPosition, new Vector3(-MapCanvas.rect.width / 2, MapBG.localPosition.y, MapBG.localPosition.z), _speed * Time.deltaTime);

            MapCanvas.GetComponent<CanvasGroup>().alpha -= _speed * Time.deltaTime;
        }
    }


    public bool GetIsOpenMenu()
    {
        return IsOpenMenu;
    }
}
