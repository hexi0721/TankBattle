using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionSetting : MonoBehaviour
{
    
    Resolution[] resolutionsArr;
    public  TMP_Dropdown resolutionDropdown;

    [SerializeField] List<string> s;
    string[] words;

    void Start()
    {
        resolutionsArr = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        s = new List<string>();

        for (var i = 0; i < resolutionsArr.Length; i++)
        {
            // 把重複的解析度去掉
            if(i >= 1)
            {
                if (resolutionsArr[i].width == resolutionsArr[i-1].width && resolutionsArr[i].height == resolutionsArr[i - 1].height)
                {
                    continue;
                }
            }
            
            // 16 : 9
            if((float)resolutionsArr[i].width / 16f == (float)resolutionsArr[i].height / 9f)
            {
                s.Add(resolutionsArr[i].width + " x " + resolutionsArr[i].height);
            }
            
            /*
            if((float)resolutionsArr[i].width > 800f)
            {
                s.Add(resolutionsArr[i].width + " x " + resolutionsArr[i].height);
            }
            */

        }

        resolutionDropdown.AddOptions(s);
        Get(); // 刷新解析度選項 以免出現錯誤
        
    }


    public void Set() // 設定解析度
    {

        words = s[resolutionDropdown.value].Split("x");
        PlayerPrefs.SetInt("currentResolutionIndex", resolutionDropdown.value);

        Screen.SetResolution(int.Parse(words[0]) , int.Parse(words[1]), FullScreenMode.Windowed, new RefreshRate() { numerator = 60, denominator = 1 });    
    }

    void Get()
    {
        
        resolutionDropdown.value = PlayerPrefs.GetInt("currentResolutionIndex");
        resolutionDropdown.RefreshShownValue();
    }
}