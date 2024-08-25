using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float ph;

    public float speed;
    public GameManage gameManage;
    public GameObject shell;
    public GameObject trackTrigger1, trackTrigger2;

    Rigidbody rb;

    // public float tmp;
    [SerializeField] bool _canForward, _canBackward;

    private void Start()
    {
        speed = 0f;
        _canForward = false;
        _canBackward = false;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(-transform.up * 9.8f * 2.5f); // ­«¤O ¤U­°

        RaycastHit hit; 
        if(Physics.Raycast(shell.transform.position , shell.transform.forward , out hit, 8f ,  1 << 14))
        {
            if (hit.collider != null) 
            {
                _canForward = false;
            }
            Debug.DrawRay(shell.transform.position , shell.transform.forward * 8f, Color.cyan );
        }
        else
        {
            _canForward = true;
        }

        if (Physics.Raycast(shell.transform.position, -shell.transform.forward, out hit, 8f, 1 << 14))
        {
            if (hit.collider != null)
            {
                _canBackward = false;
            }
            Debug.DrawRay(shell.transform.position, -shell.transform.forward * 8f, Color.cyan);
        }
        else
        {
            _canBackward = true;
        }


        if (!gameManage.IsOpenMenu)
        {

            if (Input.GetKey(KeyCode.W) && _canForward)
            {

                speed += Input.GetAxis("Vertical");
                
            }


            if (Input.GetKey(KeyCode.S) && _canBackward)
            {

                speed += Input.GetAxis("Vertical");
            }

            speed = Mathf.Clamp(speed , -4f , 4f);

            rb.MovePosition(rb.position + shell.transform.TransformDirection(speed * Vector3.forward * Time.fixedDeltaTime));

            if (speed > 0)
            {
                speed -= Time.fixedDeltaTime;
            }
            else if (speed < 0)
            {
                speed += Time.fixedDeltaTime;
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
    /*
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.transform.tag)
        {
            case "Terrain":

                rb.velocity -= Vector3.one * Time.fixedDeltaTime;

                break;

        }
        
    }
    */


}
