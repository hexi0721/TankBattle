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
        _currentMsg = "與友軍順利突破包圍網後，你與小隊成員在原地稍作休整，不過根據回來的偵查員匯報，於東方山脈上發現有座敵軍坦克工廠，刻不容緩之際，你未將這層消息上報至最高指揮部，而是趁勢率領小隊突襲工廠，若成功則將為前線友軍緩解龐大壓力，即便失敗也能作為誘餌，只是你的小隊將獨自面對源源不絕的敵軍。";
        _messages = new List<string>();
    }

    private void Update()
    {

        if(string.IsNullOrEmpty(_currentMsg))
        {
            return;
        }    

        _timer -= Time.deltaTime;

        if (_timer < 0 )
        {
            _charIndex += 1;
            _timer += _timePerChar;


            string _tmpMsg = _currentMsg.Substring(0 , _charIndex);
            TextComponent.text = _tmpMsg;
        }

        
        if(_charIndex > _currentMsg.Length)
        {
            _currentMsg = null;
        }
        




    }



}
