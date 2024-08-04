using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject bullet; // 子彈物件
    public GameObject muzzle;
    public GameManage gameManage;

    float _ReloadTime;
    

    private void Start()
    {
        _ReloadTime = 2.5f;
    }

    void Update()
    {
        if(!gameManage.IsOpenMenu)
        {
            Shooting(); // 左鍵射擊
            Reloading();
        }
        
    }

    void Shooting()
    {
        if (PlayerSetting.Instance.BulletEnegy >= _ReloadTime && Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(bullet, muzzle.transform.position + muzzle.transform.forward * 5.5f , Quaternion.Euler(muzzle.transform.eulerAngles)) as GameObject;
            go.transform.LookAt(PlayerRotation.targetPoint); // 動用到 PlayerRotation 變數

            PlayerSetting.Instance.BulletEnegy = 0; // 裝填


        }
    }

    void Reloading()
    {

        if(PlayerSetting.Instance.BulletEnegy < _ReloadTime)
        {
            PlayerSetting.Instance.BulletEnegy += Time.deltaTime;
        }
        
        
    }
}
