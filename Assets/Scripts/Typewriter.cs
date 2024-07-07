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
        //_messages.Add("與友軍順利突破包圍網後，你與小隊成員在原地稍作休整，不過根據出去偵查的成員回來匯報，於東方山脈上發現有座敵軍坦克工廠。\r\n\r\n");
        //_messages.Add("刻不容緩之際，你未將這層消息上報至最高指揮部，而是兵貴神速率領小隊突襲，若成功則將為前線友軍緩解龐大壓力，即便失敗也能作為誘餌，只是你的小隊將獨自面對源源不絕的敵軍。\r\n");
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
