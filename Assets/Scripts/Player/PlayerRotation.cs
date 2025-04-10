using UnityEngine;


public class PlayerRotation : MonoBehaviour
{
    
    public GameObject MainCamera;
    public GameObject Muzzle;
    public GameObject turret;
    
    public float speed; // 平移速度

    float _clampMin = -8f;
    float _clampMax = 3f;
    Vector3 _rotation;

    public static Vector3 targetPoint; // Aim 中心點

    private void Start()
    {
        _rotation = turret.transform.localEulerAngles;
    }


    private void FixedUpdate()
    {

        // turret 左右 xz 保持不變
        turret.transform.localRotation = Quaternion.Lerp(turret.transform.localRotation , 
            Quaternion.Euler(turret.transform.localRotation.eulerAngles.x, MainCamera.transform.localRotation.eulerAngles.y, turret.transform.localRotation.eulerAngles.z), speed * Time.deltaTime);

        // muzzle 上下 yz 保持不變
        _rotation.x += Input.GetAxis("Mouse Y") * 2 * (-1);
        _rotation.x = Mathf.Clamp(_rotation.x, _clampMin, _clampMax);
        Muzzle.transform.localRotation = Quaternion.Lerp(Muzzle.transform.localRotation ,
            Quaternion.Euler(_rotation.x, Muzzle.transform.localRotation.eulerAngles.y, Muzzle.transform.localRotation.eulerAngles.z), 2 * Time.deltaTime);
        
        
        RaycastHit hit = Raycast();

        
        if (hit.collider != null && !Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, 4.85f, ~(1 << 14)))
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
        
        // 視角座標轉換世界座標
        Vector3 WorldPosNear = MainCamera.GetComponent<Camera>().ViewportToWorldPoint(ScreenPosNear);
        Vector3 WorldPosFar = MainCamera.GetComponent<Camera>().ViewportToWorldPoint(ScreenPosFar);

        Physics.Raycast(WorldPosNear, WorldPosFar - WorldPosNear, out hit , 1000f , ~(1 << 14));
        Debug.DrawRay(WorldPosNear, WorldPosFar - WorldPosNear, Color.green);
        
        return hit;
    }
    
}
