using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManage : MonoBehaviour
{

    //public GameObject MapCam; // �a�����Y
    //public RectTransform MapBG , MapCanvas; // �a��bg , MapCanvas 

    public Image OpeningBlackBg;

    public GameObject SubMenu;

    [SerializeField] bool _IsOpenMenu;
    // bool _showMap ;
    public bool IsOpenMenu // ���O�_�}��
    {
        get => _IsOpenMenu;
        set => _IsOpenMenu = value;
    }
    float _speed = 5f;

    GameObject _PlayerTank ;

    bool _backuptrigger;  
    public bool BackUpTrigger // �Ĥ贩�xĲ�o
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
        _IsOpenMenu = false; // true �O�}�ҵ�� false �O�������
        /*
        MapCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        MapBG.localPosition = new Vector3(-MapCanvas.rect.width / 2 , 0, 0);
        */
        _PlayerTank = GameObject.FindWithTag("Player");

        StackSettings._stack = new List<GameObject>();
    }

    private void Update()
    {


        // TabKeyCode();

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if(StackSettings._stack.Count != 0)
            {

                if(StackSettings._stack.Count == 1)
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


        if (_PlayerTank == null && PlayerSetting.Instance.Hp2 >= 0) // �ץ����a���` ����HP���T����
        {

            PlayerSetting.Instance.Hp2 -= 1;
            PlayerSetting.Instance.HpImage2.fillAmount = PlayerSetting.Instance.Hp2 / 100;
        }

        // �}����¹��h��
        float BlackBgAlpha = OpeningBlackBg.color.a - (Time.deltaTime * 0.25f);
        OpeningBlackBg.color = new Color(0, 0, 0, BlackBgAlpha);

        
        

    }

    public static void OpenCloseMenu()
    {

        _instance.IsOpenMenu = !_instance.IsOpenMenu;

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
