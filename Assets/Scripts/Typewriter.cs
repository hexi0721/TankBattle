using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Typewriter : MonoBehaviour
{
    static Typewriter _instance;

    public TMP_Text TextComponent;

    float _timer;
    [SerializeField] int _charIndex;
    float _timePerChar;
    [SerializeField] string _currentMsg;
    [SerializeField] int _msgIndex;
    bool _done;

    List<string> _messages;

    private void Awake()
    {
        _instance = this;

        _messages = new List<string>();
        _messages.Add("與友軍順利突破包圍網後，你與小隊成員在原地稍作休整，不過根據出去偵查的成員回來匯報，於東方山脈上發現有座敵軍坦克工廠。\r\n\r\n");
        _messages.Add("刻不容緩之際，你未將這層消息上報至最高指揮部，而是兵貴神速率領小隊突襲，若成功則將為前線友軍緩解龐大壓力，即便失敗也能作為誘餌，只是你的小隊將獨自面對源源不絕的敵軍。\r\n");
        _msgIndex = 0;
        _currentMsg = _messages[_msgIndex];
    }

    private void Start()
    {
        _timer = 0;
        _charIndex = 0;
        _timePerChar = 0.05f;
        _done = false;
    }

    private void Update()
    {

        if (_msgIndex >= _messages.Count)
        {
            return;
        }

        if (_done)
        {
            _done = false;
            _msgIndex++;
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
            _done = true;
        }
        

    }

    bool IsActive()
    {
        Debug.Log(_charIndex < _currentMsg.Length);
        return _charIndex < _currentMsg.Length;
    }

    public void WriteNextMessageInQueue()
    {

        if(IsActive())
        {
            TextComponent.text = _currentMsg;
            _charIndex = _currentMsg.Length;

            
            _done = true;
        }

    }

}
