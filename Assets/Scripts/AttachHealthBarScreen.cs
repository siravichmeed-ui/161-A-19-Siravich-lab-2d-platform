using UnityEngine;

public class AttachHealthBarScreen : MonoBehaviour
{
    [SerializeField] private PlayerHPBar hpBarPrefab;   // Prefab ของ HPBar
    [SerializeField] private Canvas uiCanvas;           // Canvas หลัก (Screen Space)
    [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1.2f, 0f);

    private Character owner;
    private PlayerHPBar bar;

    void Start()
    {
        // ใช้ field owner / bar ไม่สร้างตัวแปรใหม่
        owner = GetComponent<Character>();
        if (owner == null)
        {
            Debug.LogError("[AttachHealthBarScreen] Character component not found on " + name);
            enabled = false;
            return;
        }

        if (hpBarPrefab == null || uiCanvas == null)
        {
            Debug.LogError("[AttachHealthBarScreen] hpBarPrefab or uiCanvas is not assigned on " + name);
            enabled = false;
            return;
        }

        bar = Instantiate(hpBarPrefab, uiCanvas.transform);
        bar.Bind(owner);   // ผูก HPBar เข้ากับตัวละครนี้

        // ให้ UI วิ่งตาม Enemy
        var follow = bar.gameObject.AddComponent<UIFollowTarget>();
        follow.target = transform;
        follow.worldOffset = worldOffset;

        // สมัครฟัง event เวลาเลือดเปลี่ยน
        owner.OnHealthChanged += OnOwnerHealthChanged;
    }

    private void OnOwnerHealthChanged(int cur, int max)
    {
        // ถ้าเลือดหมด ให้เคลียร์ HPBar
        if (cur <= 0)
        {
            Cleanup();
        }
    }

    private void OnDestroy()
    {
        // กันกรณีตัวละครโดน Destroy ด้วยเหตุผลอื่น
        Cleanup();
    }

    private void Cleanup()
    {
        // ยกเลิกฟัง event
        if (owner != null)
        {
            owner.OnHealthChanged -= OnOwnerHealthChanged;
            owner = null;
        }

        // ลบ HPBar ออกจาก Canvas
        if (bar != null)
        {
            Destroy(bar.gameObject);
            bar = null;
        }
    }
}
