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
    
    Vector2 O; // �ù�����
    bool _IsOpenMenu; // �O�_�}�ҵ��
    bool _IsCenter;  // �O�_�b����

    

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
        // �P�_���ЬO�_�������I
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
        // ���в��� & ��v�����
        if (!_IsOpenMenu)
        {

            Vector3 ScreenPosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            Vector3 ScreenPosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);

            Vector3 WorldPosNear = Camera.main.ScreenToWorldPoint(ScreenPosNear);
            Vector3 WorldPosFar = Camera.main.ScreenToWorldPoint(ScreenPosFar);

            Vector3 v = WorldPosFar - WorldPosNear;

            v.Normalize();

            if (_IsCenter == false)  // �p�G���Ф��b���߫h��V
            {
                transform.forward = v;
            }

            Mouse.current.WarpCursorPosition(O); // ���Ц^�k�ܿù�����


        }
    }

    private void EscEnable()
    {
        // Esc �}�����
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

    private void SetO() // �]�m�ù������I
    {
        
        O = new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
