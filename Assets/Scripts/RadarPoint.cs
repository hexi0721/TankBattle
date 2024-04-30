using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPoing : MonoBehaviour
{
    public Camera MapCamera; // �a�����Y
    public GameObject Obj; // ����@�ɮy��
    public RectTransform Map; // �a�� canva
    public RectTransform MapBG; // �a�� BG

    
    private void Update()
    {

        if(Obj != null)
        {
            Vector3 v = MapCamera.WorldToViewportPoint(Obj.transform.position);
            transform.localPosition = new Vector3((v.x * Map.rect.width) - Map.rect.width / 2, (v.y * Map.rect.height) - Map.rect.height / 2 , -7) + MapBG.transform.localPosition;


        }
        else
        {
            Destroy(this.gameObject);
        }

    }

}
