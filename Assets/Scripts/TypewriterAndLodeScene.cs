using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MessageJson
{
    public string Message;
}
public class TypewriterJson
{
    public MessageJson[] Messages;
}

[System.Serializable]
public class TypewriterMessage
{

    Action onActionCallback = null;

    float _timer = 0;
    int _charIndex = 0;
    public int CharIndex
    {
        get => _charIndex;
        set => _charIndex = value;
    }
    float _timePerChar = 0.05f;

    [SerializeField]
    string _currentMsg;
    public string CurrentMsg()
    {
        return _currentMsg;
    }

    public TypewriterMessage(string msg, Action callback)
    {
        onActionCallback = callback;
        _currentMsg = msg;

    }

    public void Callback()
    {
        if (onActionCallback != null)
        {
            onActionCallback();
        }
    }

    public void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _charIndex += 1;
            _timer += _timePerChar;

        }

    }

}

public class TypewriterAndLodeScene : MonoBehaviour
{
    static TypewriterAndLodeScene _instance;

    public TMP_Text TextComponent;

    [SerializeField] int _charIndex;

    [SerializeField] string _fullMsg;
    [SerializeField] int _msgIndex;

    List<TypewriterMessage> _messages = new List<TypewriterMessage>();

    bool _doNextScene;
    [SerializeField] bool _fadeIn;
    [SerializeField] bool _fadeOut;
    TMP_Text _Continue;

    private void Awake()
    {
        _instance = this;

    }

    private void Start()
    {
        _charIndex = 0;
        _doNextScene = false;

        _Continue = GameObject.Find("Continue").GetComponent<TMP_Text>();
        _Continue.color = new Color(255,255,255,0);
        _fadeIn = true;
        _fadeOut = false;
    }

    private void Update()
    {
        


        if (_msgIndex >= _messages.Count) // 當所有句子已結束時 執行漸入漸出
        {
            float Alpha;

            if (_fadeIn)
            {

                if (_Continue.color.a < 1.0f)
                {
                    Alpha = _Continue.color.a + (Time.deltaTime);
                    _Continue.color = new Color(255, 255, 255, Alpha);

                    if (_Continue.color.a >= 1.0f)
                    {
                        _fadeIn = false;
                        FadeOut();
                    }
                }

            }

            if (_fadeOut)
            {

                if (_Continue.color.a > 0.0f)
                {
                    Alpha = _Continue.color.a - (Time.deltaTime);
                    _Continue.color = new Color(255, 255, 255, Alpha);

                    if (_Continue.color.a <= 0.0f)
                    {
                        _fadeOut = false;
                        FadeIn();
                    }
                }

            }


            return;
        }

        if (_charIndex >= _fullMsg.Length) // 當一個句子結束時 將下一個句子加進來
        {
            _messages[_msgIndex].Callback();
            _msgIndex++;
            if (_msgIndex >= _messages.Count) // 當所有句子已結束時 不要再執行
            {
                return;
            }

            _fullMsg += _messages[_msgIndex].CurrentMsg();
            _messages[_msgIndex].CharIndex = _charIndex; // 將上一個句子字元數量繼承到此句

        }

        _messages[_msgIndex].Update();
        TextComponent.text = _fullMsg.Substring(0, _messages[_msgIndex].CharIndex);
        _charIndex = _messages[_msgIndex].CharIndex;


    }

    bool IsActive()
    {
        return _charIndex < _fullMsg.Length;
    }

    bool LoadNextScene()
    {
        return _doNextScene;
    }

    void FadeIn()
    {
        _fadeIn = true;
    }

    void FadeOut()
    {
        _fadeOut = true;
    }

    public void WriteNextMessageInQueue()
    {

        if (IsActive())
        {
            TextComponent.text = _fullMsg;
            _charIndex = _fullMsg.Length;
        }
        else if (LoadNextScene())
        {
            SceneManager.LoadScene(1);
        }

    }

    public static void Add(string msg, Action callback = null)
    {
        TypewriterMessage type = new TypewriterMessage(msg, callback);
        _instance._messages.Add(type);
    }
    /*
    public static void Add(TypewriterScriptableObj SourceObj)
    {
        TypewriterMessage type;
        for (int i = 0;i < SourceObj.Messages.Count;i++) 
        {
            
            if (i !=  SourceObj.Messages.Count-1)
            {
                type = new TypewriterMessage(SourceObj.Messages[i].CurrentMsg() + "\r\n", () => { Debug.Log("CallBack"); });
                
            }
            else
            {
                type = new TypewriterMessage(SourceObj.Messages[i].CurrentMsg() , () => { Debug.Log("CallBack"); });
                
            }
            _instance._messages.Add(type);
        }
    }
    */
    public static void Add(TypewriterJson json)
    {
        TypewriterMessage type;
        for (int i = 0; i < json.Messages.Length; i++)
        {

            if(i != json.Messages.Length - 1)
            {
                type = new TypewriterMessage(json.Messages[i].Message + "\r\n", null);
            }
            else
            {
                type = new TypewriterMessage(json.Messages[i].Message , () => { _instance._doNextScene = true; });
            }

            
            _instance._messages.Add(type);
        }
    }

    public static void Activate()
    {
        _instance._msgIndex = 0;
        _instance._fullMsg = _instance._messages[_instance._msgIndex].CurrentMsg();
    }
}