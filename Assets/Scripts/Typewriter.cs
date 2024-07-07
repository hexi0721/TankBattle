using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class TypewriterMessage
{
    float _timer = 0;
    [SerializeField] int _charIndex = 0;
    float _timePerChar = 0.05f;

    [SerializeField] string _currentMsg = null;
    string _displayMsg = null;

    bool _done = false;

    public string FullMsg
    {
        get => _currentMsg;
        set => _currentMsg = value;
    }

    public string DisplayMsg
    {
        get => _displayMsg;
    }

    public int CharIndex
    {
        get => _charIndex; 
        set => _charIndex = value;
    }

    public bool Done
    {
        get => _done;
        set => _done = value;
    }

    [SerializeField] int _msgIndex = 0;

    public TypewriterMessage(string _msg)
    {
        _currentMsg = _msg;
    }
    public void Update()
    {
        

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _charIndex += 1;
            _timer += _timePerChar;

            _displayMsg = _currentMsg.Substring(0, _charIndex);
            //TextComponent.text = _displayMsg;
        }

        if (_charIndex >= _currentMsg.Length)
        {
            _done = true;
        }
    }

    public bool IsActive()
    {
        //Debug.Log(_charIndex < _currentMsg.Length);
        return _charIndex < _currentMsg.Length;
    }
}


public class Typewriter : MonoBehaviour
{
    static Typewriter _instance;

    public TMP_Text TextComponent;

    
    [SerializeField] TypewriterMessage _currentMsg;
    [SerializeField] int _msgIndex = 0;
    

    List<TypewriterMessage> _messages;

    //bool _done = false;

    private void Awake()
    {
        _instance = this;

        _messages = new List<TypewriterMessage>();
        //_messages.Add("�P�ͭx���Q��}�]�����A�A�P�p�������b��a�y�@���A���L�ھڥX�h���d�������^�Ӷ׳��A��F��s�ߤW�o�{���y�ĭx�Z�J�u�t�C\r\n\r\n");
        //_messages.Add("�褣�e�w���ڡA�A���N�o�h�����W���̰ܳ��������A�ӬO�L�Q���t�v��p����ŧ�A�Y���\�h�N���e�u�ͭx�w���e�j���O�A�Y�K���Ѥ]��@������A�u�O�A���p���N�W�ۭ��﷽���������ĭx�C\r\n");
        //_msgIndex = 0;
        //_currentMsg = _messages[_msgIndex];
    }


    private void Update()
    {
        if (_msgIndex >= _messages.Count)
        {
            return;
        }

        if (_currentMsg.Done)
        {
            _currentMsg.Done = false;
            _msgIndex++;
            if (_msgIndex >= _messages.Count)
            {
                return;
            }

            _currentMsg.FullMsg += _messages[_msgIndex];

        }

        _currentMsg.Update();
        TextComponent.text = _currentMsg.FullMsg;

    }



    public void WriteNextMessageInQueue()
    {

        if(_currentMsg.IsActive())
        {
            TextComponent.text = _currentMsg.FullMsg;
            _currentMsg.CharIndex = _currentMsg.FullMsg.Length;

            
            _currentMsg.Done = true;
        }

    }
    
    public static void Add(string msg)
    {
        TypewriterMessage _typeMsg = new TypewriterMessage(msg);
        _instance._messages.Add(_typeMsg);
    }
    
    public static void Activate()
    {
        _instance._currentMsg = _instance._messages[0];
    }

    
}
