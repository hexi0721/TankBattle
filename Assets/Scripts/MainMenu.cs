using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public GameObject setting;

    private void Start()
    {
        StackSettings._stack = new List<GameObject> ();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (StackSettings._stack.Count != 0)
            {

                StackSettings.PullStack();


            }
            else
            {
                
                StackSettings.AddStack(setting);
            }


        }
    }

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
        if (setting.activeSelf == true)
        {
            StackSettings.PullStack();
        }
        else
        {
            StackSettings.AddStack(setting);
        }

    }

    public void CloseTheGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }

}
