using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class CameraController : MonoBehaviour
{

    public GameObject turret;
    public GameObject Muzzle;
    
    GameManage gameManage;
    
    Vector3 _rotation;
    [SerializeField] float CamSmoothFactor;
    float _LookUpMin , _LookUpMax ;

    
    public GameObject MuzzleAimImage; 
    public GameObject AimImage; 
    public RectTransform AimC; // Aim canva
    [SerializeField] Image _blackBg;
    float BlackBgAlpha;
    [SerializeField] bool boolBlackBg;

    Vector2 screenPos;

    public Vector2 ScreenPos 
    {
        get => screenPos;

    }

    private void Start()
    {
        gameManage = GameObject.FindWithTag("GameManage").GetComponent<GameManage>();
        _blackBg = GameObject.FindWithTag("BlackBg").GetComponent<Image>();
        BlackBgAlpha = _blackBg.color.a;
        boolBlackBg = false;

        _rotation = turret.transform.localEulerAngles;

        _LookUpMin = -10 ;
        _LookUpMax = 6;
        CamSmoothFactor = 2f;
    }

    private void Update()
    {
        BlackBgInOut(); // 黑幕

        
    }



    private void LateUpdate()
    {
        if(PlayerSetting.Instance.Hp <= 0)
        {
            boolBlackBg = true;
            transform.localPosition = new Vector3(transform.localPosition.x , 0 , transform.localPosition.z);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y , -35f);
            
        }
        else
        {
            boolBlackBg = false;

            transform.position = turret.transform.position + turret.transform.up * 1.1f;
            switch (!PlayerSetting.Instance.animator.enabled)
            {
                case false:

                    transform.forward = turret.transform.forward;

                    break;

                case true:

                    if (!gameManage.IsOpenMenu)
                    {

                        
                        screenPos = GetComponent<Camera>().WorldToViewportPoint(Muzzle.transform.position + Muzzle.transform.forward * 1000);
                        screenPos.y = 0.5f;

                        if (Input.GetMouseButton(1)) 
                        {
                            MuzzleAimImage.SetActive(true);
                            MuzzleAimImage.transform.localPosition = new Vector3((screenPos.x * AimC.rect.width) - AimC.rect.width / 2, ((screenPos.y * AimC.rect.height) - AimC.rect.height / 2), 0);

                            AimImage.transform.localScale = Vector3.Lerp(AimImage.transform.localScale, Vector3.one * 3.5f, 4f * Time.deltaTime);
                            GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetComponent<Camera>().fieldOfView, 10f, 4f * Time.deltaTime);

                        }
                        else
                        {
                            MuzzleAimImage.SetActive(false);


                            AimImage.transform.localScale = Vector3.Lerp(AimImage.transform.localScale, Vector3.one * 8f, 4f * Time.deltaTime);
                            GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetComponent<Camera>().fieldOfView, 60f, 4f * Time.deltaTime);

                        }

                        
                        _rotation.x += Input.GetAxis("Mouse Y") * CamSmoothFactor * (-1);
                        _rotation.y += Input.GetAxis("Mouse X") * CamSmoothFactor;

                        _rotation.x = Mathf.Clamp(_rotation.x, _LookUpMin, _LookUpMax);
                        transform.localEulerAngles = _rotation;


                    }

                    break;
            }
        }

        
        
    }

    private void BlackBgInOut()
    {
        if (boolBlackBg)
        {
            BlackBgAlpha += Time.deltaTime * 0.25f;
        }
        else
        {
            BlackBgAlpha -= Time.deltaTime * 0.25f;
        }
        BlackBgAlpha = Mathf.Clamp(BlackBgAlpha, 0f, 1f);
        _blackBg.color = new Color(0, 0, 0, BlackBgAlpha);
    }

}
