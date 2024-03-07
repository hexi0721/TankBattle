using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float pv;
    [SerializeField] float ph;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        

        if (Input.GetKey(KeyCode.W))
        {
            transform.parent.GetComponent<Rigidbody>().MovePosition(transform.parent.position + transform.forward * v * pv * Time.deltaTime);
            //transform.position += transform.forward * Time.deltaTime * pv;
        }


        if (Input.GetKey(KeyCode.S))
        {
            transform.parent.GetComponent<Rigidbody>().MovePosition(transform.parent.position + transform.forward * v * pv * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.A))
        {
            
            //rb.MoveRotation(transform.rotation * Quaternion.Euler(new Vector3(0, ph, 0) * h * Time.deltaTime));

            transform.RotateAround(transform.position, Vector3.up, -ph * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {

            //rb.MoveRotation(transform.rotation * Quaternion.Euler(new Vector3(0, ph, 0) * h * Time.deltaTime));
            transform.RotateAround(transform.position, Vector3.up, ph * Time.deltaTime);
        }

        



    }
}
