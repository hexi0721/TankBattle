using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypewriterAssistant : MonoBehaviour
{
    
    private void Start()
    {
        

        Typewriter1.Add("�P�ͭx���Q��}�]�����A�A�P�p�������b��a�y�@���A���L�ھڥX�h���d�������^�Ӷ׳��A��F��s�ߤW�o�{���y�ĭx�Z�J�u�t�C\r\n\r\n", () => { Debug.Log("CallBack"); });
        Typewriter1.Add("�褣�e�w���ڡA�A���N�o�h�����W���̰ܳ��������A�ӬO�L�Q���t�v��p����ŧ�A�Y���\�h�N���e�u�ͭx�w���e�j���O�A" +
            "�Y�K���Ѥ]��@������A�u�O�A���p���N�W�ۭ��﷽���������ĭx�C\r\n" , () => { Debug.Log("CallBack"); });

        Typewriter1.Activate();
        
    }

    

}
