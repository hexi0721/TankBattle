using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject MapCam; // ¶aπœ√Ë¿Y
    public Canvas Map;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MapCam.SetActive(!MapCam.activeSelf);
            Map.gameObject.SetActive(!Map.isActiveAndEnabled);
        }
    }
}
