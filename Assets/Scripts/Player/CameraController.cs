using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.Controls.AxisControl;


public class CameraController : MonoBehaviour
{

    public GameObject Obj; // player turret
    public GameManage gameManage;
    
    Vector3 _rotation;
    public float CamSmoothFactor;
    float _LookUpMin , _LookUpMax ;

    private void Start()
    {

        _rotation = transform.eulerAngles;

        _LookUpMin = -10 ;
        _LookUpMax = 6;

    }

    private void LateUpdate()
    {

        if (Obj != null)
        {
            if (Input.GetMouseButton(1) && !gameManage.IsMenuOpen())
            {
                transform.position = Obj.transform.position + transform.up + transform.forward * 6f;
            }
            else
            {
                transform.position = Obj.transform.position + transform.up;
            }


        }


        if (!PlayerSetting.Instance.animator.enabled)
        {

            if (!gameManage.IsMenuOpen())
            {
                _rotation.x += Input.GetAxis("Mouse Y") * CamSmoothFactor * (-1);
                _rotation.y += Input.GetAxis("Mouse X") * CamSmoothFactor;

                _rotation.x = Mathf.Clamp(_rotation.x, _LookUpMin, _LookUpMax);
                transform.localEulerAngles = _rotation;
            }

        }
        else
        {

            transform.forward = Obj.transform.forward;

        }
    }

}
