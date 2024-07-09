using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
/*
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
    string _currentMsg;
    public string CurrentMsg
    {
        get => _currentMsg;
    }

    public TypewriterMessage(string msg)
    {
        _currentMsg = msg;
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



public class Typewriter : MonoBehaviour
{
    static Typewriter _instance;

    public TMP_Text TextComponent;

    [SerializeField] int _charIndex;

    [SerializeField] string _fullMsg;
    [SerializeField] int _msgIndex;

    List<TypewriterMessage> _messages = new List<TypewriterMessage>();

    private void Awake()
    {
        _instance = this;

    }

    private void Start()
    {
        _charIndex = 0;

    }

    private void Update()
    {
        if (_msgIndex >= _messages.Count) // ��Ҧ��y�l�w������ ���n�A����}��
        {
            return;
        }

        if (_charIndex >= _fullMsg.Length) // ��@�ӥy�l������ �N�U�@�ӥy�l�[�i��
        {
            _msgIndex++;
            if (_msgIndex >= _messages.Count) // ��Ҧ��y�l�w������ ���n�A����}��
            {
                return;
            }

            _fullMsg += _messages[_msgIndex].CurrentMsg;
            _messages[_msgIndex].CharIndex = _charIndex; // �N�W�@�ӥy�l�r���ƶq�~�Ө즹�y

        }

        _messages[_msgIndex].Update();
        TextComponent.text = _fullMsg.Substring(0, _messages[_msgIndex].CharIndex);
        _charIndex = _messages[_msgIndex].CharIndex;


    }

    bool IsActive()
    {
        return _charIndex < _fullMsg.Length;
    }

    public void WriteNextMessageInQueue()
    {

        if (IsActive())
        {
            TextComponent.text = _fullMsg;
            _charIndex = _fullMsg.Length;
        }

    }

    public static void Add(string msg)
    {
        TypewriterMessage type = new TypewriterMessage(msg);
        _instance._messages.Add(type);
    }

    public static void Activate()
    {
        _instance._msgIndex = 0;
        _instance._fullMsg = _instance._messages[_instance._msgIndex].CurrentMsg;
    }
}
}*/