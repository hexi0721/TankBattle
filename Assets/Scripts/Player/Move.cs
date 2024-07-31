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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

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
                shell.transform.RotateAround(shell.transform.position, Vector3.up, -ph * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                shell.transform.RotateAround(shell.transform.position, Vector3.up, ph * Time.deltaTime);
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
