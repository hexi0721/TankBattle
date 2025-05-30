using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour
{
    public BoxCollider StartPosAirWall;
    
    float _stay;

    private void Start()
    {
        StartPosAirWall.enabled = false;
        _stay = 2f;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {

            StartPosAirWall.enabled = true;
            
            _stay -= Time.deltaTime;
            if( _stay <= 0 )
            {
                PlayerSetting.Instance.animator.enabled = false;
                Destroy(gameObject);
            }
        }
    }
}
