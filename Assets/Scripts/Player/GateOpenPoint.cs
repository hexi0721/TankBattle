using UnityEngine;

public class GateOpenPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
        }
        
    }


}
