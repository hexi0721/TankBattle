using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTankActionScript : MonoBehaviour
{
    public Behaviour component;

    private void Update()
    {

        component.enabled = !PlayerSetting.Instance.animator.enabled ? true : false;



    }


}
