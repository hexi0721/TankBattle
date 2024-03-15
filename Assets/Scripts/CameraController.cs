using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;


public class CameraController : MonoBehaviour
{

    public GameObject Obj; // player turret
    
    Vector2 O; // 螢幕中央
    bool _IsOpenMenu; // 是否開啟菜單
    bool _IsCenter;  // 是否在中央

    

    private void Start()
    {

        Cursor.visible = true;

        SetO();

        _IsOpenMenu = true;
        _IsCenter = false;
    }

    private void LateUpdate()
    {

        transform.position = Obj.transform.position + Obj.transform.forward * -0.6f + transform.up;

        MouseMove();
        

    }

    private void Update()
    {
        

        EscEnable();

        SetO();

        IsMouseCenter();

        //MouseMove();

        


    }

    

    private void IsMouseCenter()
    {
        // 判斷鼠標是否為中心點
        if (Input.mousePosition.x <= O.x + 1.0f && Input.mousePosition.x >= O.x - 1.0f && Input.mousePosition.y <= O.y + 1.0f && Input.mousePosition.y >= O.y - 1.0f)
        {
            _IsCenter = true;
        }
        else
        {
            _IsCenter = false;
        }
    }

    private void MouseMove()
    {
        // 鼠標移動 & 攝影機轉動
        if (!_IsOpenMenu)
        {

            Vector3 ScreenPosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            Vector3 ScreenPosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);

            Vector3 WorldPosNear = Camera.main.ScreenToWorldPoint(ScreenPosNear);
            Vector3 WorldPosFar = Camera.main.ScreenToWorldPoint(ScreenPosFar);

            Vector3 v = WorldPosFar - WorldPosNear;

            v.Normalize();

            if (_IsCenter == false)  // 如果鼠標不在中心則轉向
            {
                transform.forward = v;
            }

            Mouse.current.WarpCursorPosition(O); // 鼠標回歸至螢幕中心


        }
    }

    private void EscEnable()
    {
        // Esc 開關菜單
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _IsOpenMenu = !_IsOpenMenu;
            if (_IsOpenMenu)
            {
                Cursor.visible = true;
            }
            else
            {
                Cursor.visible = false;
            }
        }
    }

    private void SetO() // 設置螢幕中心點
    {
        
        O = new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
