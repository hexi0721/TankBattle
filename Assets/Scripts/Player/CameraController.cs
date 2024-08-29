using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.Controls.AxisControl;


public class CameraController : MonoBehaviour
{

    public GameObject turret;
    public GameObject Muzzle;
    
    GameManage gameManage;
    
    Vector3 _rotation;
    [SerializeField] float CamSmoothFactor;
    float _LookUpMin , _LookUpMax ;

    // 瞄準圖片
    public GameObject MuzzleAimImage; // 準心
    public GameObject AimImage; // 放大黑幕
    public RectTransform AimC; // Aim canva

    Vector2 screenPos;

    public Vector2 ScreenPos // 給PlayerRotation
    {
        get => screenPos;

    }

    private void Start()
    {
        gameManage = GameObject.FindWithTag("GameManage").GetComponent<GameManage>();

        _rotation = turret.transform.localEulerAngles;

        _LookUpMin = -10 ;
        _LookUpMax = 6;
        CamSmoothFactor = 2f;
    }


    private void LateUpdate()
    {
        if(PlayerSetting.Instance.Hp <= 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x , 0 , transform.localPosition.z);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y , -35f);
        }
        else
        {
            transform.position = turret.transform.position + turret.transform.up * 1.1f;
            switch (!PlayerSetting.Instance.animator.enabled)
            {
                case false:

                    transform.forward = turret.transform.forward;

                    break;

                case true:

                    if (!gameManage.IsOpenMenu)
                    {

                        // 世界座標轉換視角座標
                        screenPos = GetComponent<Camera>().WorldToViewportPoint(Muzzle.transform.position + Muzzle.transform.forward * 1000);
                        screenPos.y = 0.5f;

                        if (Input.GetMouseButton(1)) // 右鍵放大
                        {
                            MuzzleAimImage.SetActive(true);
                            MuzzleAimImage.transform.localPosition = new Vector3((screenPos.x * AimC.rect.width) - AimC.rect.width / 2, ((screenPos.y * AimC.rect.height) - AimC.rect.height / 2), 0);

                            AimImage.transform.localScale = Vector3.Lerp(AimImage.transform.localScale, Vector3.one * 3.5f, 4f * Time.deltaTime);
                            GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetComponent<Camera>().fieldOfView, 10f, 4f * Time.deltaTime);

                        }
                        else
                        {
                            MuzzleAimImage.SetActive(false);


                            AimImage.transform.localScale = Vector3.Lerp(AimImage.transform.localScale, Vector3.one * 8f, 4f * Time.deltaTime);
                            GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetComponent<Camera>().fieldOfView, 60f, 4f * Time.deltaTime);

                        }

                        // 滑鼠移動時畫面跟著移動
                        _rotation.x += Input.GetAxis("Mouse Y") * CamSmoothFactor * (-1);
                        _rotation.y += Input.GetAxis("Mouse X") * CamSmoothFactor;

                        _rotation.x = Mathf.Clamp(_rotation.x, _LookUpMin, _LookUpMax);
                        transform.localEulerAngles = _rotation;


                    }

                    break;
            }
        }
        
    }

}
