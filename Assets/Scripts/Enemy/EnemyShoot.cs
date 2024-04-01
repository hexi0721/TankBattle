using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        RaycastHit hit;

        if(Physics.Raycast(transform.position , transform.forward , out hit , 80f , ~(1 << 11 | 1 << 12)))
        {

            if (hit.collider.CompareTag("Player"))
            {
                Shooting();
            }

        }

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

    public void Reloading()
    {

        if (EnemyTank.Instance.BulletEnegy < _ReloadTime)
        {
            EnemyTank.Instance.BulletEnegy += Time.deltaTime;
        }


    }




}
