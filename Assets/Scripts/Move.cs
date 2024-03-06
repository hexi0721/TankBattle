using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float pv;
    [SerializeField] float ph;

    
    
    void Update()
    {

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        //»Ý­×§ïmove
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.GetChild(0).gameObject.transform.forward * Time.deltaTime * pv;
        }


        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.GetChild(0).gameObject.transform.forward * Time.deltaTime * pv;
        }


        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(transform.position, Vector3.up, -ph * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(transform.position, Vector3.up, ph * Time.deltaTime);
        }





    }
}
