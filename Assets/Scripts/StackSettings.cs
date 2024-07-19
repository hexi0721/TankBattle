using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackSettings 
{
    // 僅限菜單使用的stack
    public static List<GameObject> _stack;

    public static void AddStack(GameObject go) // 添加GameObject 並啟用
    {
        go.SetActive(true);
        _stack.Add(go);
    }

    public static void PullStack() // 將最後一個GameObject踢出 啟用改成關閉
    {

        GameObject go = _stack[_stack.Count - 1];
        go.SetActive(false);
        _stack.RemoveAt(_stack.Count - 1);

    }


}
