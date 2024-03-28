using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    public GameObject EnemyBullet; // 子彈物件

    float _ReloadTime;

    private void Start()
    {
        _ReloadTime = 5f;
    }

    void Update()
    {
        Debug.Log(EnemyTank.Instance.BulletEnegy);
        
        Reloading();
        
    }

    public void Shooting()
    {
        if (EnemyTank.Instance.BulletEnegy >= _ReloadTime)
        {
            GameObject go = Instantiate(EnemyBullet, transform.position + transform.forward * 10, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)) as GameObject;


            EnemyTank.Instance.BulletEnegy = 0; // 裝填


        }

        
    }

    private void Reloading()
    {

        if (EnemyTank.Instance.BulletEnegy < _ReloadTime)
        {
            EnemyTank.Instance.BulletEnegy += Time.deltaTime;
        }


    }




}
