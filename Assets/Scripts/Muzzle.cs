using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI.Table;

public class Muzzle : MonoBehaviour
{
    public GameObject MainCamera;
    
    // �˷ǹϤ�
    public GameObject MuzzleAimImage;

    float clamp;


    private void Update()
    {
        // ���k�O������ �u���ܰ��� y
        
        Vector3 v = MainCamera.transform.position + MainCamera.transform.forward * 20 - transform.position;
        v.Normalize();


        Quaternion targetRotation = Quaternion.LookRotation(v);
        
        if (targetRotation.eulerAngles.x > 3.0f && targetRotation.eulerAngles.x < 180f)
        {
            clamp = 3.0f;
        }
        else if (targetRotation.eulerAngles.x > 180f && targetRotation.eulerAngles.x < 352f)
        {
            clamp = -8.0f;
        }
        else
        {
            clamp = targetRotation.eulerAngles.x;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(clamp , transform.rotation.eulerAngles.y , transform.rotation.eulerAngles.z), 2 * Time.deltaTime);


        // �@�ɮy���ഫ�ù��y��
        Vector2 ScreenPos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * 30);
        MuzzleAimImage.transform.position = ScreenPos;


        

    }

}
