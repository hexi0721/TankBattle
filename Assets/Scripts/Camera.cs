using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Vector3 offset;
    public GameObject Tank;

    void Start()
    {
        offset = transform.position - Tank.transform.position;
    }

    
    void  LateUpdate()
    {
        
        transform.rotation = Quaternion.Euler(new Vector3(11, Tank.transform.eulerAngles.y , 0));

        transform.position = Tank.transform.position + offset;

        
    }

    void Update()
    {
        
    }




}

