using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : MonoBehaviour
{
    GameObject setting;

    private void Start()
    {
        setting = GameObject.FindWithTag("Setting");
        setting.SetActive(false);

        gameObject.SetActive(false);
    }


    public void ReturnGame()
    {
        GameManage.OpenCloseMenu();
        StackSettings.PullStack();
    }

    public void Setting()
    {
        if (setting.activeSelf == true)
        {
            StackSettings.PullStack();
        }
        else
        {
            StackSettings.AddStack(setting);
        }
    }

    public void ReturnMainMenu()
    {

        PlayerPrefs.SetInt("SceneNum", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }

}
