using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject MapCam; // ¶aπœ√Ë¿Y


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MapCam.SetActive(!MapCam.activeSelf);
        }
    }
}
