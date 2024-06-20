using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenEnemyTankDestroy : MonoBehaviour
{

    public bool IsDestroy;
    public List<GameObject> TankBodyChild;
    

    private void Start()
    {
        IsDestroy = false;
    }

    private void Update()
    {

        if (IsDestroy) // 將玩家 其他坦克 子彈碰撞取消
        {

            foreach (GameObject child in TankBodyChild) 
            {
                
                MeshCollider MC = child.GetComponent<MeshCollider>();

                MC.excludeLayers += 1 << 3;
                MC.excludeLayers += 1 << 9;
                MC.excludeLayers += 1 << 8;
                MC.excludeLayers += 1 << 10;
                MC.excludeLayers += 1 << 11;
                MC.excludeLayers += 1 << 12;

            }

            this.enabled = false;

        }



    }


}
