using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    public GameObject SettingBtn;

    public void Setting()
    {
        SettingBtn.SetActive(!SettingBtn.activeSelf);
    }


}
