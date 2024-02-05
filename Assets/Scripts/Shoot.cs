using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{


    public GameObject bullet; // ¤l¼uª«¥ó

    float _ReloadTime;

    private void Start()
    {
        _ReloadTime = 0f;
    }

    void Update()
    {

        Shooting(); // ¥ªÁä®gÀ»
        
        


    }

    private void Shooting()
    {
        if (_ReloadTime < 0 && Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(bullet) as GameObject;

            go.transform.SetParent(transform);
            go.transform.localPosition = new Vector3(0.053f, -0.046f, 5.05f);
            go.transform.localRotation = Quaternion.Euler(Vector3.zero);

            _ReloadTime = 2.5f;
        }

        Reloading(); 
    }

    private void Reloading()
    {
        _ReloadTime -= Time.deltaTime;
    }
}
