using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTankActionScript : MonoBehaviour
{
    public GameManage gameManage;
    public Behaviour Move , Shoot , Rotation;

    private void Update()
    {


        if (gameManage.IsOpenMenu || PlayerSetting.Instance.animator.enabled)
        {
            Move.enabled = false;
            Shoot.enabled = false;
            Rotation.enabled = false;

        }
        else
        {
            Move.enabled =  true;
            Shoot.enabled = true;
            Rotation.enabled = true;
        }

    }


}
