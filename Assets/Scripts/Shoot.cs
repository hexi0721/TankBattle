using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    

    public GameObject bullet; // 子彈物件

    float _ReloadTime;

    private void Start()
    {
        _ReloadTime = 2.5f;
    }

    void Update()
    {

        Shooting(); // 左鍵射擊
        
        


    }

    private void Shooting()
    {
        if (PlayerSetting.Instance.BulletEnegy >= _ReloadTime && Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, transform.position + transform.forward * 10 , Quaternion.Euler(transform.eulerAngles.x , transform.eulerAngles.y , transform.eulerAngles.z));

            


            PlayerSetting.Instance.BulletEnegy = 0; // 裝填

            



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
