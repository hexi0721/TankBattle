using UnityEngine;

public class DisableTankActionScript : MonoBehaviour
{
    public GameManage gameManage;
    public Behaviour Move , Shoot , Rotation;

    Vector3 _postion;
    Quaternion _rotation;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * 350); // ­«¤O ¤U­°
    }

    private void Update()
    {

        if (gameManage.IsOpenMenu || PlayerSetting.Instance.animator.enabled)
        {
            Move.enabled = false;
            Shoot.enabled = false;
            Rotation.enabled = false;

            transform.position = _postion;
            transform.rotation = _rotation;

        }
        else
        {
            Move.enabled =  true;
            Shoot.enabled = true;
            Rotation.enabled = true;

            _postion = transform.position;
            _rotation = transform.rotation;
        }

    }


}
