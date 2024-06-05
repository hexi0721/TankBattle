using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTankActionScript : MonoBehaviour
{
    public GameManage gameManage;
    public Behaviour component;

    private void Update()
    {
        if (gameManage.IsOpenMenu)
        {
            component.enabled = false;
        }
        else
        {
            component.enabled = !PlayerSetting.Instance.animator.enabled ? true : false;
        }

        


    }


}
