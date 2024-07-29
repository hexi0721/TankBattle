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
    // public CameraController cameraController;

    public GameObject MainCamera;
    
    public float speed; // 平移速度

    public GameObject body;


    float _clampMin = -8f;
    float _clampMax = 3f;
    Vector3 _rotation;

    public static Vector3 targetPoint; // Aim 中心點


    private void Start()
    {
        _rotation = transform.GetChild(0).transform.localEulerAngles;
    }

    private void FixedUpdate()
    {
        /*
        // 綁定數值
        transform.localPosition = body.transform.localPosition + body.transform.up * 1.1f;
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        transform.localEulerAngles = new Vector3(body.transform.localEulerAngles.x, transform.localEulerAngles.y, body.transform.localEulerAngles.z);
        */
        


    }
    private void Update()
    {

        //Debug.Log(transform.localPosition + " " + transform.position);
        //Debug.Log(MainCamera.transform.localPosition + " " + MainCamera.transform.position);


        Debug.DrawRay(MainCamera.transform.position , MainCamera.transform.forward * 20 , Color.black);

        Vector3 v = MainCamera.transform.localPosition + MainCamera.transform.forward * 20 - transform.localPosition;
        Quaternion rotation = Quaternion.LookRotation(v.normalized);
        // turret 左右 xz 保持不變
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.localRotation.eulerAngles.x, rotation.eulerAngles.y, transform.localRotation.eulerAngles.z), Time.deltaTime);


        // muzzle 上下 yz 保持不變
        _rotation.x += Input.GetAxis("Mouse Y") * 2 * (-1);
        _rotation.x = Mathf.Clamp(_rotation.x, _clampMin, _clampMax);
        transform.GetChild(0).transform.localRotation = Quaternion.Lerp(transform.GetChild(0).transform.localRotation , Quaternion.Euler(_rotation.x, transform.GetChild(0).transform.localRotation.eulerAngles.y, transform.GetChild(0).transform.localRotation.eulerAngles.z), 2 * Time.deltaTime);
        




        RaycastHit hit = Raycast();
        
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.GetChild(0).transform.position, hit.point - transform.GetChild(0).transform.position, Color.blue);
            targetPoint = hit.point;

        }
        else
        {
            targetPoint = transform.GetChild(0).transform.position + transform.GetChild(0).transform.forward * 1000;
        }
        

    }
    
    private RaycastHit Raycast() 
    {

        CameraController cameraController = MainCamera.GetComponent<CameraController>();

        RaycastHit hit;

        Vector3 ScreenPosNear = new Vector3(cameraController.ScreenPos.x, cameraController.ScreenPos.y, MainCamera.GetComponent<Camera>().nearClipPlane);
        Vector3 ScreenPosFar = new Vector3(cameraController.ScreenPos.x , cameraController.ScreenPos.y , MainCamera.GetComponent<Camera>().farClipPlane);
        
        // 視角座標轉換世界座標
        Vector3 WorldPosNear = MainCamera.GetComponent<Camera>().ViewportToWorldPoint(ScreenPosNear);
        Vector3 WorldPosFar = MainCamera.GetComponent<Camera>().ViewportToWorldPoint(ScreenPosFar);

        Physics.Raycast(WorldPosNear, WorldPosFar - WorldPosNear, out hit , 1000f , ~(1 << 14));
        Debug.DrawRay(WorldPosNear, WorldPosFar - WorldPosNear, Color.green);
        
        return hit;
    }
    
}
