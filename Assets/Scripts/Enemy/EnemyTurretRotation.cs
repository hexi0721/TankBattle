using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class EnemyTurretRotation : MonoBehaviour
{
    
    public GameObject body;
    GameObject _Player;

    [SerializeField]float _clamp;

    private void Start()
    {
        _Player = GameObject.FindWithTag("Player");
    }

    public void LookPlayer()
    {

        Vector3 v = _Player.transform.position - transform.position;
        
        Quaternion rotation = Quaternion.LookRotation(v.normalized);

        // Turret ¥ª¥k
        transform.rotation = Quaternion.Lerp(transform.rotation , Quaternion.Euler(transform.rotation.eulerAngles.x , rotation.eulerAngles.y , transform.rotation.eulerAngles.z) , Time.deltaTime);


        // Muzzle ¤W¤U
        if (rotation.eulerAngles.x > 3.0f && rotation.eulerAngles.x < 180f)
        {
            _clamp = 3.0f;
        }
        else if (rotation.eulerAngles.x > 180f && rotation.eulerAngles.x < 352f)
        {
            _clamp = -8.0f;
        }
        else
        {
            _clamp = rotation.eulerAngles.x;
        }
        
        transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).transform.rotation, Quaternion.Euler(_clamp, transform.GetChild(0).eulerAngles.y, transform.GetChild(0).eulerAngles.z), 2 * Time.deltaTime);

    }

    public void PatrolStat()
    {

        transform.forward = Vector3.Lerp(transform.forward, body.transform.forward, Time.deltaTime);

    }
}
