using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typewriter : MonoBehaviour
{
    static Typewriter _instance;

    public TMP_Text TextComponent;

    float _timer;
    int _charIndex;
    float _timePerChar;
    string _currentMsg;
    int _msgIndex;

    List<string> _messages;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _timer = 0;
        _charIndex = 0;
        _timePerChar = 0.05f;
        
        _messages = new List<string>();
        _messages.Add("與友軍順利突破包圍網後，你與小隊成員在原地稍作休整，不過根據出去偵查的成員回來匯報，於東方山脈上發現有座敵軍坦克工廠。\r\n\r\n刻不容緩之際，你未將這層消息上報至最高指揮部，而是兵貴神速率領小隊突襲，若成功則將為前線友軍緩解龐大壓力，即便失敗也能作為誘餌，只是你的小隊將獨自面對源源不絕的敵軍。\r\n");
        _msgIndex = 0;
        _currentMsg = _messages[_msgIndex];
    }

    private void Update()
    {


        if (string.IsNullOrEmpty(_currentMsg))
        {
            return;
        }

        if (_charIndex >= _messages[_msgIndex].Length)
        {
            _charIndex = 0;
            _msgIndex += 1;
            if (_msgIndex >= _messages.Count)
            {
                return;
            }
            _currentMsg += _messages[_msgIndex];
        }

        _timer -= Time.deltaTime;

        if (_timer < 0 )
        {
            _charIndex += 1;
            _timer += _timePerChar;


            string _tmpMsg = _currentMsg.Substring(0 , _charIndex);
            TextComponent.text = _tmpMsg;
        }

        if (_charIndex >= _currentMsg.Length)
        {
            _currentMsg = null;
        }
        

    }


}
