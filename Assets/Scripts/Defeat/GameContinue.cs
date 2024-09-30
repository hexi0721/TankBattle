using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameContinue : MonoBehaviour
{

    public Image LoadingBar;
    AsyncOperation _async;
    bool _doNextScene;

    [SerializeField] bool _fadeIn;
    [SerializeField] bool _fadeOut;
    TMP_Text _Continue;

    private void Start()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _doNextScene = false;
        LoadSceneAndGameContinue();

        _Continue = GameObject.Find("Continue").GetComponent<TMP_Text>();
        _Continue.color = new Color(255, 255, 255, 0);
        _fadeIn = true;
        _fadeOut = false;
    }

    private void Update()
    {
        
        if (_async != null)
        {
            LoadingBar.fillAmount = _async.progress / 0.9f;
            if (_async.progress >= 0.9f)
            {
                FadeIn();
                FadeOut();
                _doNextScene = true;
            }
        }

    }


    private void LoadSceneAndGameContinue()
    {
        // Åª¨ú¶i«×
        //int SceneNum = PlayerPrefs.GetInt("SceneNum");
        
        _async = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("SceneNum"));
        _async.allowSceneActivation = false;

        // Debug.Log(PlayerPrefs.GetInt("SceneNum"));
    }

    public void DoNextScene()
    {
        if (_doNextScene)
        {
            _async.allowSceneActivation = true;
        }
        
    }

    void FadeIn()
    {
        if (_fadeIn)
        {
            if (_Continue.color.a < 1.0f)
            {
                float Alpha = _Continue.color.a + (Time.deltaTime);
                _Continue.color = new Color(255, 255, 255, Alpha);

                if (_Continue.color.a >= 1.0f)
                {
                    _fadeIn = false;
                    _fadeOut = true;
                }
            }
        }
    }

    void FadeOut()
    {
        if (_fadeOut)
        {
            if (_Continue.color.a > 0.0f)
            {
                float Alpha = _Continue.color.a - (Time.deltaTime);
                _Continue.color = new Color(255, 255, 255, Alpha);

                if (_Continue.color.a <= 0.0f)
                {
                    _fadeOut = false;
                    _fadeIn = true;
                }
            }
        }
    }
}
