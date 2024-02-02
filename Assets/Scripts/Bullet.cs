using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip sExplosion; // Ãz¬µ­µ®Ä
    public GameObject Explosion; // Ãz¬µ²É¤l
    public GameObject Flame; // ¿U¿N²É¤l
    public Material material; // ªo±í³QÃz¬µ¯À§÷

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
