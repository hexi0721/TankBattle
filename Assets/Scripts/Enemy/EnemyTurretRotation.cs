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

    void Update()
    {
        // 綁定數值
        transform.localPosition = body.transform.localPosition + body.transform.up * 1.1f;
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

    }

    public void LookPlayer()
    {

        Vector3 v = _Player.transform.position - transform.position;
        v.Normalize();
        

        Quaternion rotation = Quaternion.LookRotation(v);

        // Turret 左右
        transform.rotation = Quaternion.Lerp(transform.rotation , Quaternion.Euler(transform.rotation.eulerAngles.x , rotation.eulerAngles.y , transform.rotation.eulerAngles.z) , Time.deltaTime);


        // Muzzle 上下
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
