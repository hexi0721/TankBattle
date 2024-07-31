using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class PlayerRotation : MonoBehaviour
{
    
    public GameObject MainCamera;
    public GameObject Muzzle;
    
    public float speed; // �����t��

    float _clampMin = -8f;
    float _clampMax = 3f;
    Vector3 _rotation;

    public static Vector3 targetPoint; // Aim �����I
    

    private void Start()
    {
        _rotation = transform.localEulerAngles;
        
    }

 
    private void Update()
    {
        
        // turret ���k xz �O������
        _rotation.y += Input.GetAxis("Mouse X") * 2;
        transform.localRotation = Quaternion.Lerp(transform.localRotation,
            Quaternion.Euler(transform.localRotation.eulerAngles.x, _rotation.y, transform.localRotation.eulerAngles.z), 2 * Time.deltaTime);
        
        
        // muzzle �W�U yz �O������
        _rotation.x += Input.GetAxis("Mouse Y") * 2 * (-1);
        _rotation.x = Mathf.Clamp(_rotation.x, _clampMin, _clampMax);
        Muzzle.transform.localRotation = Quaternion.Lerp(Muzzle.transform.localRotation ,
            Quaternion.Euler(_rotation.x, Muzzle.transform.localRotation.eulerAngles.y, Muzzle.transform.localRotation.eulerAngles.z), 2 * Time.deltaTime);
        
        
        RaycastHit hit = Raycast();
        
        if (hit.collider != null)
        {
            Debug.DrawRay(Muzzle.transform.position, hit.point - Muzzle.transform.position, Color.blue);
            targetPoint = hit.point;

        }
        else
        {
            targetPoint = Muzzle.transform.position + Muzzle.transform.forward * 1000;
        }
        

    }
    
    private RaycastHit Raycast() 
    {

        CameraController cameraController = MainCamera.GetComponent<CameraController>();

        RaycastHit hit;

        Vector3 ScreenPosNear = new Vector3(cameraController.ScreenPos.x, cameraController.ScreenPos.y, MainCamera.GetComponent<Camera>().nearClipPlane);
        Vector3 ScreenPosFar = new Vector3(cameraController.ScreenPos.x , cameraController.ScreenPos.y , MainCamera.GetComponent<Camera>().farClipPlane);
        
        // �����y���ഫ�@�ɮy��
        Vector3 WorldPosNear = MainCamera.GetComponent<Camera>().ViewportToWorldPoint(ScreenPosNear);
        Vector3 WorldPosFar = MainCamera.GetComponent<Camera>().ViewportToWorldPoint(ScreenPosFar);

        Physics.Raycast(WorldPosNear, WorldPosFar - WorldPosNear, out hit , 1000f , ~(1 << 14));
        Debug.DrawRay(WorldPosNear, WorldPosFar - WorldPosNear, Color.green);
        
        return hit;
    }
    
}
