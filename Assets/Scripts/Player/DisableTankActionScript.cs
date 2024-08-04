using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTankActionScript : MonoBehaviour
{
    public GameManage gameManage;
    public Behaviour Move , Shoot , Rotation;

    private void Update()
    {
        if (gameManage.IsOpenMenu)
        {
            Move.enabled = false;
            Shoot.enabled = false;
            Rotation.enabled = false;
        }
        else
        {
            Move.enabled = !PlayerSetting.Instance.animator.enabled ? true : false;
            Shoot.enabled = !PlayerSetting.Instance.animator.enabled ? true : false;
            Rotation.enabled = !PlayerSetting.Instance.animator.enabled ? true : false;
        }

        


    }


}
