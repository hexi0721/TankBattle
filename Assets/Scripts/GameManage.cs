using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject MapCam; // �a�����Y


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MapCam.SetActive(!MapCam.activeSelf);
        }
    }
}
