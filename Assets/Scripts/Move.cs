using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float pv;
    [SerializeField] float ph;


    void Start()
    {
        
    }

    
    void Update()
    {

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(0 , 0 , v) * pv * Time.deltaTime );
        transform.RotateAround(transform.position , Vector3.up , h * ph * Time.deltaTime);


    }
}
