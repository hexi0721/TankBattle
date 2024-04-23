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

    public static Vector3 targetPoint; // Aim �����I
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
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_clamp, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), 2 * Time.deltaTime);

        
        // �@�ɮy���ഫ�ù��y��
        ScreenPos = Camera.main.WorldToScreenPoint(transform.position + transform.forward * 1000);
        MuzzleAimImage.transform.position = ScreenPos;


        RaycastHit hit = Raycast();
        
        if (hit.collider != null)
        {
            
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = transform.position + transform.forward * 1000;
        }

        
    }


    private RaycastHit Raycast()
    {
        RaycastHit hit;

        Vector3 ScreenPosNear = new Vector3(MuzzleAimImage.transform.position.x, MuzzleAimImage.transform.position.y, Camera.main.nearClipPlane);
        Vector3 ScreenPosFar = new Vector3(MuzzleAimImage.transform.position.x, MuzzleAimImage.transform.position.y, Camera.main.farClipPlane);

        Vector3 WorldPosNear = Camera.main.ScreenToWorldPoint(ScreenPosNear);
        Vector3 WorldPosFar = Camera.main.ScreenToWorldPoint(ScreenPosFar);

        Physics.Raycast(WorldPosNear, WorldPosFar - WorldPosNear, out hit);

        return hit;
    }
}
