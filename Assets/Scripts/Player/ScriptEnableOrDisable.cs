using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnableOrDisable : MonoBehaviour
{

    public GameManage gameManage;

    private void Update()
    {
        GetComponent<PlayerRotation>().enabled = gameManage.CanPlayerAction() ? true : false;



    }






}
