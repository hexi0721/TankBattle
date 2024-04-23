using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ResolutionSetting : MonoBehaviour
{

    Resolution[] resolutionsArr;
    public  TMP_Dropdown resolutionDropdown;

    
    void Start()
    {
        resolutionsArr = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> s = new List<string>();

        foreach (var resolution in resolutionsArr)
        {
            s.Add(resolution.width + " x " + resolution.height);
        }

        resolutionDropdown.AddOptions(s);

        Get();
        
    }


    public void Set()
    {

        Screen.SetResolution(resolutionsArr[resolutionDropdown.value].width, resolutionsArr[resolutionDropdown.value].height, Screen.fullScreen ,60);         
    }

    void Get()
    {
        var currentResolutionIndex = 0;

        
        for(var i = 0;i < resolutionsArr.Length;i++)
        {
            if (resolutionsArr[i].width == Screen.width && resolutionsArr[i].height == Screen.height)
            {
                currentResolutionIndex = i;
                
            }
        }
            
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
}