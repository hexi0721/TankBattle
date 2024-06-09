using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour
{
    public BoxCollider StartPosAirWall;
    public Image AimImage;
    [SerializeField]float _stay = 2f;

    private void Start()
    {
        AimImage.enabled = false;
        StartPosAirWall.enabled = false;
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
                AimImage.enabled = true;
            }
        }
    }
}
