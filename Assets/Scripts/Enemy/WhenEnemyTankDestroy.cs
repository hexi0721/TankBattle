using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenEnemyTankDestroy : MonoBehaviour
{

    public bool IsDestroy;
    public List<GameObject> TankBodyChild;

    float _disappearTime;


    private void Start()
    {
        IsDestroy = false;
        _disappearTime = 15f;
    }

    private void Update()
    {

        if (IsDestroy) // 將玩家 其他坦克 子彈碰撞取消
        {

            foreach (GameObject child in TankBodyChild) 
            {
                
                MeshCollider MC = child.GetComponent<MeshCollider>();

                MC.excludeLayers = (int)Mathf.Pow(2, 3) + (int)Mathf.Pow(2, 8) + (int)Mathf.Pow(2, 9) + (int)Mathf.Pow(2, 10) + (int)Mathf.Pow(2, 11) + (int)Mathf.Pow(2, 12);
                
            }

            //this.enabled = false;

            _disappearTime -= Time.deltaTime;
            if(_disappearTime < 0 )
            {
                Destroy(this.gameObject);
            }
        }



    }


}
