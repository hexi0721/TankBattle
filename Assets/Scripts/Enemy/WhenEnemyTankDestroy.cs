using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenEnemyTankDestroy : MonoBehaviour
{

    bool _isDestroy;
    public bool IsDestroy
    {
        get => _isDestroy;
        set => _isDestroy = value;
    }

    EnemyTank enemyTank;
    public List<GameObject> listBodyChild;

    float _disappearTime;


    private void Start()
    {
        IsDestroy = false;
        _disappearTime = 15f;

        enemyTank = GetComponent<EnemyTank>();

        listBodyChild.Add(enemyTank.Body.gameObject);
        for (int i = 0;i < enemyTank.Body.transform.childCount; i++)
        {
            listBodyChild.Add(enemyTank.Body.transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {

        if (IsDestroy) // 將玩家 其他坦克 子彈碰撞取消
        {

            foreach (GameObject child in listBodyChild) 
            {
                MeshCollider MC = child.GetComponent<MeshCollider>();
                MC.excludeLayers = (int)Mathf.Pow(2, 3) + (int)Mathf.Pow(2, 8) + (int)Mathf.Pow(2, 9) + (int)Mathf.Pow(2, 10) + (int)Mathf.Pow(2, 11) + (int)Mathf.Pow(2, 12);
            }

            _disappearTime -= Time.deltaTime;
            if(_disappearTime < 0 )
            {
                Destroy(gameObject);
            }
        }



    }


}
