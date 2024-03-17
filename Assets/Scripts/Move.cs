using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float pv;
    [SerializeField] float ph;

    public float speed;

    private void FixedUpdate()
    {
        
        if (transform.parent.GetComponent<Rigidbody>().velocity.magnitude < 10.0f)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.parent.GetComponent<Rigidbody>().AddForce(transform.forward * speed * Time.fixedDeltaTime);
            }


            if (Input.GetKey(KeyCode.S))
            {
                transform.parent.GetComponent<Rigidbody>().AddForce(-transform.forward * speed * Time.fixedDeltaTime);
            }
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


    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.transform.tag)
        {

            case "Terrain":

                transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;

                break;


        }



        
    }



}
