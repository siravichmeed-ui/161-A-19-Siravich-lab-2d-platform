using UnityEngine;

public class AttachHealthBarScreen : MonoBehaviour
{
    [SerializeField] private PlayerHPBar hpBarPrefab;   // prefab HP bar ที่อยู่ใน Canvas หลัก
    [SerializeField] private Canvas uiCanvas;           // Canvas หลัก (แบบ Screen Space)
    [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1.2f, 0f);
    private Character owner;
    private PlayerHPBar bar;
    void Start()
    {
        var owner = GetComponent<Character>();
        var bar = Instantiate(hpBarPrefab, uiCanvas.transform);
        bar.Bind(owner);   // ผูก event กับเลือดของ Enemy

        // เพิ่มสคริปต์ให้ UI วิ่งตาม Enemy
        var follow = bar.gameObject.AddComponent<UIFollowTarget>();
        follow.target = transform;
        follow.worldOffset = worldOffset;

        owner.OnHealthChanged += OnOwnerHealthChanged;
    }
    private void OnOwnerHealthChanged(int cur, int max)
    {
        if (cur <= 0)
            Cleanup();
    }
    private void OnDestroy()
    {
        Cleanup(); // กันกรณีศัตรูถูกลบด้วยเหตุผลอื่น
    }
    private void Cleanup()
    {
        if (owner != null)
            owner.OnHealthChanged -= OnOwnerHealthChanged;

        if (bar != null)
        {
            Destroy(bar.gameObject);
            bar = null;
        }
    }
}
