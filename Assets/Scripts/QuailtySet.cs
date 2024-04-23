using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QualitySet : MonoBehaviour
{

    private TMP_Dropdown dropdown;


    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.value = QualitySettings.GetQualityLevel();
    }


    public void Qualitytransform()
    {
        int value = dropdown.value;
        QualitySettings.SetQualityLevel(value);


    }
}