using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data" , menuName = "ScriptableObj/TypewriterScriptableObj" , order = 1)]
public class TypewriterScriptableObj : ScriptableObject
{

    public List<TypewriterMessage> Messages = new List<TypewriterMessage>();


}
