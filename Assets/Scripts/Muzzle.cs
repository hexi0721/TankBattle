using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public GameObject MainCamera;
    
    // �˷ǹϤ�
    public GameObject MuzzleAimImage;

    public float tmp;
    public float tmp2;
    public float tmp3;

    private void Update()
    {
        // ���k�O������ �u���ܰ��� y
        
        Vector3 v = MainCamera.transform.position + MainCamera.transform.forward * 20 - transform.position;
        v.Normalize();

        //�վ�W�U����
        v.y = Mathf.Clamp(v.y, -0.01f, 0.05f);
        transform.forward = new Vector3(transform.forward.x, Mathf.Lerp(transform.forward.y, v.y, Time.deltaTime * 2), transform.forward.z);
        //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(transform.forward.x , v.y , transform.forward.z), tmp * Time.deltaTime, 0.0F));
        // �@�ɮy���ഫ�ù��y��
        Vector2 ScreenPos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * 30);
        MuzzleAimImage.transform.position = ScreenPos;


        

    }

}
