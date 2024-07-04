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
        _currentMsg = "�P�ͭx���Q��}�]�����A�A�P�p�������b��a�y�@���A���L�ھڦ^�Ӫ����d���׳��A��F��s�ߤW�o�{���y�ĭx�Z�J�u�t�A�褣�e�w���ڡA�A���N�o�h�����W���̰ܳ��������A�ӬO�X�ղv��p����ŧ�u�t�A�Y���\�h�N���e�u�ͭx�w���e�j���O�A�Y�K���Ѥ]��@������A�u�O�A���p���N�W�ۭ��﷽���������ĭx�C";
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
