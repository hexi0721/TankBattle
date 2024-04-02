using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyShoot : MonoBehaviour
{

    public GameObject EnemyBullet; // �l�u����
    EnemyTank _EnemyTankScript;

    float _ReloadTime;

    private void Start()
    {
        _ReloadTime = 5f;

        _EnemyTankScript = GetComponent<EnemyTank>();
    }

    public void Shooting(float BulletEnegy , Vector3 TargetPoint)
    {
        if (BulletEnegy >= _ReloadTime)
        {
            GameObject go = Instantiate(EnemyBullet, 
                _EnemyTankScript.Muzzle.transform.position + _EnemyTankScript.Muzzle.transform.forward * 5, 
                Quaternion.Euler(_EnemyTankScript.Muzzle.transform.eulerAngles.x, _EnemyTankScript.Muzzle.transform.eulerAngles.y, _EnemyTankScript.Muzzle.transform.eulerAngles.z)) as GameObject;
            go.transform.LookAt(TargetPoint);

            _EnemyTankScript.BulletEnegy = 0; // �˶�

        }
    }

    public void Reloading(float BulletEnegy)
    {

        if (BulletEnegy < _ReloadTime)
        {
            _EnemyTankScript.BulletEnegy += Time.deltaTime;
        }

    }


}
