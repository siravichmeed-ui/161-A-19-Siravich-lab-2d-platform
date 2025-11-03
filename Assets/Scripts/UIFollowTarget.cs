using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    public Transform target;        // วัตถุที่จะให้ตาม
    public Vector3 worldOffset;     // ระยะห่างเหนือหัว
    private Camera cam;
    private RectTransform rect;

    void Awake()
    {
        cam = Camera.main;
        rect = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (!target || !cam) return;

        // คำนวณตำแหน่งบนหน้าจอจากตำแหน่งโลก
        Vector3 screenPos = cam.WorldToScreenPoint(target.position + worldOffset);

        // ถ้าหลุดหลังกล้องจะไม่โชว์
        gameObject.SetActive(screenPos.z > 0);
        rect.position = screenPos;

        // ป้องกันไม่ให้ HPBar หมุนตามมุมกล้อง
        rect.rotation = Quaternion.identity;
        rect.localScale = Vector3.one;
    }
}
