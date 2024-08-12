using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public AudioClip sExplosion; // Ãz¬µ­µ®Ä
    public GameObject Explosion; // Ãz¬µ²É¤l

    public float speed;
    float _time;

    private void Start()
    {
        _time = 0f;
    }

    private void FixedUpdate()
    {
        
        GetComponent<Rigidbody>().AddForce(transform.forward * speed , ForceMode.Impulse);
    }
    

    private void Update()
    {

        _time += Time.deltaTime;

        if (_time > 5.0f)
        {
            Destroy(gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(Explosion, transform.position, Quaternion.identity);

    }
}






