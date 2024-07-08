using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypewriterMessage
{
    float _timer = 0;
    int _charIndex = 0;
    public int CharIndex
    {
        get => _charIndex;
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

            //TextComponent.text = _fullMsg.Substring(0, _charIndex);
        }
    }
    
}

public class Typewriter1 : MonoBehaviour
{
    static Typewriter1 _instance;

    public TMP_Text TextComponent;

    float _timer;
    [SerializeField] int _charIndex;
    public int tmpIndex=0;
    float _timePerChar;

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
        _timer = 0;
        _timePerChar = 0.05f;
    }

    private void Update()
    {

        if (_charIndex >= _fullMsg.Length) // ��@�ӥy�l������ ���U�@�ӥy�l
        {
            
            if (_msgIndex >= _messages.Count) // ��Ҧ��y�l�w������ ���n�A����}��
            {
                return;
            }

            _fullMsg += _messages[_msgIndex].CurrentMsg; // �q���y�}�l
            _msgIndex++; // _msg�Ǹ�+1
        }

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _charIndex += 1;
            _timer += _timePerChar;

            TextComponent.text = _fullMsg.Substring(0, _charIndex);
        }
        // �յ۩�itypewritemessage
        int a = _msgIndex - 1;
        tmpIndex += _messages[a].CharIndex;

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