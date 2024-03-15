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
    
    public float tmp;

    public GameObject body;


    private void FixedUpdate()
    {
        // 綁定數值
        transform.position = body.transform.position + body.transform.up * 1.1f;
        transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, transform.eulerAngles.y, body.transform.eulerAngles.z);


    }

    private void Update()
    {
        


        // 左右轉向 y 高度保持不變

        Vector3 v = MainCamera.transform.position + MainCamera.transform.forward * 20 - transform.position;
        v.Normalize();

        transform.forward = new Vector3(Mathf.Lerp(transform.forward.x, v.x, Time.deltaTime * speed), transform.forward.y, Mathf.Lerp(transform.forward.z, v.z, Time.deltaTime * speed));
    }

}
