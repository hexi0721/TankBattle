using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float ph;

    public float speed;
    public GameManage gameManage;
    public GameObject shell;
    
    Rigidbody rb;

    float _clampMin = -5f;
    float _clampMax = 5f;
    Vector3 _rotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _rotation = transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        /*
        Vector3 angle = transform.eulerAngles;
        float x = angle.x;
        float z = angle.z;
        if (Vector3.Dot(transform.up, Vector3.up) >= 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = angle.x - 360f;
            }
        }
        if (Vector3.Dot(transform.up, Vector3.up) < 0f)
        {
            if (angle.x >= 0f && angle.x <= 90f)
            {
                x = 180 - angle.x;
            }
            if (angle.x >= 270f && angle.x <= 360f)
            {
                x = 180 - angle.x;
            }
        }

        if (Vector3.Dot(transform.forward, Vector3.forward) >= 0f)
        {
            if (angle.z >= 0f && angle.z <= 90f)
            {
                z = angle.z;
            }
            if (angle.z >= 270f && angle.z <= 360f)
            {
                z = angle.z - 360f;
            }
        }
        if (Vector3.Dot(transform.forward, Vector3.forward) < 0f)
        {
            if (angle.z >= 0f && angle.z <= 90f)
            {
                z = 180 - angle.z;
            }
            if (angle.z >= 270f && angle.z <= 360f)
            {
                z = 180 - angle.z;
            }
        }*/


        //transform.eulerAngles = new Vector3(Mathf.Clamp(_rotation.x, _clampMin, _clampMax), transform.eulerAngles.y, Mathf.Clamp(_rotation.z, _clampMin, _clampMax));

        if (!gameManage.IsOpenMenu)
        {
            
            if (rb.velocity.magnitude < 10.0f)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(speed * Time.fixedDeltaTime * shell.transform.forward);
                    
                }


                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(speed * Time.fixedDeltaTime * -shell.transform.forward);
                    
                }
            }


            if (Input.GetKey(KeyCode.A))
            {
                shell.transform.RotateAround(shell.transform.position, transform.up, -ph * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                shell.transform.RotateAround(shell.transform.position, transform.up, ph * Time.deltaTime);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.transform.tag)
        {
            case "Terrain":

                rb.velocity -= Vector3.one * Time.fixedDeltaTime;

                break;

        }
        
    }



}
