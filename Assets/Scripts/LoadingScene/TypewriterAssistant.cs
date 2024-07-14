using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypewriterAssistant : MonoBehaviour
{
    // public TypewriterScriptableObj ScriptableOnbj;
    
    public TextAsset textAsset;
    private void Start()
    {

        // 普通版
        /* 
        Typewriter1.Add("與友軍順利突破包圍網後，你與小隊成員在原地稍作休整，不過根據出去偵查的成員回來匯報，於東方山脈上發現有座敵軍坦克工廠。\r\n\r\n", () => { Debug.Log("CallBack1"); });
        Typewriter1.Add("刻不容緩之際，你未將這層消息上報至最高指揮部，而是兵貴神速率領小隊突襲，若成功則將為前線友軍緩解龐大壓力，" +
            "即便失敗也能作為誘餌，只是你的小隊將獨自面對源源不絕的敵軍。\r\n" , () => { Debug.Log("CallBack2"); });
        */

        // ScriptableObj
        //Typewriter1.Add(ScriptableOnbj);

        // Json
        TypewriterJson json = JsonUtility.FromJson<TypewriterJson>(textAsset.text);
        TypewriterAndLodeScene.Add(json);

        TypewriterAndLodeScene.Activate();
        
    }

    

}
