using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManage : MonoBehaviour
{

    //public GameObject MapCam; // 地圖鏡頭
    //public RectTransform MapBG , MapCanvas; // 地圖bg , MapCanvas 

    [SerializeField] Image _blackBg;
    float BlackBgAlpha;

    public GameObject SubMenu;

    [SerializeField] bool _IsOpenMenu;
    // bool _showMap ;
    public bool IsOpenMenu // 菜單是否開啟
    {
        get => _IsOpenMenu;
        set => _IsOpenMenu = value;
    }
    float _speed = 5f;

    GameObject _PlayerTank ;

    [SerializeField] bool _backuptrigger;  
    public bool BackUpTrigger // 敵方援軍觸發
    {
        get => _backuptrigger;
        set => _backuptrigger = value;
    }

    static GameManage _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Cursor.visible = false;

        //_showMap = false;
        _IsOpenMenu = false; // true 是開啟菜單 false 是關閉菜單
        /*
        MapCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        MapBG.localPosition = new Vector3(-MapCanvas.rect.width / 2 , 0, 0);
        */
        _PlayerTank = GameObject.FindWithTag("Player");
        _blackBg = GameObject.FindWithTag("BlackBg").GetComponent<Image>();
        BlackBgAlpha = _blackBg.color.a;

        StackSettings._stack = new List<GameObject>();
    }

    private void Update()
    {
        BlackBgInOut(); // 黑幕

        // TabKeyCode();

        EscKeyCode();
        

        switch (IsOpenMenu)
        {
            case true:

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                
                SubMenu.GetComponent<CanvasGroup>().alpha += _speed * Time.deltaTime;

                if(SubMenu.GetComponent<CanvasGroup>().alpha == 1.0f)
                {
                    SubMenu.GetComponent<CanvasGroup>().interactable = true;
                }
                else
                {
                    SubMenu.GetComponent<CanvasGroup>().interactable = false;
                }



                break;

            case false:

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;


                SubMenu.GetComponent<CanvasGroup>().alpha -= _speed * Time.deltaTime;

                break;
        }


        if (_PlayerTank == null && PlayerSetting.Instance.Hp2 >= 0) // 修正玩家死亡 紅色HP正確消除
        {

            PlayerSetting.Instance.Hp2 -= 1;
            PlayerSetting.Instance.HpImage2.fillAmount = PlayerSetting.Instance.Hp2 / 100;
        }


    }

    public static void OpenCloseMenu()
    {

        _instance.IsOpenMenu = !_instance.IsOpenMenu;

    }

    void EscKeyCode()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (StackSettings._stack.Count != 0)
            {

                if (StackSettings._stack.Count == 1)
                {
                    IsOpenMenu = false;
                }
                StackSettings.PullStack();

            }
            else
            {
                IsOpenMenu = true;
                StackSettings.AddStack(SubMenu);
            }


        }
    }

    private void BlackBgInOut()
    {
        if (CameraController.boolBlackBg)
        {
            BlackBgAlpha += Time.deltaTime * 0.25f;
            if (BlackBgAlpha >= 1f)
            {
                SceneManager.LoadScene("Defeat");
            }
        }
        else
        {
            BlackBgAlpha -= Time.deltaTime * 0.25f;
        }
        BlackBgAlpha = Mathf.Clamp(BlackBgAlpha, 0f, 1f);
        _blackBg.color = new Color(0, 0, 0, BlackBgAlpha);

        
    }

    /*
    private void TabKeyCode()
    {
        if (Input.GetKey(KeyCode.Tab) && !_instance.IsOpenMenu)
        {
            _showMap = true;

            if (_showMap)
            {
                MapBG.localPosition = Vector3.Lerp(MapBG.localPosition, new Vector3(0, MapBG.localPosition.y, MapBG.localPosition.z), _speed * Time.deltaTime);

                MapCanvas.GetComponent<CanvasGroup>().alpha += _speed * Time.deltaTime;

            }

        }

        if (Input.GetKeyUp(KeyCode.Tab) || _instance.IsOpenMenu)
        {
            _showMap = false;

        }
        if (!_showMap)
        {
            MapBG.localPosition = Vector3.Lerp(MapBG.localPosition, new Vector3(-MapCanvas.rect.width / 2, MapBG.localPosition.y, MapBG.localPosition.z), _speed * Time.deltaTime);

            MapCanvas.GetComponent<CanvasGroup>().alpha -= _speed * Time.deltaTime;
        }
    }
    */

}
