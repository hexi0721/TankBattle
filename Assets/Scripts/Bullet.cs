using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip sExplosion; // �z������
    public GameObject Explosion; // �z���ɤl
    public GameObject Flame; // �U�N�ɤl
    public Material material; // �o��Q�z������

    public float tmp;

    private void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * tmp);
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
