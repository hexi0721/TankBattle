using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class PlayerRotation : MonoBehaviour
{

    public GameObject MainCamera;
    
    public float speed; // 平移速度

    public GameObject body;

    
    // 瞄準圖片
    public GameObject MuzzleAimImage;
    public RectTransform AimC; // Aim canva

    float _clamp;
    Vector2 ScreenPos;
    public static Vector3 targetPoint; // Aim 中心點
    
    private void Update()
    {
        /*
        // 綁定數值
        transform.localPosition = body.transform.localPosition + body.transform.up * 1.1f;
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        */

        Vector3 v = MainCamera.transform.position + MainCamera.transform.forward * 20 - transform.position;
        Quaternion rotation = Quaternion.LookRotation(v.normalized);
        // 左右轉向 y 高度保持不變
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Time.deltaTime);

        
        // Muzzle 上下轉向
        if (rotation.eulerAngles.x > 3.0f && rotation.eulerAngles.x < 180f)
        {
            _clamp = 3.0f;
        }
        else if (rotation.eulerAngles.x > 180f && rotation.eulerAngles.x < 352f)
        {
            _clamp = -8.0f;
        }
        else
        {
            _clamp = rotation.eulerAngles.x;
        }

        transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).transform.rotation, Quaternion.Euler(_clamp, transform.GetChild(0).rotation.eulerAngles.y, transform.GetChild(0).eulerAngles.z), 2 * Time.deltaTime);


        // 世界座標轉換視角座標
        ScreenPos = MainCamera.GetComponent<Camera>().WorldToViewportPoint(transform.GetChild(0).position + transform.GetChild(0).forward * 1000);
        MuzzleAimImage.transform.localPosition = new Vector3((ScreenPos.x * AimC.rect.width) - AimC.rect.width / 2, (ScreenPos.y * AimC.rect.height) - AimC.rect.height / 2, 0);
        
        
        RaycastHit hit = Raycast();
        
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.GetChild(0).position, hit.point - transform.GetChild(0).position, Color.blue);
            targetPoint = hit.point;

        }
        else
        {
            targetPoint = transform.GetChild(0).position + transform.GetChild(0).forward * 1000;
        }
        

    }
    
    private RaycastHit Raycast() 
    {
        RaycastHit hit;

        Vector3 ScreenPosNear = new Vector3(ScreenPos.x, ScreenPos.y, MainCamera.GetComponent<Camera>().nearClipPlane);
        Vector3 ScreenPosFar = new Vector3(ScreenPos.x , ScreenPos.y , MainCamera.GetComponent<Camera>().farClipPlane);

        // 視角座標轉換世界座標
        Vector3 WorldPosNear = MainCamera.GetComponent<Camera>().ViewportToWorldPoint(ScreenPosNear);
        Vector3 WorldPosFar = MainCamera.GetComponent<Camera>().ViewportToWorldPoint(ScreenPosFar);

        Physics.Raycast(WorldPosNear, WorldPosFar - WorldPosNear, out hit);
        Debug.DrawRay(WorldPosNear, WorldPosFar - WorldPosNear, Color.blue);
        
        return hit;
    }
    
}
