using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float pv;
    [SerializeField] float ph;

    float _v , _h;

    

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.parent.GetComponent<Rigidbody>().MovePosition(transform.parent.position + transform.forward * _v * pv * Time.deltaTime);
            
        }


        if (Input.GetKey(KeyCode.S))
        {
            transform.parent.GetComponent<Rigidbody>().MovePosition(transform.parent.position + transform.forward * _v * pv * Time.deltaTime);
        }
    }

    void Update()
    {
        
        _v = Input.GetAxis("Vertical");
        _h = Input.GetAxis("Horizontal");
        

        


        if (Input.GetKey(KeyCode.A))
        {
            
            

            transform.RotateAround(transform.position, Vector3.up, _h * ph * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {

            
            transform.RotateAround(transform.position, Vector3.up, _h * ph * Time.deltaTime);
        }

        



    }
}
