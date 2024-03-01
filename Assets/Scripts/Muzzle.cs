using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public GameObject MainCamera;
    
    // �˷ǹϤ�
    public GameObject MuzzleAimImage;


    private void Update()
    {
        // ���k�O������ �u���ܰ��� y
        
        Vector3 v = MainCamera.transform.position + MainCamera.transform.forward * 19.0f - transform.position;
        v.Normalize();

        //�վ�W�U����
        v.y = Mathf.Clamp(v.y, -0.05f, 0.1f);
        transform.forward = new Vector3(transform.forward.x, Mathf.Lerp(transform.forward.y, v.y, Time.deltaTime * 2), transform.forward.z);

        // �@�ɮy���ഫ�ù��y��
        Vector2 ScreenPos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * 19.0f);
        MuzzleAimImage.transform.position = ScreenPos;



    }

}
