using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip sExplosion; // �z������
    public GameObject Explosion; // �z���ɤl
    public GameObject Flame; // �U�N�ɤl
    public Material material; // �o��Q�z������

    Rigidbody _rb; // ��������


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rb.AddRelativeForce(new Vector3(0, 0, 5.0f), ForceMode.Impulse);

        
    }




    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        

        Instantiate(Explosion , transform.position , Quaternion.identity);
        

        switch (other.transform.tag)
        {
            case "barrel":

                other.GetComponent<Renderer>().material = material;
                
                if(other.transform.childCount != 0)
                {
                    Destroy(other.transform.GetChild(0).gameObject);
                }

                

                GameObject go = Instantiate(Flame, other.transform.position, Quaternion.identity) as GameObject;

                



                break;

            

        }
    }


}
