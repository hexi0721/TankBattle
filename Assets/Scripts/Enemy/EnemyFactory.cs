using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFactory : MonoBehaviour
{
    public Image FacHp;

    float _MaxHp;
    public float Hp;

    public float HpShowTime;

    private void Start()
    {
        _MaxHp = 50f;
        Hp = 50f;
        HpShowTime = 4f;

        FacHp.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {

        if(FacHp.transform.parent.gameObject.activeSelf)
        {
            HpShowTime -= Time.deltaTime;
            if(HpShowTime <= 0)
            {
                FacHp.transform.parent.gameObject.SetActive(false);
                
            }
        }

        FacHp.fillAmount = Hp / _MaxHp;
    }

    public float GetMaxHp()
    {
        return _MaxHp;
    }

}
