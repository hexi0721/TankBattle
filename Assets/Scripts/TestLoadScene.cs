using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoadScene : MonoBehaviour
{
    int _SceneNum = 1;

    public void TESTLOAD()
    {
        PlayerPrefs.SetInt("SceneNum", _SceneNum);

        SceneManager.LoadScene(_SceneNum);
    }
}
