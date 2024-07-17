using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int _SceneNum = 1;
    public GameObject SettingBtn;

    public void GameStart()
    {
        PlayerPrefs.SetInt("SceneNum", _SceneNum);

        SceneManager.LoadScene(_SceneNum);
    }

    public void CloseTheGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }

    public void Setting()
    {
        SettingBtn.SetActive(!SettingBtn.activeSelf);
    }


}
