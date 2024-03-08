using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCamera : MonoBehaviour
{
    public GameObject Obj;
    public GameObject turret;
    Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - Obj.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Obj.transform.position + _offset;
        transform.localEulerAngles = new Vector3(90, 0, -turret.transform.eulerAngles.y);

        
    }

    

}
