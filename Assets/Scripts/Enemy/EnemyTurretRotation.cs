using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTurretRotation : MonoBehaviour
{
    public GameObject body;
    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // ¸j©w¼Æ­È
        transform.localPosition = body.transform.localPosition + body.transform.up * 1.1f;
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

    }

    public void LookPlayer()
    {

        Vector3 relativePos = Player.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation , rotation , Time.deltaTime);
            
        
    }

    public void PatrolStat()
    {

        transform.forward = Vector3.Lerp(transform.forward, body.transform.forward, Time.deltaTime);

    }
}
