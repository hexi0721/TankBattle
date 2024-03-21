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

    float _clamp;
    Vector2 ScreenPos;

    public float tmp; // AIM �Z�� �ݭק�

    private void Update()
    {
        // ���k�O������ �u���ܰ��� y
        Vector3 v = MainCamera.transform.position + MainCamera.transform.forward * 20 - transform.position;
        v.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(v);
        if (targetRotation.eulerAngles.x > 3.0f && targetRotation.eulerAngles.x < 180f)
        {
            _clamp = 3.0f;
        }
        else if (targetRotation.eulerAngles.x > 180f && targetRotation.eulerAngles.x < 352f)
        {
            _clamp = -8.0f;
        }
        else
        {
            _clamp = targetRotation.eulerAngles.x;
        }

        //transform.forward = new Vector3(transform.forward.x, Mathf.Lerp(transform.forward.y, v.y, Time.deltaTime * 2), transform.forward.z);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_clamp, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), 2 * Time.deltaTime);


        RaycastHit hit;

        // �ק�layer 
        if(Physics.Raycast(transform.position , transform.forward , out hit))
        {
            if (hit.collider != null) 
            {
                Debug.Log(hit.point);
            
            }
        }


        // �@�ɮy���ഫ�ù��y��
        ScreenPos = Camera.main.WorldToScreenPoint(hit.point);
        MuzzleAimImage.transform.position = ScreenPos;

        /*
        // �@�ɮy���ഫ�ù��y��
        ScreenPos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * tmp);
        MuzzleAimImage.transform.position = ScreenPos;
        */
    }

    

}
