using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public GameObject MainCamera;
    
    // 瞄準圖片
    public GameObject MuzzleAimImage;


    private void Update()
    {
        // 左右保持不變 只改變高度 y
        
        Vector3 v = MainCamera.transform.position + MainCamera.transform.forward * 19.0f - transform.position;
        v.Normalize();

        //調整上下限制
        v.y = Mathf.Clamp(v.y, -0.05f, 0.1f);
        transform.forward = new Vector3(transform.forward.x, Mathf.Lerp(transform.forward.y, v.y, Time.deltaTime * 2), transform.forward.z);

        // 世界座標轉換螢幕座標
        Vector2 ScreenPos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * 19.0f);
        MuzzleAimImage.transform.position = ScreenPos;



    }

}
