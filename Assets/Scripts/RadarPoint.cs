using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPoing : MonoBehaviour
{
    public Camera MapCamera; // �a�����Y
    public GameObject Obj; // ����@�ɮy��
    public RectTransform Map; // �a�� canva

    private void Update()
    {

        Vector3 v = MapCamera.WorldToViewportPoint(Obj.transform.position);
        
        transform.localPosition = new Vector2((v.x * Map.rect.width) - Map.rect.width / 2, (v.y * Map.rect.height) - Map.rect.height / 2);



    }

}
