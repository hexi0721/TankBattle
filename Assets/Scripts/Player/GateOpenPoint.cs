using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenPoint : MonoBehaviour
{

    public GameObject Gate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Destroy(Gate);
            Destroy(this.gameObject);
        }
        
    }


}
