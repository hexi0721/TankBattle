using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public GameObject MapCam; // 地圖鏡頭
    public RectTransform MapBG , MapCanvas; // 地圖bg , MapCanvas 

    public Image OpeningBlackBg;

    bool _showMap , _IsOpenMenu ;
    public bool IsOpenMenu
    {
        get { return _IsOpenMenu; }
    }
    float _speed = 5f;

    GameObject _PlayerTank ;

    
    private void Start()
    {
        Cursor.visible = false;

        _showMap = false;
        _IsOpenMenu = false; // true 是開啟菜單 false 是關閉菜單

        MapCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        MapBG.localPosition = new Vector3(-MapCanvas.rect.width / 2 , 0, 0);

        _PlayerTank = GameObject.FindWithTag("Player");
        


    }

    private void Update()
    {
        TabKeyCode();

        EscKeyCode();

        if(_PlayerTank == null && PlayerSetting.Instance.Hp2 >= 0)
        {

            PlayerSetting.Instance.Hp2 -= 1;
            PlayerSetting.Instance.HpImage2.fillAmount = PlayerSetting.Instance.Hp2 / 100;
        }

        float BlackBgAlpha = OpeningBlackBg.color.a - (Time.deltaTime * 0.25f);
        OpeningBlackBg.color = new Color(0, 0, 0, BlackBgAlpha);
        
    }

    private void EscKeyCode()
    {


        // Esc 開關菜單
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _IsOpenMenu = !_IsOpenMenu;
            if (_IsOpenMenu)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    private void TabKeyCode()
    {
        if (Input.GetKey(KeyCode.Tab) && !_IsOpenMenu)
        {
            _showMap = true;

            if (_showMap)
            {
                MapBG.localPosition = Vector3.Lerp(MapBG.localPosition, new Vector3(0, MapBG.localPosition.y, MapBG.localPosition.z), _speed * Time.deltaTime);

                MapCanvas.GetComponent<CanvasGroup>().alpha += _speed * Time.deltaTime;

            }

        }

        if (Input.GetKeyUp(KeyCode.Tab) || _IsOpenMenu)
        {
            _showMap = false;

        }
        if (!_showMap)
        {
            MapBG.localPosition = Vector3.Lerp(MapBG.localPosition, new Vector3(-MapCanvas.rect.width / 2, MapBG.localPosition.y, MapBG.localPosition.z), _speed * Time.deltaTime);

            MapCanvas.GetComponent<CanvasGroup>().alpha -= _speed * Time.deltaTime;
        }
    }

    
}
