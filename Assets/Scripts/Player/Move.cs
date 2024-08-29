using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] float ph , speed;
    [SerializeField] bool _canForward, _canBackward;
    public GameManage gameManage;
    public GameObject shell;
    
    Rigidbody rb;

    private void Start()
    {
        speed = 0f;
        _canForward = false;
        _canBackward = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(shell.transform.position, shell.transform.forward, out hit, 4, 1 << 14 | 1 << 11 | 1 << 7))
        {
            if (hit.collider != null)
            {
                _canForward = false;
            }
        }
        else
        {
            _canForward = true;
        }

        if (Physics.Raycast(shell.transform.position, -shell.transform.forward, out hit, 4, 1 << 14))
        {
            if (hit.collider != null)
            {
                _canBackward = false;
            }
        }
        else
        {
            _canBackward = true;
        }
    }

    private void FixedUpdate()
    {

        if (!gameManage.IsOpenMenu)
        {
            speed = Mathf.Lerp(speed, 0, Time.fixedDeltaTime * 5f);
            

            if (Input.GetKey(KeyCode.W) && _canForward || Input.GetKey(KeyCode.S) && _canBackward)
            {
                
                speed += Input.GetAxis("Vertical");
                
            }

            speed = Mathf.Clamp(speed , -4f , 4f);

            rb.MovePosition(rb.position + shell.transform.TransformDirection(speed * Vector3.forward * Time.fixedDeltaTime));

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


}
