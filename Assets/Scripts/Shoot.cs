using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //public int tmp;

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

            go.transform.position = transform.position + transform.forward * 5;
            go.GetComponent<Rigidbody>().AddForce(transform.forward * 4000);
            go.transform.rotation = transform.rotation;
            

            _ReloadTime = 2.5f;
        }

        Reloading(); 
    }

    private void Reloading()
    {
        _ReloadTime -= Time.deltaTime;
    }
}
