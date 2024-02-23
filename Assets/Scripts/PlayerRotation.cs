using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerRotation : MonoBehaviour
{

    public GameObject MainCamera;
    public float speed; // 平移速度
    

    private void Update()
    {

        // 左右轉向 y 高度保持不變
        
        Vector3 v = MainCamera.transform.position + MainCamera.transform.forward * 20.0f -transform.position;
        v.Normalize();

        transform.forward = new Vector3(Mathf.Lerp(transform.forward.x, v.x, Time.deltaTime*speed), transform.forward.y, Mathf.Lerp(transform.forward.z, v.z, Time.deltaTime*speed));

    }


}
