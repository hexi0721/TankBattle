using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public AudioClip sExplosion; // Ãz¬µ­µ®Ä
    public GameObject Explosion; // Ãz¬µ²É¤l
    public GameObject Flame; // ¿U¿N²É¤l
    public GameObject BigExplode; // ¤jÃz¬µ

    public Material material; // ªo±í³QÃz¬µ¯À§÷

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

        if(_time >= 5.0f)
        {
            Destroy(this.gameObject);
        }

        



    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        Instantiate(Explosion, collision.contacts[0].point, Quaternion.identity);


        switch (collision.transform.tag)
        {
            case "barrel":

                
                if (collision.transform.childCount != 0)
                {
                    //Debug.Log(collision.gameObject.GetComponent<Renderer>().material, material);
                    collision.gameObject.GetComponent<Renderer>().material = material;
                    Destroy(collision.transform.GetChild(0).gameObject);
                    Instantiate(Flame, collision.transform.position, Quaternion.identity);
                    Instantiate(BigExplode, collision.contacts[0].point, Quaternion.identity);
                    PlayerSetting.Instance._Ishit = true;
                }

                
                

                //collision.collider.GetComponent<Collider>().excludeLayers += 1 << 8;


                break;



            case "EnemyTank":

                PlayerSetting.Instance._Ishit = true;


                break;

            case "EnemyFactory":

                PlayerSetting.Instance._Ishit = true;


                break;


        }
    }




}
