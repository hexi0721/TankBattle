using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject bullet; // �l�u����

    float _ReloadTime;

    private void Start()
    {
        _ReloadTime = 2.5f;
    }

    void Update()
    {

        Shooting(); // ����g��

    }

    private void Shooting()
    {
        if (PlayerSetting.Instance.BulletEnegy >= _ReloadTime && Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(bullet, transform.position + transform.forward * 5 , Quaternion.Euler(transform.eulerAngles.x , transform.eulerAngles.y , transform.eulerAngles.z)) as GameObject;
            go.transform.LookAt(PlayerRotation.targetPoint); // �ʥΨ� PlayerRotation �ܼ�

            PlayerSetting.Instance.BulletEnegy = 0; // �˶�


        }

        Reloading(); 
    }

    private void Reloading()
    {

        if(PlayerSetting.Instance.BulletEnegy < _ReloadTime)
        {
            PlayerSetting.Instance.BulletEnegy += Time.deltaTime;
        }
        
        
    }
}
