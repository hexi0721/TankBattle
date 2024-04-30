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
    public GameManage gameManage;
    
    Vector2 O; // �ù�����
    
    bool _IsCenter;  // �O�_�b����

    

    private void Start()
    {

        Cursor.visible = true;

        SetO();

        _IsCenter = false;
    }

    private void FixedUpdate() // ��Fixed ���ӬO�]��Move�����ʦbFixed ���F�P�B���ݰ� �ҥH�o�̤]�n�bFixed (?
    {
        if(Obj != null) 
        {
            transform.position = Obj.transform.position + transform.up;
        }
        

    }

    private void Update()
    {

        SetO();
        IsMouseCenter();
        MouseMove();

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
        if (!gameManage.GetIsOpenMenu())
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


    private void SetO() // �]�m�ù������I
    {
        
        O = new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
