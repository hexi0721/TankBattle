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

    public float _distance;


    // 瞄準圖片
    public GameObject MuzzleAimImage; // 原本倍率
    public GameObject AimImage; // 放大倍率
    public RectTransform AimC; // Aim canva

    Vector2 screenPos;

    public Vector2 ScreenPos
    {
        get => screenPos;

    }

    private void Start()
    {

        _rotation = transform.eulerAngles;

        _LookUpMin = -10 ;
        _LookUpMax = 6;

    }

    private void LateUpdate()
    {


        if (!PlayerSetting.Instance.animator.enabled)
        {
            transform.position = Obj.transform.position + transform.up;
            // 世界座標轉換視角座標
            screenPos = transform.GetComponent<Camera>().WorldToViewportPoint(Obj.transform.GetChild(0).position + Obj.transform.GetChild(0).forward * 1000);
            Debug.Log(screenPos);

            if (Obj != null && Input.GetMouseButton(1) && !gameManage.IsOpenMenu)
            {
                screenPos.y -= 0.28f;
                
                MuzzleAimImage.SetActive(false);
                AimImage.SetActive(true);
                transform.GetComponent<Camera>().fieldOfView = 10f;
                AimImage.transform.localPosition = new Vector3((screenPos.x * AimC.rect.width) - AimC.rect.width / 2, ((screenPos.y * AimC.rect.height) - AimC.rect.height / 2), 0);

            }
            else
            {
                screenPos.y -= 0.04f;
                AimImage.SetActive(false);
                MuzzleAimImage.SetActive(true);
                
                transform.GetComponent<Camera>().fieldOfView = 60f;
                MuzzleAimImage.transform.localPosition = new Vector3((screenPos.x * AimC.rect.width) - AimC.rect.width / 2, ((screenPos.y * AimC.rect.height) - AimC.rect.height / 2), 0);
            }
            
            

            if (!gameManage.IsOpenMenu)
            {
                _rotation.x += Input.GetAxis("Mouse Y") * CamSmoothFactor * (-1);
                _rotation.y += Input.GetAxis("Mouse X") * CamSmoothFactor;

                _rotation.x = Mathf.Clamp(_rotation.x, _LookUpMin, _LookUpMax);
                transform.localEulerAngles = _rotation;
            }

        }
        else
        {
            transform.position = Obj.transform.position + transform.up;
            transform.forward = Obj.transform.forward;

        }
    }

}
