using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : MonoBehaviour
{
    public GameObject SettingBtn;



    public void ReturnGame()
    {
        GameManage.OpenCloseMenu();
    }

    public void Setting()
    {
        SettingBtn.SetActive(!SettingBtn.activeSelf);
    }

    public void ReturnMainMenu()
    {

        PlayerPrefs.SetInt("SceneNum", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }


}
