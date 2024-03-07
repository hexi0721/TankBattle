using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public GameObject MainCamera;
    
    // 瞄準圖片
    public GameObject MuzzleAimImage;

    public float tmp;
    public float tmp2;
    public float tmp3;

    private void Update()
    {
        // 左右保持不變 只改變高度 y
        
        Vector3 v = MainCamera.transform.position + MainCamera.transform.forward * 20 - transform.position;
        v.Normalize();

        //調整上下限制
        v.y = Mathf.Clamp(v.y, -0.01f, 0.05f);
        transform.forward = new Vector3(transform.forward.x, Mathf.Lerp(transform.forward.y, v.y, Time.deltaTime * 2), transform.forward.z);
        //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(transform.forward.x , v.y , transform.forward.z), tmp * Time.deltaTime, 0.0F));
        // 世界座標轉換螢幕座標
        Vector2 ScreenPos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * 30);
        MuzzleAimImage.transform.position = ScreenPos;


        

    }

}
