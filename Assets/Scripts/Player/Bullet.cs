using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip sExplosion; // �z������
    public GameObject Explosion; // �z���ɤl
    public GameObject Flame; // �U�N�ɤl
    public Material material; // �o��Q�z������

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

        if(_time > 5.0f)
        {
            Destroy(this.gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        Instantiate(Explosion, transform.position, Quaternion.identity);


        switch (collision.transform.tag)
        {
            case "barrel":

                collision.gameObject.GetComponent<Renderer>().material = material;

                if (collision.transform.childCount != 0)
                {
                    Destroy(collision.transform.GetChild(0).gameObject);
                    Instantiate(Flame, collision.transform.position, Quaternion.identity);
                }









                break;



        }
    }

    


}
