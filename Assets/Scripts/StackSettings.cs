using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSettings 
{
    // �ȭ����ϥΪ�stack
    public static List<GameObject> _stack;

    public static void AddStack(GameObject go) // �K�[GameObject �ñҥ�
    {
        go.SetActive(true);
        _stack.Add(go);
    }

    public static void PullStack() // �N�̫�@��GameObject��X �ҥΧ令����
    {

        GameObject go = _stack[_stack.Count - 1];
        go.SetActive(false);
        _stack.RemoveAt(_stack.Count - 1);

    }


}
