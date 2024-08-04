using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float ph;

    public float speed;
    public GameManage gameManage;
    public GameObject shell;
    //public GameObject track1, track2;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 angle = transform.eulerAngles;
        float x = angle.x;
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

        Debug.Log(x);


        if (!gameManage.IsOpenMenu)
        {
            
            if (rb.velocity.magnitude < 10.0f && x < 5f && x > -5f)// && transform.eulerAngles.z < 5f && transform.eulerAngles.z > -5f)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(speed * Time.fixedDeltaTime * shell.transform.forward);
                    // rb.AddForce(speed * Time.fixedDeltaTime * track2.transform.forward);
                }


                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(speed * Time.fixedDeltaTime * -shell.transform.forward);
                    //rb.AddForce(speed * Time.fixedDeltaTime * -track2.transform.forward);
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
