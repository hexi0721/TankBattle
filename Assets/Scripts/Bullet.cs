using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip sExplosion; // 爆炸音效
    public GameObject Explosion; // 爆炸粒子
    public GameObject Flame; // 燃燒粒子
    public Material material; // 油桶被爆炸素材

    Rigidbody _rb; // 本身剛體


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
