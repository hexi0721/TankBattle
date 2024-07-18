using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public GameObject SettingBtn;

    public void GameStart()
    {
        PlayerPrefs.SetInt("SceneNum", 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("SceneNum"));
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SceneNum"));
    }

    public void Setting()
    {
        SettingBtn.SetActive(!SettingBtn.activeSelf);
    }

    public void CloseTheGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }

}
