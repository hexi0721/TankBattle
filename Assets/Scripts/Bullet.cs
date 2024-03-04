using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip sExplosion; // Ãz¬µ­µ®Ä
    public GameObject Explosion; // Ãz¬µ²É¤l
    public GameObject Flame; // ¿U¿N²É¤l
    public Material material; // ªo±í³QÃz¬µ¯À§÷

    public float tmp;

    private void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * tmp);
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
                    Instantiate(Flame, other.transform.position, Quaternion.identity);
                }

                

                

                



                break;

            

        }
    }


}
