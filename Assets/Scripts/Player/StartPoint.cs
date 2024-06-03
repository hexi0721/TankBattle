using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    float _stay = 2f;
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            _stay -= Time.deltaTime;
            if( _stay <= 0 )
            {
                PlayerSetting.Instance.animator.enabled = false;
            }
        }
    }
}
