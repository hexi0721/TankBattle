using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyFactory : MonoBehaviour
{
    public Image FacHp;

    float _maxHp;
    public float MaxHp
    {
        get => _maxHp;
    }
    [SerializeField] float _hp;
    public float Hp
    {
        get => _hp;
    }
    [SerializeField] float _hpShowTime;

    static EnemyFactory _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _maxHp = 50f;
        _hp = 50f;
        _hpShowTime = 4f;

        FacHp.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {

        if(FacHp.transform.parent.gameObject.activeSelf)
        {
            _hpShowTime -= Time.deltaTime;
            if(_hpShowTime <= 0)
            {
                FacHp.transform.parent.gameObject.SetActive(false);
                
            }
        }

        FacHp.fillAmount = _hp / _maxHp;

        if (_hp < 0)
        {
            FacHp.transform.parent.gameObject.SetActive(false);
            // Destroy(transform.parent.gameObject);
            SceneManager.LoadScene("Story");
        }
    }

    public static void UnderAttack()
    {
        _instance._hp -= Random.Range(1, 5);
        _instance.FacHp.transform.parent.gameObject.SetActive(true);
        _instance._hpShowTime = 4f;
    }

}
