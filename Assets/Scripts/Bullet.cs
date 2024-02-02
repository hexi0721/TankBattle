using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip sExplosion; // �z������
    public GameObject Explosion; // �z���ɤl
    public GameObject Flame; // �U�N�ɤl
    public Material material; // �o��Q�z������

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        


        Instantiate(Explosion , transform.position , Quaternion.identity);
        

        switch (other.transform.tag)
        {
            case "barrel":

                other.GetComponent<Renderer>().material = material;


                GameObject go = Instantiate(Flame, other.transform.position, Quaternion.identity) as GameObject;

                



                break;

            

        }
    }


}
