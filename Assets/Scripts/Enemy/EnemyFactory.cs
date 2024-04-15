using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFactory : MonoBehaviour
{
    public Image FacHp;

    float MaxHp = 50f;
    public float Hp = 50f;


    private void Update()
    {
        FacHp.fillAmount = Hp / MaxHp;
    }

    public float GetMaxHp()
    {
        return MaxHp;
    }

}
